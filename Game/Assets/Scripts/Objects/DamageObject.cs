using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageObject : BaseObject
{
    int damageUpgrade = 0;

    protected void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        SetRarity(objectRarity);
    }

    public void SetRarity(Rarity rare)
    {
        objectRarity = rare;

        List<int> listValues = ObjectsManager.instance.getDamageUpgrades();
        List<Sprite> listSprites = ObjectsManager.instance.getDamageSprites();

        switch (objectRarity)
        {
            case Rarity.Common:
                renderer.sprite = listSprites[0];
                damageUpgrade = listValues[0];
                break;
            case Rarity.Uncommon:
                renderer.sprite = listSprites[1];
                damageUpgrade = listValues[1];
                break;
            case Rarity.Rare:
                renderer.sprite = listSprites[2];
                damageUpgrade = listValues[2];
                break;
            case Rarity.Epic:
                renderer.sprite = listSprites[3];
                damageUpgrade = listValues[3];
                break;
            case Rarity.Legendary:
                renderer.sprite = listSprites[4];
                damageUpgrade = listValues[4];
                break;
            default:
                Console.WriteLine("Rareza desconocida.");
                break;
        }
    }
    public int GetUpgrade()
    {
        return damageUpgrade;
    }
}
