using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealObject : BaseObject
{
    int heal = 0;

    protected void Awake()
    {
        base.Awake();

    }

    private void Start()
    {

        List<int> listValues = ObjectsManager.instance.getHealUpgrades();
        List<Sprite> listSprites = ObjectsManager.instance.getHealSprites();

        switch (objectRarity)
        {
            case Rarity.Common:
                renderer.sprite = listSprites[0];
                heal = listValues[0];
                break;
            case Rarity.Uncommon:
                renderer.sprite = listSprites[1];
                heal = listValues[1];
                break;
            case Rarity.Rare:
                renderer.sprite = listSprites[2];
                heal = listValues[2];
                break;
            case Rarity.Epic:
                renderer.sprite = listSprites[3];
                heal = listValues[3];
                break;
            case Rarity.Legendary:
                renderer.sprite = listSprites[4];
                heal = listValues[4];
                break;
            default:
                Console.WriteLine("Rareza desconocida.");
                break;
        }
    }

    public int GetHeal()
    {
        return heal;
    }
}
