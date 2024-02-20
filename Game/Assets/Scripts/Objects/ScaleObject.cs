using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleObject : BaseObject
{
    float reducedScale = 0;

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

        List<float> listValues = ObjectsManager.instance.getScaleUpgrades();
        List<Sprite> listSprites = ObjectsManager.instance.getScaleSprites();

        switch (objectRarity)
        {
            case Rarity.Common:
                renderer.sprite = listSprites[0];
                reducedScale = listValues[0];
                break;
            case Rarity.Uncommon:
                renderer.sprite = listSprites[1];
                reducedScale = listValues[1];
                break;
            case Rarity.Rare:
                renderer.sprite = listSprites[2];
                reducedScale = listValues[2];
                break;
            case Rarity.Epic:
                renderer.sprite = listSprites[3];
                reducedScale = listValues[3];
                break;
            case Rarity.Legendary:
                renderer.sprite = listSprites[4];
                reducedScale = listValues[4];
                break;
            default:
                Console.WriteLine("Rareza desconocida.");
                break;
        }
    }
    public float GetUpgrade()
    {
        return reducedScale;
    }
}
