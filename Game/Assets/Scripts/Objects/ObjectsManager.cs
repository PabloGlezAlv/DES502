using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public enum Items { none, heal, ScaleHelmet,DamageHelmet ,SpeedHelmet } //Leave SCALEHELMET FIRST SpeedHelmet LAST


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
    List<int> speedShopPrice;

    [SerializeField]
    List<Sprite> scaleSprites;
    [SerializeField]
    List<float> scaleUpgrade; 
    [SerializeField]
    List<int> scaleShopPrice;

    [SerializeField]
    List<Sprite> healSprites;
    [SerializeField]
    List<int> healUpgrade;
    [SerializeField]
    List<int> healShopPrice;

    [SerializeField]
    List<Sprite> damageSprites;
    [SerializeField]
    List<int> damageUpgrade;
    [SerializeField]
    List<int> damageShopPrice;

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
    public int getSpeedPrice(int i)
    {
        return speedShopPrice[i];
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
    public int getScalePrice(int i)
    {
        return scaleShopPrice[i];
    }
    //-------------------------------DAMAGE OBJECT----------------------------
    public Sprite getDamageSprite(int i)
    {
        return damageSprites[i];
    }
    public List<Sprite> getDamageSprites()
    {
        return damageSprites;
    }
    public int getDamageUpgrade(int i)
    {
        return damageUpgrade[i];
    }
    public List<int> getDamageUpgrades()
    {
        return damageUpgrade;
    }
    public int getDamagePrice(int i)
    {
        return damageShopPrice[i];
    }


    //-------------------------------HEAL OBJECT----------------------------

    public Sprite getHealSprite(int i)
    {
        return healSprites[i];
    }
    public List<Sprite> getHealSprites()
    {
        return healSprites;
    }
    public int getHealUpgrade(int i)
    {
        return healUpgrade[i];
    }
    public List<int> getHealUpgrades()
    {
        return healUpgrade;
    }
    public int getHealPrice(int i)
    {
        return healShopPrice[i];
    }
}
