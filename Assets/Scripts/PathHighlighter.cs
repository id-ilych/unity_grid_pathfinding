using System;
using System.Collections.Generic;
using Collections;
using Grid;
using References;
using UnityEngine;

public sealed class PathHighlighter : MonoBehaviour
{
    public GridPositionReference currentPosition;
    public GridSize size;
    public GridGeometryReference geometry;
    public HighlighterCollection highlighters;

    private GridPosition _prevTarget;
    private GridPosition _target;
    private bool _validationScheduled;

    public void Start()
    {
        _target = currentPosition.Value;
        _prevTarget = _target;
        if (!size.Contains(_target))
        {
            _target = new GridPosition();
            currentPosition.Value = _target;
        }
        
        currentPosition.AddListener(OnTargetChanged);
        _validationScheduled = true;
    }

    public void OnDestroy()
    {
        currentPosition.RemoveListener(OnTargetChanged);
    }

    public void Update()
    {
        if (!_validationScheduled)
        {
            return;
        }

        _validationScheduled = false;
        Validate();
    }

    private void OnTargetChanged()
    {
        _prevTarget = _target;
        _target = currentPosition.Value;
        Validate();
    }

    private void Validate()
    {
        var path = SafeBuildPath(_prevTarget, _target);
        DrawPath(path);
    }

    private GridPosition[] SafeBuildPath(GridPosition from, GridPosition to)
    {
        if (size.Contains(from) && size.Contains(to))
        {
            return GridPathfinder.FindPath(from, to, geometry.Value, size);
        }
        else if (size.Contains(to))
        {
            return new[] {to};
        }
        else
        {
            return new GridPosition[] { };
        }
    }

    private void DrawPath(GridPosition[] path)
    {
        var positions = new HashSet<GridPosition>(path);
        foreach (var highlighter in highlighters)
        {
            var node = highlighter.GetComponent<GridNode>();
            var highlighted = node && positions.Contains(node.Position);
            highlighter.ToggleHighlight(highlighted);
        }
    }
}
