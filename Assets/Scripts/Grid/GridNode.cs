using UnityEngine;

namespace Grid
{
    public sealed class GridNode : MonoBehaviour
    {
        private GridComponent _grid;
        private GridPosition _position;
        private Highlighter _highlighter;
    
        public void Init(GridPosition pos, GridComponent grid)
        {
            _position = pos;
            _grid = grid;
            _highlighter = GetComponent<Highlighter>();
        }

        public GridPosition Position => _position;

        public Highlighter Highlighter => _highlighter;
    }
}
