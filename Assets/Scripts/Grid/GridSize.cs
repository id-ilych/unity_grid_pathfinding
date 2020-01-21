using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(menuName = "Grid/Size")]
    public sealed class GridSize : ScriptableObject
    {
        public int minA;
        public int minB;
        public int maxA;
        public int maxB;

        public bool Contains(GridPosition pos)
        {
            return minA <= pos.a && pos.a <= maxA && minB <= pos.b && pos.b <= maxB;
        }
    }
}