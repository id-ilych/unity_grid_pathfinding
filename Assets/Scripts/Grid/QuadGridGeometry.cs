using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    [CreateAssetMenu(fileName = "QuadGridGeometry.asset", menuName = "Grid/QuadGeometry")]
    public sealed class QuadGridGeometry : GridGeometry
    {
        public Vector2 unitA = new Vector2(1, 0);
        public Vector2 unitB = new Vector2(0, 1);

        public override IEnumerable<GridPosition> NeighborPositions(GridPosition pos, GridSize gridSize)
        {
            if (pos.a > gridSize.minA)
            {
                yield return new GridPosition(pos.a - 1, pos.b);
            }

            if (pos.b > gridSize.minB)
            {
                yield return new GridPosition(pos.a, pos.b - 1);
            }

            if (pos.a < gridSize.maxA)
            {
                yield return new GridPosition(pos.a + 1, pos.b);
            }

            if (pos.b < gridSize.maxB)
            {
                yield return new GridPosition(pos.a, pos.b + 1);
            }
        }

        public override Vector2 PositionCoordinates(GridPosition p)
        {
            return unitA * p.a + unitB * p.b;
        }

        public override IEnumerable<(GridPosition, GridPosition)> AllConnections(GridSize gridSize)
        {
            for (var a = gridSize.minA; a <= gridSize.maxA; a++)
            {
                for (var b = gridSize.minB; b <= gridSize.maxB; b++)
                {
                    var pos = new GridPosition(a, b);
                    if (a < gridSize.maxA)
                    {
                        yield return (pos, new GridPosition(pos.a + 1, pos.b));
                    }

                    if (b < gridSize.maxB)
                    {
                        yield return (pos, new GridPosition(pos.a, pos.b + 1));
                    }
                }
            }
        }
    }
}