using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGeometrySetter : MonoBehaviour
{
    public GridGeometryVariable variable;
    public GridGeometry value;
    
    public void Apply()
    {
        variable.Value = value;
    }
}
