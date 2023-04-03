using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    [SerializeField] private Transform _towerContainer;

    [SerializeField] private GameObject _towerI;

    private GameObject _selectedTower;
    private Camera _camera;
    private int _layerMask;

    void Start()
    {
        _layerMask = 1 << 7;
        _selectedTower = null;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_selectedTower == null) return;

        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);

        Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, _layerMask);
        _selectedTower.transform.position = hit.point;

        if (Input.GetMouseButtonDown(0))
        {
            TryPlaceTower();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            CancelTowerPlacement();
        }
    }

    public void TryPlaceTower()
    {
        Tower tower = _selectedTower.GetComponent<Tower>();

        if (!tower.IsTouchingOtherTowers)
        {
            _selectedTower.GetComponent<Tower>().IsBeingPlaced = false;
            _selectedTower = null;
        }
    }

    public void CancelTowerPlacement()
    {
        Destroy(_selectedTower);
        _selectedTower = null;
    }

    public void SelectTower()
    {
        RaycastHit hit;
        Ray ray = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(ray.origin, ray.direction, out hit, Mathf.Infinity, _layerMask);

        _selectedTower = Instantiate(_towerI, hit.point, _towerI.transform.rotation, _towerContainer);
        _selectedTower.GetComponent<Tower>().IsBeingPlaced = true;
    }
}
