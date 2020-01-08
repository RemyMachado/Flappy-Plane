using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    public TextMeshProUGUI text;
    
    public static int money;
    public int startMoney = 400;

    public static int lives;
    public int startLives = 1;

    public static int energy;
    public int startEnergy = 100;

    public void Start()
    {
        money = startMoney;
        lives = startLives;
        energy = startEnergy;
    }

    public void Update()
    {
        text.text = "$" + money.ToString();
    }
}
