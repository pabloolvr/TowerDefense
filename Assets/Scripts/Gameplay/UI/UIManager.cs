using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public GameManager GameManager => _gameManager;
    public ScrollRect TowerCardsScrollRect => _towerCardsScrollRect;
    public Button TowersButton => _towersButton;
    public TowerInfoPanel TowerInfoPanel => _towerInfoPanel;
    public GameOverPanel GameOverPanel => _gameOverPanel;

    [SerializeField] private GameManager _gameManager;
    [SerializeField] private TextMeshProUGUI _playerHealthField;
    [SerializeField] private TextMeshProUGUI _playerGoldField;
    [SerializeField] private TextMeshProUGUI _playerScoreField;
    [SerializeField] private TowerInfoPanel _towerInfoPanel;
    [SerializeField] private ScrollRect _towerCardsScrollRect;
    [SerializeField] private Button _towersButton;
    [SerializeField] private GameOverPanel _gameOverPanel;
    [SerializeField] private Canvas[] _uiRegions;

    private UITowerCard[] _towerCards;
    private Camera _camera;
    private int _towerLayerMask;
    private Ray _mouseRay;
    private RaycastHit _mouseHit;
    private SelectableObject _curSelectableObject;

    private void Awake()
    {
        _camera = Camera.main;
        _towerLayerMask = 1 << 6;
        _curSelectableObject = null;

        _towerInfoPanel.OnUpgrade += () => 
        {
            _gameManager.PlayerManager.AddGoldAmount(-_towerInfoPanel.CurTower.UpgradeCost);
            UpdateGoldAmount(_gameManager.PlayerManager.PlayerGold);
            _towerInfoPanel.UpdateUpgradeButton(_gameManager.PlayerManager.PlayerGold);
        };
    }

    private void Start()
    {
        StartTowerCards();
    }

    private void Update()
    {
        _mouseRay = _camera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(_mouseRay.origin, _mouseRay.direction, out _mouseHit, Mathf.Infinity, _towerLayerMask))
        {
            //Debug.DrawRay(_mouseRay.origin, _mouseRay.direction * 1000, Color.yellow);
            if (_mouseHit.transform.parent.TryGetComponent(out SelectableObject selectableObject))
            {
                if (_curSelectableObject == null)
                {
                    _curSelectableObject = selectableObject;
                    _curSelectableObject.SetOutlineActive(true);
                }
                else if (_curSelectableObject != selectableObject)
                {
                    _curSelectableObject.SetOutlineActive(false);
                    _curSelectableObject.GetComponent<Tower>().ShowTowerRange(false);
                    _curSelectableObject = selectableObject;
                    _curSelectableObject.SetOutlineActive(true);
                }    
                
                if (_curSelectableObject.IsSelected)
                {
                    Tower towerComponent = _curSelectableObject.GetComponent<Tower>();

                    if (Input.GetMouseButtonDown(0))
                    {
                        SetTowerInfoPanel(towerComponent);
                        _towerInfoPanel.UpdateUpgradeButton(_gameManager.PlayerManager.PlayerGold);
                    }

                    towerComponent.ShowTowerRange(true);
                }
            }
        }
        else if (_curSelectableObject != null)
        {
            _curSelectableObject.SetOutlineActive(false);
            _curSelectableObject.GetComponent<Tower>().ShowTowerRange(false);
            _curSelectableObject = null;
        }
    }

    private void SetTowerInfoPanel(Tower tower)
    {
        _towerInfoPanel.CurTower = tower;
    }

    private void StartTowerCards()
    {
        _towerCards = _towerCardsScrollRect.content.GetComponentsInChildren<UITowerCard>(true);

        foreach (var towerCard in _towerCards)
        {
            towerCard.Initialize(this);
        }
    }

    public void UpdateGoldAmount(int amount)
    {
        _playerGoldField.text = "Gold: " + amount.ToString();

        _towerInfoPanel.UpdateUpgradeButton(amount);

        foreach (var towerCard in _towerCards)
        {
            towerCard.SetPlaceButtonInteractivity(amount);
        }
    }

    public void UpdateHealthAmount(int amount)
    {
        _playerHealthField.text = "Health: " + amount.ToString();
    }

    public void UpdateScore(int amount)
    {
        _playerScoreField.text = "Score: " + amount.ToString();
    }

    public void SetRegionsActive(bool active)
    {
        foreach (Canvas region in _uiRegions)
        {
            region.enabled = active;
        }
    }
}
