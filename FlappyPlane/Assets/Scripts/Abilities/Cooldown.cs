using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Cooldown : MonoBehaviour
{
    public event Action<float> OnTimeLeftChange;
    public event Action<float> OnTimeLeftPercentageChange;
    public event Action OnBegin;
    public event Action OnReset;

    public float cooldown = 15.0f;
    private float _timeLeft;
    private bool _isCoolingDown;

    private void Update()
    {
        if (_isCoolingDown)
        {
            if (_timeLeft <= 0)
            {
                Reset();
                return;
            }

            DecrementTime();
        }
    }

    public void Begin()
    {
        _timeLeft = cooldown;
        _isCoolingDown = true;
        OnBegin?.Invoke();
    }

    public bool IsCoolingDown()
    {
        return _isCoolingDown;
    }

    private void Reset()
    {
        _isCoolingDown = false;
        _timeLeft = cooldown;
        OnReset?.Invoke();
    }

    private void DecrementTime()
    {
        float percentage = 0.0f;

        _timeLeft -= Time.deltaTime;
        percentage = _timeLeft / cooldown;

        OnTimeLeftChange?.Invoke(_timeLeft);
        OnTimeLeftPercentageChange?.Invoke(percentage);
    }
}