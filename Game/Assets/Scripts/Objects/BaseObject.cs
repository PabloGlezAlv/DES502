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
    [SerializeField]
    protected Sprite commonSprite;
    [SerializeField]
    protected Sprite uncommonSprite;
    [SerializeField]
    protected Sprite rareSprite;
    [SerializeField]
    protected Sprite epicSprite;
    [SerializeField]
    protected Sprite legendarySprite;

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
            renderer.sprite = commonSprite;
            return Rarity.Common;
        }
        else if (randomValue < commonProbability + uncommonProbability)
        {
            renderer.sprite = uncommonSprite;
            return Rarity.Uncommon;
        }
        else if (randomValue < commonProbability + uncommonProbability + rareProbability)
        {
            renderer.sprite = rareSprite;
            return Rarity.Rare;
        }
        else if (randomValue < commonProbability + uncommonProbability + rareProbability + epicProbability)
        {
            renderer.sprite = epicSprite;
            return Rarity.Epic;
        }
        else
        {
            renderer.sprite = legendarySprite;
            return Rarity.Legendary;
        }
    }

}
