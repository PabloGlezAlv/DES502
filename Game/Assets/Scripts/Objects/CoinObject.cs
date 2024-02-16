using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : BaseObject
{
    int amount = 0;

    protected void Awake()
    {
        base.Awake();



        switch (objectRarity)
        {
            case Rarity.Common:
                amount = 1;
                break;
            case Rarity.Uncommon:
                amount = 2;
                break;
            case Rarity.Rare:
                amount = 4;
                break;
            case Rarity.Epic:
                amount = 8;
                break;
            case Rarity.Legendary:
                amount = 16;
                break;
            default:
                Console.WriteLine("Rareza desconocida.");
                break;
        }
    }

    public int GetCoins()
    {
        return amount;
    }
}
