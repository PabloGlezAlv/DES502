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

    }

    private void Start()
    {

        List<int> listValues = ObjectsManager.instance.getCoinAmounts();
        List<Sprite> listSprites = ObjectsManager.instance.getCoinSprites();

        switch (objectRarity)
        {
            case Rarity.Common:
                renderer.sprite = listSprites[0];
                amount = listValues[0];
                break;
            case Rarity.Uncommon:
                renderer.sprite = listSprites[1];
                amount = listValues[1];
                break;
            case Rarity.Rare:
                renderer.sprite = listSprites[2];
                amount = listValues[2];
                break;
            case Rarity.Epic:
                renderer.sprite = listSprites[3];
                amount = listValues[3];
                break;
            case Rarity.Legendary:
                renderer.sprite = listSprites[4];
                amount = listValues[4];
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
