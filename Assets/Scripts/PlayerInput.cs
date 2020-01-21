using System.Collections;
using System.Collections.Generic;
using Grid;
using JetBrains.Annotations;
using References;
using UnityEngine;

public sealed class PlayerInput : MonoBehaviour
{
    public Camera camera;
    public GridPositionReference target;
    
    void Update()
    {
        if (!Input.GetMouseButtonDown(0))
        {
            return;
        }

        var ray = camera.ScreenPointToRay(Input.mousePosition);
        if (!Physics.Raycast(ray, out var hit))
        {
            return;
        }

        var node = ExtractGridNode(hit);
        if (node == null)
        {
            return;
        }

        target.Value = node.Position;
    }

    [CanBeNull]
    private GridNode ExtractGridNode(RaycastHit hit)
    {
        var t = hit.transform;
        while (t != null)
        {
            var node = t.gameObject.GetComponent<GridNode>();
            if (node)
            {
                return node;
            }

            t = t.parent;
        }

        return null;
    }
}
