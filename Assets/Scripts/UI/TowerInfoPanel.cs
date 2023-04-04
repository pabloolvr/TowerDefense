using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TowerInfoPanel : MonoBehaviour
{
    public Tower CurTower
    {
        get
        {
            return _curTower;
        } 
        set
        {
            if (_curTower)
            {
                _curTower.SelectableComponent.SetOutlineActive(false);
                _curTower.SelectableComponent.SetDefaultOutlineWidth();
            }

            _curTower = value;

            if (_curTower)
            {
                SetTowerInfoPanel();
            }
        }
    }

    public TextMeshProUGUI NameField => _nameField;
    public TextMeshProUGUI LevelField => _levelField;
    public TextMeshProUGUI UpgradeCostField => _upgradeCostField;
    public TextMeshProUGUI[] CombatInfoField => _combatField;
    public Button UpgradeButton => _upgradeButton;

    public event Action OnUpgrade = () => { };

    [SerializeField] private UIManager _uiManager;
    [SerializeField] private TextMeshProUGUI _nameField;
    [SerializeField] private TextMeshProUGUI _levelField;
    [SerializeField] private TextMeshProUGUI _upgradeCostField;
    [SerializeField] private TextMeshProUGUI[] _combatField;
    [SerializeField] private Button _upgradeButton;

    private Tower _curTower;

    private void Update()
    {
        _curTower.SelectableComponent.SetOutlineActive(true);
        _curTower.SelectableComponent.SetOutlineWidth(12);
    }

    private void OnDisable()
    {
        _curTower.SelectableComponent.SetOutlineActive(false);
        _curTower.SelectableComponent.SetDefaultOutlineWidth();
    }

    private void SetTowerInfoPanel()
    {
        gameObject.SetActive(true);
        _nameField.text = _curTower.Name;
        _levelField.text = "Current Level: " + _curTower.Level;
        _upgradeCostField.text = "Upgrade Cost: " + _curTower.UpgradeCost;

        foreach (var field in _combatField)
        {
            field.gameObject.SetActive(false);
        }

        if (_curTower is TowerI)
        {
            TowerI towerI = (TowerI)_curTower;

            _combatField[0].text = "Hits per Second: " + towerI.AttacksPerSecond;
            _combatField[0].gameObject.SetActive(true);
            _combatField[1].text = "Hit Damage: " + towerI.AttackDamage;
            _combatField[1].gameObject.SetActive(true);
        }
        else if (_curTower is TowerT)
        {
            TowerT towerT = (TowerT)_curTower;

            _combatField[0].text = "Damage per Second: " + towerT.DamagePerSecond;
            _combatField[0].gameObject.SetActive(true);
        }
        else if (_curTower is TowerY)
        {
            TowerY towerY = (TowerY)_curTower;

            _combatField[0].text = "Slow Amount: " + (towerY.Slow * 100) + "%";
            _combatField[0].gameObject.SetActive(true);
        }

        _upgradeButton.onClick.RemoveAllListeners();
        _upgradeButton.onClick.AddListener(() =>
        {
            OnUpgrade();
            _curTower.IncreaseLevel();           
            SetTowerInfoPanel();
        });
    }

    public void UpdateUpgradeButton(int playerMoney)
    {
        if (_curTower)
        {
            UpgradeButton.interactable = playerMoney >= _curTower.UpgradeCost && _curTower.Level < _curTower.MaxLevel ;
        }
    }
}
