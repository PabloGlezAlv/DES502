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


        switch (objectRarity)
        {
            case Rarity.Common:
                speedImprovement = 1.04f;
                break;
            case Rarity.Uncommon:
                speedImprovement = 1.08f;
                break;
            case Rarity.Rare:
                speedImprovement = 1.12f;
                break;
            case Rarity.Epic:
                speedImprovement = 1.16f;
                break;
            case Rarity.Legendary:
                speedImprovement = 1.20f;
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
