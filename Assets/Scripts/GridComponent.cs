using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using UnityEngine;

public sealed class GridComponent : MonoBehaviour
{
    [SerializeField]
    private GridSize size;
    [SerializeField]
    private GameObject nodePrefab;
    [SerializeField]
    private GameObject connectionPrefab;
    [SerializeField]
    private GridGeometryReference geometry;

    private Camera _camera;
    private GridNodesData<Highlighter> _highlighters;
    private GridPosition _target = new GridPosition(0, 0);
    private GridGeometry _geometry;
    
    void Start()
    {
        _camera = Camera.main;
        _geometry = geometry.Value;
        Reset();
        geometry.AddListener(OnGeometryChanged);
    }

    private void OnDestroy()
    {
        geometry.RemoveListener(OnGeometryChanged);
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
        {
            return;
        }

        var node = hit.transform.parent?.gameObject?.GetComponent<GridNode>();
        if (node == null)
        {
            return;
        }
        var from = _target;
        _target = node.Position;
        DrawPath(from, _target);
    }

    private void OnGeometryChanged()
    {
        _geometry = geometry.Value;
        Reset();
    }

    private void Reset()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }

        if (_geometry == null)
        {
            return;
        }
        
        var nodes = CreateNodes();
        CreateConnections(nodes);
        _highlighters = nodes.Transform(go => go.GetComponent<Highlighter>());
        _highlighters[_target].ToggleHighlight(true);
    }

    private void DrawPath(GridPosition from, GridPosition to)
    {
        var pathArr = GridPathfinder.FindPath(from, to, _geometry, size);
        var pathSet = new HashSet<GridPosition>(pathArr);
        foreach (var pos in _geometry.AllNodes(size))
        {
            var highlighter = _highlighters[pos];
            var highlighted = pathSet.Contains(pos);
            highlighter.ToggleHighlight(highlighted);
        }
    }

    private GridNodesData<GameObject> CreateNodes()
    {
        var result = new GridNodesData<GameObject>(size);
        foreach (var pos in _geometry.AllNodes(size))
        {
            var coords = _geometry.PositionCoordinates(pos);
            var vec = new Vector3(coords.x, coords.y, 0);
            var node = Instantiate(nodePrefab, vec, Quaternion.identity, transform);
            result[pos] = node;
            node.GetComponent<GridNode>().Init(pos, this);
        }
        return result;
    }

    private void CreateConnections(GridNodesData<GameObject> nodes)
    {
        foreach (var (f, t) in _geometry.AllConnections(size))
        {
            var from = _geometry.PositionCoordinates(f);
            var connection = Instantiate(connectionPrefab, from, Quaternion.identity, transform);
            connection.transform.LookAt(nodes[t].transform);
        }
    }
}
