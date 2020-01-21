using UnityEngine;

[CreateAssetMenu(menuName = "Grid/Size")]
public sealed class GridSize : ScriptableObject
{
    public int minA;
    public int minB;
    public int maxA;
    public int maxB;
}