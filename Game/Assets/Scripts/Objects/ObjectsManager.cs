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
        speedDescription.Add("A shimmering golden winged helm. Enchanted with a special ability that slightly boosts your movement speed. ");
        speedDescription.Add("A glistening golden winged helm. Enchanted with a special ability that noticeably boosts your movement speed. ");
        speedDescription.Add("A gleaming golden winged helm. Enchanted with a special ability that considerably boosts your movement speed. ");
        speedDescription.Add("A beautiful golden winged helm. Enchanted with a special ability that dramatically boosts your movement speed. ");
        speedDescription.Add("An ethereal golden winged helm. Enchanted with a special ability that tremendously boosts your movement speed. ");


        scaleDescription.Add("A tall, blue ordinary looking hat. Enchanted with a special ability that boosts your size by a small amount. ");
        scaleDescription.Add("A tall, blue odd hat. Enchanted with a special ability that boosts your size by a decent amount. ");
        scaleDescription.Add("A tall, blue strange hat. Enchanted with a special ability that boosts your size by a good amount. ");
        scaleDescription.Add("A very tall, blue bizarre hat. Enchanted with a special ability that boosts your size by a large amount. ");
        scaleDescription.Add("An incredibly tall, blue mountainous hat. Enchanted with a special ability that boosts your size by a massive amount. ");

        healDescription.Add("A small glass bottle filled up with a weak healing potion. Will raises your health by a small amount when consumed. ");
        healDescription.Add("A small glass bottle filled up with a modest healing potion. Will raises your health by a fair amount when consumed. ");
        healDescription.Add("A medium-sized glass bottle filled up with a strong healing potion. Will raises your health by a humble amount when consumed. ");
        healDescription.Add("A medium-sized bottle filled up with a powerful healing potion. Will raises your health by a fantastic amount when consumed. ");
        healDescription.Add("A large glass bottle filled up with a masterful healing potion. Will raises your health by an insane amount when consumed. ");

        damageDescription.Add("A leather and iron horned helmet. Enchanted with a special ability that raises your attack damage. ");
        damageDescription.Add("A leather and steel horned helmet. Enchanted with a special ability that fairly improves your attack damage. ");
        damageDescription.Add("A leather and tungsten horned helmet. Enchanted with a special ability that greatly enhances your attack damage. ");
        damageDescription.Add("A leather and chromium horned helmet. Enchanted with a special ability that superbly reinforces your attack damage. ");
        damageDescription.Add("A leather and titanium horned helmet. Enchanted with a special ability that skyrockets your attack damage. ");

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
