using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum rarity{common, uncommon, rare, epic, legendary }

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


    protected rarity objectRarity;
    protected void Start()
    {
        objectRarity = GenerateRandomRarity();
    }
    private rarity GenerateRandomRarity()
    {
        float randomValue = Random.value;

        if (randomValue < commonProbability)
        {
            return rarity.common;
        }
        else if (randomValue < commonProbability + uncommonProbability)
        {
            return rarity.uncommon;
        }
        else if (randomValue < commonProbability + uncommonProbability + rareProbability)
        {
            return rarity.rare;
        }
        else if (randomValue < commonProbability + uncommonProbability + rareProbability + epicProbability)
        {
            return rarity.epic;
        }
        else
        {
            return rarity.legendary;
        }
    }

}
