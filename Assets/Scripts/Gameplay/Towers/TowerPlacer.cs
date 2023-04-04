using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerPlacer : MonoBehaviour
{
    public GameObject SelectedTower => _selectedTower;

    [SerializeField] private Transform _towerContainer;

    [SerializeField] private GameObject _towerI;

    public event Action OnTowerPlaced = () => { };
    public event Action OnTowerCanceled = () => { };

    private GameObject _selectedTower;
    private Camera _camera;
    private int _groundLayerMask;
    private RaycastHit _mouseHit;
    private Ray _mouseRay;

    private void Start()
    {
        _groundLayerMask = 1 << 7;
        _selectedTower = null;
        _camera = Camera.main;
    }

    private void Update()
    {
        if (_selectedTower == null) return;

        _mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_mouseRay.origin, _mouseRay.direction, out _mouseHit, Mathf.Infinity, _groundLayerMask);
        _selectedTower.transform.position = _mouseHit.point;

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
            OnTowerPlaced();
        }
    }

    public void RemoveListeners()
    {
        OnTowerPlaced = null;
        OnTowerCanceled = null;
    }

    public void CancelTowerPlacement()
    {
        Destroy(_selectedTower);
        _selectedTower = null;
        OnTowerCanceled();
    }

    public void SelectTower(GameObject towerObj)
    {
        RaycastHit _mouseHit;
        Ray _mouseRay = _camera.ScreenPointToRay(Input.mousePosition);
        Physics.Raycast(_mouseRay.origin, _mouseRay.direction, out _mouseHit, Mathf.Infinity, _groundLayerMask);

        _selectedTower = Instantiate(towerObj, _mouseHit.point, towerObj.transform.rotation, _towerContainer);
        _selectedTower.GetComponent<Tower>().IsBeingPlaced = true;
    }
}
