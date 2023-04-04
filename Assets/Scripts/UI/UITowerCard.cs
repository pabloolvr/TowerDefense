using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerCard : MonoBehaviour
{
    public Button PlaceButton => _placeButton;
    public int TotalTowerPrice => _towerPrefab.GetComponent<Tower>().GetPrice(PlacedTowersQty);
    public int PlacedTowersQty
    {
        get
        {
            return _placedTowersQty;
        }
        private set
        {
            _placedTowersQty = value;
            _priceField.text = "Price: " + TotalTowerPrice;
        }
    }

    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _descriptionField;
    [SerializeField] private TextMeshProUGUI _priceField;
    [SerializeField] private Button _placeButton;

    private TowerPlacer _towerPlacer;
    private UIManager _uiManager;
    private int _placedTowersQty;

    private void Awake()
    {
        Tower prefabTowerComponent = _towerPrefab.GetComponent<Tower>();
        _nameField.text = prefabTowerComponent.Name;
        _descriptionField.text = prefabTowerComponent.Description;
        PlacedTowersQty = 0;
    }

    public void Initialize(UIManager uiManager)
    {
        _uiManager = uiManager;
        _towerPlacer = _uiManager.GameManager.TowerPlacer;

        _placeButton.onClick.AddListener(() => 
        { 
            _towerPlacer.SelectTower(_towerPrefab);
            _uiManager.TowerCardsScrollRect.gameObject.SetActive(false);
            _uiManager.TowersButton.gameObject.SetActive(true);
            _towerPlacer.OnTowerPlaced += BuyNewTower;
            _towerPlacer.OnTowerCanceled += ResetTowerPlacerListeners;
        });
    }

    public void SetPlaceButtonInteractivity(int playerGold)
    {
        _placeButton.interactable = TotalTowerPrice <= playerGold;
    }

    private void BuyNewTower()
    {

        _uiManager.GameManager.PlayerManager.AddGoldAmount(-TotalTowerPrice);
        _uiManager.UpdateGoldAmount(_uiManager.GameManager.PlayerManager.PlayerGold);        
        PlacedTowersQty++;
        ResetTowerPlacerListeners();
    }

    private void ResetTowerPlacerListeners()
    {
        _towerPlacer.RemoveListeners();
    }
}
