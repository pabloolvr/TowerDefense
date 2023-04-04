using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Networking.UnityWebRequest;

/// <summary>
/// Every Tower in the game apply effects to enemies in its range.
/// </summary>
public abstract class Tower : MonoBehaviour
{
    public static readonly int MaxUpgradeCost = 20000;

    public bool IsBeingPlaced
    {
        get
        {
            return _isBeingPlaced;
        } 
        set
        {
            _isBeingPlaced = value;

            _towerBase.GetComponent<Collider>().enabled = !_isBeingPlaced;

            if (!_isBeingPlaced)
            {
                _materialPicker.SetDefaultMaterials();
                Invoke(nameof(ActivateSelectableComponent), .2f);
            }
        }
    }
    public bool IsTouchingOtherTowers => _towersCollisions.Length > 0;
    public int Level => _level;
    public int MaxLevel => _maxLevel;
    public int BasePrice => _basePrice;
    public string Name => _name;
    public string Description => _description;
    public int UpgradeCost => (int)(_baseUpgradeCost * Mathf.Max(1, _upgradeCostGrowth * (Level)));
    public SelectableObject SelectableComponent => _selectableComponent;

    [Header("References")]
    [SerializeField] protected EnemyDetector _enemyDetector;
    [SerializeField] protected Transform _towerBase;

    [Header("Stats")]
    [SerializeField] protected string _name;
    [SerializeField] protected string _description;
    [SerializeField] protected int _basePrice;
    [SerializeField] protected int _baseUpgradeCost;
    [SerializeField] protected int _maxLevel;
    [SerializeField, Range(0f, 2f)] protected float _upgradeCostGrowth;
    [SerializeField] protected bool _isMultiTarget;

    protected MeshRenderer _towerActionRange;
    protected SelectableObject _selectableComponent;
    protected TowerMaterialPicker _materialPicker;
    protected float _lastAppliedEffectTime;
    protected int _level;

    private Vector3 _towerColliderSize;
    private Collider[] _towersCollisions;
    private int _towerLayerMask;
    private bool _isBeingPlaced;

    protected virtual void Start()
    {
        _selectableComponent = GetComponent<SelectableObject>();
        _maxLevel = (_maxLevel <= 0)? int.MaxValue : _maxLevel;
        _towerActionRange = _enemyDetector.GetComponent<MeshRenderer>();
        _materialPicker = GetComponent<TowerMaterialPicker>();
        _towerLayerMask = 1 << 6;
        _towerColliderSize = new Vector3(_towerBase.lossyScale.x, 1 , _towerBase.lossyScale.z);
        _lastAppliedEffectTime = 0;
        _level = 0;
    }

    protected virtual void Update()
    {
        if (IsBeingPlaced)
        {
            _towersCollisions = Physics.OverlapBox(_towerBase.position, _towerColliderSize / 2, _towerBase.rotation, _towerLayerMask);

            if (IsTouchingOtherTowers)
            {
                _materialPicker.SetRedMaterial();
            }
            else
            {
                _materialPicker.SetGreenMaterial();
            }
        }
    }

    public void ShowTowerRange(bool show)
    {
        _towerActionRange.enabled = show;
    }

    protected abstract void ApplyEffect(Enemy enemy);
    public virtual void IncreaseLevel()
    {
        if (_level < _maxLevel)
        {
            _level++;
        }
    }

    /// <summary>
    /// Base effect applicator that should run every frame.
    /// </summary>
    /// <param name="effectApplicationTimer">The interval of time at which the effect is applied.</param>
    /// <param name="inArea">If the effect is applied to a single or multiple enemies.</param>
    protected void EffectApplicator(float effectApplicationTimer, bool inArea)
    {
        if (Time.fixedTime - _lastAppliedEffectTime >= effectApplicationTimer)
        {
            List<int> deadEnemies = new List<int>();

            for (int i = 0; i < _enemyDetector.DetectedEnemies.Count; i++)
            {
                if (!_enemyDetector.DetectedEnemies[i].gameObject.activeSelf)
                {
                    deadEnemies.Add(i);
                }
                else
                {
                    _lastAppliedEffectTime = Time.fixedTime;
                    ApplyEffect(_enemyDetector.DetectedEnemies[i]);

                    if (!inArea)
                    {
                        break;
                    }
                }
            }

            _enemyDetector.UpdateDetectedEnemies(deadEnemies);
        }
    }
    private void ActivateSelectableComponent()
    {
        _selectableComponent.enabled = true;
    }
    /*
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(_towerBase.position, _towerColliderSize);
    }
    */
}
