using System;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "HexGridGeometry.asset", menuName = "Grid/HexGeometry")]
public sealed class HexGridGeometry : GridGeometry
{
    public double unit = 1;
    private double W => unit * Math.Sqrt(3.0);
    private double H => unit * 2.0;

    public override IEnumerable<GridPosition> NeighborPositions(GridPosition p, GridSize gridSize)
    {
        var even = p.b % 2 == 0;
        if (even)
        {
            if (p.a > gridSize.minA && p.b < gridSize.maxB)
            {
                yield return new GridPosition(p.a - 1, p.b + 1);
            }

            if (p.b < gridSize.maxB)
            {
                yield return new GridPosition(p.a, p.b + 1);
            }

            if (p.a > gridSize.minA)
            {
                yield return new GridPosition(p.a - 1, p.b);
            }

            if (p.a < gridSize.maxA)
            {
                yield return new GridPosition(p.a + 1, p.b);
            }

            if (p.a > gridSize.minA && p.b > gridSize.minB)
            {
                yield return new GridPosition(p.a - 1, p.b - 1);
            }

            if (p.b > gridSize.minB)
            {
                yield return new GridPosition(p.a, p.b - 1);
            }
        }
        else
        {
            if (p.b < gridSize.maxB)
            {
                yield return new GridPosition(p.a, p.b + 1);
            }

            if (p.a < gridSize.maxA && p.b < gridSize.maxB)
            {
                yield return new GridPosition(p.a + 1, p.b + 1);
            }

            if (p.a > gridSize.minA)
            {
                yield return new GridPosition(p.a - 1, p.b);
            }

            if (p.a < gridSize.maxA)
            {
                yield return new GridPosition(p.a + 1, p.b);
            }

            if (p.b > gridSize.minB)
            {
                yield return new GridPosition(p.a, p.b - 1);
            }

            if (p.a < gridSize.maxA && p.b > gridSize.minB)
            {
                yield return new GridPosition(p.a + 1, p.b - 1);
            }
        }
    }

    public override Vector2 PositionCoordinates(GridPosition p)
    {
        var x = p.a * W;
        var y = p.b * 3.0 * H / 4.0;
        var even = p.b % 2 == 0;
        if (!even)
        {
            x += W / 2.0;
        }

        return new Vector2((float) x, (float) y);
    }

    public override IEnumerable<(GridPosition, GridPosition)> AllConnections(GridSize gridSize)
    {
        for (var a = gridSize.minA; a <= gridSize.maxA; a++)
        {
            for (var b = gridSize.minB; b <= gridSize.maxB; b++)
            {
                var p = new GridPosition(a, b);
                var even = p.b % 2 == 0;
                if (even)
                {
                    if (p.a > gridSize.minA && p.b < gridSize.maxB)
                    {
                        yield return (p, new GridPosition(p.a - 1, p.b + 1));
                    }

                    if (p.b < gridSize.maxB)
                    {
                        yield return (p, new GridPosition(p.a, p.b + 1));
                    }

                    if (p.a < gridSize.maxA)
                    {
                        yield return (p, new GridPosition(p.a + 1, p.b));
                    }
                }
                else
                {
                    if (p.b < gridSize.maxB)
                    {
                        yield return (p, new GridPosition(p.a, p.b + 1));
                    }

                    if (p.a < gridSize.maxA && p.b < gridSize.maxB)
                    {
                        yield return (p, new GridPosition(p.a + 1, p.b + 1));
                    }

                    if (p.a < gridSize.maxA)
                    {
                        yield return (p, new GridPosition(p.a + 1, p.b));
                    }
                }
            }
        }
    }
}