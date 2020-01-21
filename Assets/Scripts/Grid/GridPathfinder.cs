using System.Collections.Generic;
using UnityEngine;

namespace Grid
{
    public static class GridPathfinder
    {
        public static GridPosition[] FindPath(GridPosition from, GridPosition to, GridGeometry geometry, GridSize size)
        {
            var visitedNodes = new GridNodesData<bool>(size);
            var traces = new GridNodesData<GridPosition>(size);
            var counter = 0;
            visitedNodes[from] = true;
            var nextSteps = new List<GridPosition> { from };
            var currentStep = new List<GridPosition>();
            while (nextSteps.Count != 0 && !visitedNodes[to])
            {
                var t = currentStep;
                currentStep = nextSteps;
                nextSteps = t;
                nextSteps.Clear();

                foreach (var p in currentStep)
                {
                    foreach (var n in geometry.NeighborPositions(p, size))
                    {
                        if (visitedNodes[n])
                        {
                            continue;
                        }
                    
                        // TODO can check for the walls between p and n here

                        visitedNodes[n] = true;
                        traces[n] = p;
                        nextSteps.Add(n);
                    }
                }

                counter++;
            }

            if (!visitedNodes[to])
            {
                Debug.LogError("Failed to find a path");
                return null;
            }

            var cursor = to;
            var result = new GridPosition[counter + 1];
            result[counter] = cursor;
            while (cursor != from)
            {
                counter--;
                cursor = traces[cursor];
                result[counter] = cursor;
            }

            return result;
        }
    }
}