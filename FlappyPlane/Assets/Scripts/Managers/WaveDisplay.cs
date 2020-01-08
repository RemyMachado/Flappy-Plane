using System;
using System.Collections;
using System.Collections.Generic;
using Managers;
using TMPro;
using UnityEngine;

public class WaveDisplay : MonoBehaviour
{
    private WaveManager _waveManager;
    public TextMeshProUGUI waveText;
    public string prefix = "Wave #";
    private void Start()
    {
        _waveManager = GetComponent<WaveManager>();
        _waveManager.OnNewWave += UpdateDisplay;
    }

    private void UpdateDisplay(uint wave)
    {
        waveText.text = prefix + wave.ToString();
    }
}