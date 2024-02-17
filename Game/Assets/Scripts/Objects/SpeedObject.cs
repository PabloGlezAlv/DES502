using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedObject : BaseObject
{
    float speedImprovement = 0;

    protected void Awake()
    {
        base.Awake();

    }

    private void Start()
    {
        List<float> listValues = ObjectsManager.instance.getSpeedUpgrades();
        List<Sprite> listSprites = ObjectsManager.instance.getSpeedSprites();

        switch (objectRarity)
        {
            case Rarity.Common:
                renderer.sprite = listSprites[0];
                speedImprovement = listValues[0];
                break;
            case Rarity.Uncommon:
                renderer.sprite = listSprites[1];
                speedImprovement = listValues[1];
                break;
            case Rarity.Rare:
                renderer.sprite = listSprites[2];
                speedImprovement = listValues[2];
                break;
            case Rarity.Epic:
                renderer.sprite = listSprites[3];
                speedImprovement = listValues[3];
                break;
            case Rarity.Legendary:
                renderer.sprite = listSprites[4];
                speedImprovement = listValues[4];
                break;
            default:
                Console.WriteLine("Rareza desconocida.");
                break;
        }
    }
    public float GetUpgrade()
    {
        return speedImprovement;
    }
}
