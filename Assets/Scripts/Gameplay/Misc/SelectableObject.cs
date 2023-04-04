using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableObject : MonoBehaviour
{
    public bool IsSelected { get; private set; }

    [SerializeField] private Transform[] _outlineTransforms;
    [SerializeField] private Outline.Mode _outlineMode;
    [SerializeField, Range(0f, 10f)] private float _outlineWidth;

    private List<Outline> _outlines;
    
    private void Start()
    {
        IsSelected = false;
        _outlines = new List<Outline>();

        foreach (Transform transform in _outlineTransforms)
        {
            Outline outline = transform.gameObject.AddComponent<Outline>();
            outline.OutlineMode = _outlineMode;
            outline.OutlineWidth = _outlineWidth;
            _outlines.Add(outline);
            outline.enabled = false;
        }
    }

    public void SetOutlineActive(bool active)
    {
        if (_outlines == null) return;
        IsSelected = active;

        foreach (Outline outline in _outlines)
        {
            outline.enabled = active;
        }
    }

    public void SetOutlineWidth(float width)
    {
        foreach (Outline outline in _outlines)
        {
            outline.OutlineWidth = width;
        }
    }

    public void SetDefaultOutlineWidth()
    {
        foreach (Outline outline in _outlines)
        {
            outline.OutlineWidth = _outlineWidth;
        }
    }
}
