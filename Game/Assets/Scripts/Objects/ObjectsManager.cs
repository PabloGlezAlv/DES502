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
    List<string> speedDescription = new List<string>();

    [SerializeField]
    List<Sprite> scaleSprites;
    [SerializeField]
    List<float> scaleUpgrade; 
    [SerializeField]
    List<int> scaleShopPrice;
    List<string> scaleDescription = new List<string>();

    [SerializeField]
    List<Sprite> healSprites;
    [SerializeField]
    List<int> healUpgrade;
    [SerializeField]
    List<int> healShopPrice;
    List<string> healDescription = new List<string>();

    [SerializeField]
    List<Sprite> damageSprites;
    [SerializeField]
    List<int> damageUpgrade;
    [SerializeField]
    List<int> damageShopPrice;
    List<string> damageDescription = new List<string>();

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

        //Items descriptions (Every Item  have 5 1 each rarity the first one is the worst of them)
        speedDescription.Add("speed description 1");
        speedDescription.Add("speed description 2");
        speedDescription.Add("speed description 2");
        speedDescription.Add("speed description 2");
        speedDescription.Add("speed description 2");


        scaleDescription.Add("scale description 2");
        scaleDescription.Add("scale description 2");
        scaleDescription.Add("scale description 2");
        scaleDescription.Add("scale description 2");
        scaleDescription.Add("scale description 2");

        healDescription.Add("heal description 2");
        healDescription.Add("heal description 2");
        healDescription.Add("heal description 2");
        healDescription.Add("heal description 2");
        healDescription.Add("heal description 2");

        damageDescription.Add("damage description 2");
        damageDescription.Add("damage description 2");
        damageDescription.Add("damage description 2");
        damageDescription.Add("damage description 2");
        damageDescription.Add("damage description 2");

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

    public string getSpeedDescription(int i)
    {
        return speedDescription[i];
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

    public string getScaleDescription(int i)
    {
        return scaleDescription[i];
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

    public string getDamageDescription(int i)
    {
        return damageDescription[i];
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

    public string getHealDescription(int i)
    {
        return healDescription[i];
    }
}
