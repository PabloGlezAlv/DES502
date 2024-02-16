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
            case rarity.common:
                speedImprovement = 1.04f;
                renderer.color = Color.white;
                break;
            case rarity.uncommon:
                speedImprovement = 1.08f;
                renderer.color = Color.green;
                break;
            case rarity.rare:
                speedImprovement = 1.12f;
                renderer.color = Color.blue;
                break;
            case rarity.epic:
                speedImprovement = 1.16f;
                renderer.color = Color.magenta;
                break;
            case rarity.legendary:
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
