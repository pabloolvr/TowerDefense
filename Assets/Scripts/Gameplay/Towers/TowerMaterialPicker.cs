using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerMaterialPicker : MonoBehaviour
{
    [SerializeField] private Material _greenMaterial;
    [SerializeField] private Material _redMaterial;

    private Material _curMaterial;
    private MeshRenderer[] _renderers;
    private Dictionary<MeshRenderer, Material> _defaultMaterials;

    void Start()
    {
        _curMaterial = null;
        _renderers = GetComponentsInChildren<MeshRenderer>();
        _defaultMaterials = new Dictionary<MeshRenderer, Material>();

        foreach (MeshRenderer renderer in _renderers)
        {
            _defaultMaterials.Add(renderer, renderer.material);
        }
    }

    public void SetDefaultMaterials()
    {
        if (_curMaterial == null) return;

        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material = _defaultMaterials[renderer];
        }

        _curMaterial = null;
    }

    public void SetGreenMaterial()
    {
        if (_curMaterial == _greenMaterial) return;

        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material = _greenMaterial;
        }

        _curMaterial = _greenMaterial;
    }

    public void SetRedMaterial()
    {
        if (_curMaterial == _redMaterial) return;

        foreach (MeshRenderer renderer in _renderers)
        {
            renderer.material = _redMaterial;
        }

        _curMaterial = _redMaterial;
    }
}
