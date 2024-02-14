using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _textMeshPro;


    int coins = 0;


    public int getCoins()
    {
        return coins;
    }

    public void addCoins(int amount)
    {
        coins += amount;

        _textMeshPro.text = coins.ToString();
    }
}
