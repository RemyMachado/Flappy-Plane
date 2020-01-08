using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// bethink about an eventual method: Notify() -> that would trigger all events to synchronize subscribers
public class Quantity : MonoBehaviour
{
    public event Action<uint> OnQuantityChange;
    public event Action<bool> OnIsUnlimitedChange; 
    [SerializeField] private uint _value;
    private bool _isUnlimited;

    public bool IsUnlimited
    {
        get => _isUnlimited;
        private set
        {
            _isUnlimited = value;
            OnIsUnlimitedChange?.Invoke(value);
        }
    }

    public uint Value
    {
        get => _value;
        private set
        {
            if (!IsUnlimited)
            {
                _value = value;
                OnQuantityChange?.Invoke(_value);
            }
        }
    }

    public void SetIsUnlimited(bool value)
    {
        IsUnlimited = value;
    }
    
    public void SetQuantity(uint value)
    {
        Value = value;
    }
    
    public uint AddQuantity(uint value)
    {
        return Value += value;
    }

    public uint SubtractQuantity(uint value)
    {
        // Prevent unsigned Overflow
        if (value > Value)
        {
            value = Value;
        }
        return Value -= value;
    }

    public bool IsEmpty()
    {
        return Value == 0;
    }
}