using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GridGeometry : ScriptableObject {
    public abstract IEnumerable<GridPosition> NeighborPositions(GridPosition p, GridSize gridSize);
    public abstract Vector2 PositionCoordinates(GridPosition p);
    public abstract IEnumerable<(GridPosition, GridPosition)> AllConnections(GridSize gridSize);

    public IEnumerable<GridPosition> AllNodes(GridSize gridSize)
    {
        for (var a = gridSize.minA; a <= gridSize.maxA; a++)
        {
            for (var b = gridSize.minB; b <= gridSize.maxB; b++)
            {
                yield return new GridPosition(a, b);
            }
        }
    }
}
