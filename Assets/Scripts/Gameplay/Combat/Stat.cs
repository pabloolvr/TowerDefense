using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum StatModifierType
{
    AbsolutValue,
    RelativeValueAdditive,
    RelativeValueNonAdditive,
}

[Serializable]
public struct StatModifier
{
    public StatModifierType Type => _type;
    public float Value
    {
        get { return _value; }
        set { _value = value; }
    }

    [SerializeField] private StatModifierType _type;
    [SerializeField] private float _value;

    public StatModifier(StatModifierType type, float value)
    {
        _type = type;
        _value = value;
    }
}

[Serializable]
public class Stat
{
    public float BaseValue => _baseValue;
    public float Value
    {
        get
        {
            float addedAbsolutValue = 0;
            float addedRelativeValue = 0;
            float relativeValueNonAdditive = 0;

            foreach (StatModifier modifier in _modifiers)
            {
                switch (modifier.Type)
                {
                    case StatModifierType.AbsolutValue:
                        addedAbsolutValue += modifier.Value;
                        break;
                    case StatModifierType.RelativeValueAdditive:
                        addedRelativeValue += modifier.Value;
                        break;
                    case StatModifierType.RelativeValueNonAdditive:
                        relativeValueNonAdditive = modifier.Value;
                        break;
                }
            }

            return (_baseValue + addedAbsolutValue) * (1 + relativeValueNonAdditive + addedRelativeValue);
        }
    }

    public event Action OnAddModifier = () => { };
    public event Action OnRemoveModifier = () => { };

    [SerializeField] private float _baseValue;
    [SerializeField] private List<StatModifier> _modifiers = new List<StatModifier>();

    public void AddModifier(StatModifier modifier)
    {
        _modifiers.Add(modifier);
        OnAddModifier();
    }

    public void RemoveModifier(StatModifier modifier)
    {
        _modifiers.Remove(modifier);
        OnRemoveModifier();
    }

    public void RemoveAllModifiers()
    {
        _modifiers.Clear();
    }
}
