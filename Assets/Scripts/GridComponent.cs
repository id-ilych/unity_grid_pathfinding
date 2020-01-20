using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class GridComponent : MonoBehaviour
{
    public GridSize size;
    public GridGeometry geometry;
    public GameObject nodePrefab;
    public GameObject connectionPrefab;

    private Camera _camera;
    private GridNodesData<Highlighter> _highlighters;
    private GridPosition _lastTarget = new GridPosition(0, 0);
    
    void Start()
    {
        _camera = Camera.main;
        var nodes = CreateNodes();
        CreateConnections(nodes);
        _highlighters = nodes.Transform(go => go.GetComponent<Highlighter>());
        _highlighters[_lastTarget].ToggleHighlight(true);
    }

    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var ray = _camera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;
        if (!Physics.Raycast(ray, out hit))
        {
            return;
        }

        var node = hit.transform.parent?.gameObject?.GetComponent<GridNode>();
        if (node == null)
        {
            return;
        }
        Debug.Log($"OnNodeTouched {node.Position.a}, {node.Position.b}");
        var from = _lastTarget;
        _lastTarget = node.Position;
        DrawPath(from, _lastTarget);
    }

    private void DrawPath(GridPosition from, GridPosition to)
    {
        var pathArr = GridPathfinder.FindPath(from, to, geometry, size);
        var pathSet = new HashSet<GridPosition>(pathArr);
        foreach (var pos in geometry.AllNodes(size))
        {
            var highlighter = _highlighters[pos];
            var highlighted = pathSet.Contains(pos);
            highlighter.ToggleHighlight(highlighted);
        }
    }

    private GridNodesData<GameObject> CreateNodes()
    {
        var result = new GridNodesData<GameObject>(size);
        foreach (var pos in geometry.AllNodes(size))
        {
            var coords = geometry.PositionCoordinates(pos);
            var vec = new Vector3(coords.x, coords.y, 0);
            var node = Instantiate(nodePrefab, vec, Quaternion.identity, transform);
            result[pos] = node;
            node.GetComponent<GridNode>().Init(pos, this);
        }
        return result;
    }

    private void CreateConnections(GridNodesData<GameObject> nodes)
    {
        foreach (var (f, t) in geometry.AllConnections(size))
        {
            var from = geometry.PositionCoordinates(f);
            var connection = Instantiate(connectionPrefab, from, Quaternion.identity, transform);
            connection.transform.LookAt(nodes[t].transform);
        }
    }
}
