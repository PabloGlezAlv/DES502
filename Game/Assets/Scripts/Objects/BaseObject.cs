using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity{Common, Uncommon, Rare, Epic, Legendary }

public class BaseObject : MonoBehaviour
{
    [SerializeField]
    protected float commonProbability = 0.3f;
    [SerializeField]
    protected float uncommonProbability = 0.25f;
    [SerializeField]
    protected float rareProbability = 0.2f;
    [SerializeField]
    protected float epicProbability = 0.15f;
    [SerializeField]
    protected float legendaryProbability = 0.1f;

    protected SpriteRenderer renderer;

    protected Rarity objectRarity;
    protected void Awake()
    {
        renderer = gameObject.GetComponent<SpriteRenderer>();
        objectRarity = GenerateRandomRarity();


    }

    private Rarity GenerateRandomRarity()
    {
        float randomValue = Random.value;

        if (randomValue < commonProbability)
        {
            return Rarity.Common;
        }
        else if (randomValue < commonProbability + uncommonProbability)
        {
            return Rarity.Uncommon;
        }
        else if (randomValue < commonProbability + uncommonProbability + rareProbability)
        {
            return Rarity.Rare;
        }
        else if (randomValue < commonProbability + uncommonProbability + rareProbability + epicProbability)
        {
            return Rarity.Epic;
        }
        else
        {
            return Rarity.Legendary;
        }
    }

}
