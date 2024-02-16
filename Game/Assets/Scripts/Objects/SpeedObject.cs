using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedObject : BaseObject
{
    float speedImprovement = 0;

    SpriteRenderer renderer;
    void Start()
    {
        base.Start();

        renderer = gameObject.GetComponent<SpriteRenderer>();

        switch (objectRarity)
        {
            case Rarity.Common:
                speedImprovement = 1.04f;
                renderer.color = Color.white;
                break;
            case Rarity.Uncommon:
                speedImprovement = 1.08f;
                renderer.color = Color.green;
                break;
            case Rarity.Rare:
                speedImprovement = 1.12f;
                renderer.color = Color.blue;
                break;
            case Rarity.Epic:
                speedImprovement = 1.16f;
                renderer.color = Color.magenta;
                break;
            case Rarity.Legendary:
                speedImprovement = 1.20f;
                renderer.color = Color.yellow;
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
