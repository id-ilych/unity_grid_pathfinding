using UnityEngine;

public struct GridPosition {
    public int a;
    public int b;

    public GridPosition(int a, int b)
    {
        this.a = a;
        this.b = b;
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
