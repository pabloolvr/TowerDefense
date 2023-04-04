using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UITowerCard : MonoBehaviour
{
    public Button PlaceButton => _placeButton;
    public int TotalTowerPrice => BaseTowerPrice + AdditionalTowerPrice;
    public int BaseTowerPrice => _towerPrefab.GetComponent<Tower>().BasePrice + _additionalTowerPrice;
    public int AdditionalTowerPrice
    {
        get
        {
            return _additionalTowerPrice;
        }
        private set
        {
            _additionalTowerPrice = value;
            _priceField.text = "Price: " + TotalTowerPrice.ToString();
        }
    }

    [SerializeField] private GameObject _towerPrefab;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _descriptionField;
    [SerializeField] private TextMeshProUGUI _priceField;
    [SerializeField] private Button _placeButton;

    private Tower _prefabTowerComponent;
    private TowerPlacer _towerPlacer;
    private UIManager _uiManager;
    
    private int _additionalTowerPrice;

    private void Awake()
    {
        _prefabTowerComponent = _towerPrefab.GetComponent<Tower>();
        _nameField.text = _prefabTowerComponent.Name;
        _descriptionField.text = _prefabTowerComponent.Description;
        AdditionalTowerPrice = 0;
    }

    public void Initialize(TowerPlacer towerPlacer, UIManager uiManager)
    {
        _towerPlacer = towerPlacer;
        _uiManager = uiManager;

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
        ResetTowerPlacerListeners();
    }

    private void ResetTowerPlacerListeners()
    {
        _towerPlacer.RemoveListeners();
    }
}
