using System.Collections.Generic;
using References;
using UnityEngine;

namespace Grid
{
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

        private GridGeometry _geometry;
    
        void Start()
        {
            _geometry = geometry.Value;
            Reset();
            geometry.AddListener(OnGeometryChanged);
        }

        private void OnDestroy()
        {
            geometry.RemoveListener(OnGeometryChanged);
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
}
