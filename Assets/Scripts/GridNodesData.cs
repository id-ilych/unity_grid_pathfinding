using System;
using System.Collections.Generic;
using UnityEngine.UI;

public class GridNodesData<T>
{
    private readonly GridSize _gridSize;

    private readonly T[,] _data;

    public GridNodesData(GridSize gridSize, T defaultValue = default)
    {
        _gridSize = gridSize;
        _data = new T[gridSize.maxA - gridSize.minA + 1, gridSize.maxB - gridSize.minB + 1];

        if (!EqualityComparer<T>.Default.Equals(defaultValue, default))
        {
            Fill(defaultValue);
        }
    }

    public T this[GridPosition pos]
    {
        get => _data[pos.a - _gridSize.minA, pos.b - _gridSize.minB];
        set => _data[pos.a - _gridSize.minA, pos.b - _gridSize.minB] = value;
    }

    public GridSize GridSize => _gridSize;

    public void Fill(T value)
    {
        for (var i = 0; i < _data.GetLength(0); i++)
        {
            for (var j = 0; j < _data.GetLength(1); j++)
            {
                _data[i, j] = value;
            }
        }
    }

    public GridNodesData<U> Transform<U>(Func<T, U> op)
    {
        var result = new GridNodesData<U>(_gridSize);
        for (var i = 0; i < _data.GetLength(0); i++)
        {
            for (var j = 0; j < _data.GetLength(1); j++)
            {
                result._data[i, j] = op(_data[i, j]);
            }
        }

        return result;
    }
}