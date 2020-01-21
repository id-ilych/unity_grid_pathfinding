using System;
using UnityEngine;

public struct GridPosition : IEquatable<GridPosition> {
    public readonly int a;
    public readonly int b;

    public GridPosition(int a, int b)
    {
        this.a = a;
        this.b = b;
    }

    public bool Equals(GridPosition other)
    {
        return this == other;
    }

    public override int GetHashCode()
    {
        return a.GetHashCode() * 17 + b.GetHashCode();
    }

    public static bool operator ==(GridPosition a, GridPosition b)
    {
        return a.a == b.a && a.b == b.b;
    }

    public static bool operator !=(GridPosition a, GridPosition b)
    {
        return a.a != b.a || a.b != b.b;
    }
}
