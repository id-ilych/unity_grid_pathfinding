namespace Grid
{
    public sealed class GridGraph
    {
        private readonly GridSize _gridSize;
        private readonly bool[,] _data;
        private readonly int _aSize;
        private readonly int _bSize;
    
        public GridGraph(GridSize gridSize)
        {
            _gridSize = gridSize;
            _aSize = gridSize.maxA - gridSize.minA + 1;
            _bSize = gridSize.maxB - gridSize.minB + 1;
            var maxIdx = 1 + Index(new GridPosition(gridSize.maxA, gridSize.maxB));
            _data = new bool[maxIdx, maxIdx];
        }
    
        public void Connect(GridPosition a, GridPosition b)
        {
            var i = Index(a);
            var j = Index(b);
            _data[i, j] = true;
            _data[j, i] = true;
        }

        public bool IsConnected(GridPosition a, GridPosition b)
        {
            var i = Index(a);
            var j = Index(b);
            return _data[i, j];
        }

        private int Index(GridPosition pos)
        {
            var a = pos.a - _gridSize.minA;
            var b = pos.b - _gridSize.minB;
            return a + b * _aSize;
        }
    }
}
