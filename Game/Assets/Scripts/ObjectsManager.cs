using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum Items { none, ScaleHelmet, SpeedHelmet } //Leave SpeedHelmet last


public class ObjectsManager : MonoBehaviour
{
    [SerializeField]
    List<Sprite> coinsSprites;
    [SerializeField]
    List<int> coinsValue;

    [SerializeField]
    List<Sprite> speedSprites;
    [SerializeField]
    List<float> speedUpgrade;

    [SerializeField]
    List<Sprite> scaleSprites;
    [SerializeField]
    List<float> scaleUpgrade;

    public static ObjectsManager instance;

    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }
    //--------------------COINS----------------------------------
    public Sprite getCoinSprite(int i)
    {
        return coinsSprites[i];
    }
    public List<Sprite> getCoinSprites()
    {
        return coinsSprites;
    }
    public int getCoinAmount(int i)
    {
        return coinsValue[i];
    }

    public List<int> getCoinAmounts()
    {
        return coinsValue;
    }

    //---------------------SPEED OBJECT---------------------------
    public Sprite getSpeedSprite(int i)
    {
        return speedSprites[i];
    }
    public List<Sprite> getSpeedSprites()
    {
        return speedSprites;
    }
    public float getSpeedUpgrade(int i)
    {
        return speedUpgrade[i];
    }

    public List<float> getSpeedUpgrades()
    {
        return speedUpgrade;
    }

    //-------------------------------SCALE OBJECT----------------------------
    public Sprite getScaleSprite(int i)
    {
        return scaleSprites[i];
    }
    public List<Sprite> getScaleSprites()
    {
        return scaleSprites;
    }
    public float getScaleUpgrade(int i)
    {
        return scaleUpgrade[i];
    }

    public List<float> getScaleUpgrades()
    {
        return scaleUpgrade;
    }
}