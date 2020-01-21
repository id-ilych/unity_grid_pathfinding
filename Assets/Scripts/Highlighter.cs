using System;
using System.Collections;
using System.Collections.Generic;
using Collections;
using UnityEngine;
using UnityEngine.UIElements;

public sealed class Highlighter : MonoBehaviour
{
    public Material normalMaterial;
    public Material highlightedMaterial;
    public HighlighterCollection collection;
    
    private MeshRenderer[] _renderers = {};
    private bool _highlighted;
    private Material _lastMaterial;

    public void Start()
    {
        _renderers = GetComponentsInChildren<MeshRenderer>();
        _lastMaterial = _highlighted ? highlightedMaterial : normalMaterial;
        ApplyMaterial();
        
        collection.Add(this);
    }

    public void OnDestroy()
    {
        collection.Remove(this);
    }

    public void ToggleHighlight(bool highlighted)
    {
        _highlighted = highlighted;
        Validate();
    }

    private void Validate()
    {
        var material = _highlighted ? highlightedMaterial : normalMaterial;
        if (material == _lastMaterial)
        {
            return;
        }

        _lastMaterial = material;
        ApplyMaterial();
    }

    private void ApplyMaterial()
    {
        foreach (var r in _renderers)
        {
            r.material = _lastMaterial;
        }
    }
}
