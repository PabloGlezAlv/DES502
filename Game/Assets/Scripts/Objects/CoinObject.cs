using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinObject : BaseObject
{
    int amount = 0;

    SpriteRenderer renderer;
    void Start()
    {
        base.Start();

        renderer = gameObject.GetComponent<SpriteRenderer>();   

        switch (objectRarity)
        {
            case Rarity.Common:
                amount = 1;
                renderer.color = Color.white;
                break;
            case Rarity.Uncommon:
                amount = 2;
                renderer.color = Color.green;
                break;
            case Rarity.Rare:
                amount = 4;
                renderer.color = Color.blue;
                break;
            case Rarity.Epic:
                amount = 8;
                renderer.color = Color.magenta;
                break;
            case Rarity.Legendary:
                amount = 16;
                renderer.color = Color.yellow;
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
