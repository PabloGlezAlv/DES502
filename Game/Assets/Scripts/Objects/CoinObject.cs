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
            case rarity.common:
                amount = 1;
                renderer.color = Color.white;
                break;
            case rarity.uncommon:
                amount = 2;
                renderer.color = Color.green;
                break;
            case rarity.rare:
                amount = 4;
                renderer.color = Color.blue;
                break;
            case rarity.epic:
                amount = 8;
                renderer.color = Color.magenta;
                break;
            case rarity.legendary:
                amount = 16;
                renderer.color = Color.yellow;
                break;
            default:
                Console.WriteLine("Rareza desconocida.");
                break;
        }
    }

    public int getCoins()
    {
        return amount;
    }
}
