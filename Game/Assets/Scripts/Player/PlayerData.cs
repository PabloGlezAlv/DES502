using System.Collections;
using System.Collections.Generic;
using System.Runtime;
using TMPro;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    [SerializeField]
    TextMeshProUGUI _textMeshPro;

    ItemManager _itemManager;

    PlayerMovement playerMovement;

    [SerializeField]
    GameObject speed;
    [SerializeField]
    GameObject scale;

    int coins = 1000;

    private void Awake()
    {
        _itemManager = GetComponentInChildren<ItemManager>();

        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _textMeshPro.text = coins.ToString();
    }

    public void Reset()
    {
        coins = 0;
    }

    public int getCoins()
    {
        return coins;
    }

    public void addCoins(int amount)
    {
        coins += amount;

        _textMeshPro.text = coins.ToString();
    }

    public bool BuyItem(Items itemType, Rarity rare, int price)
    {
        if(coins < price) 
            return false;
        else
        {
            Debug.Log("Se compra");
            addCoins(-price);

            GameObject newItem = new GameObject();
            GameObject child = new GameObject();
            switch (itemType)
            {
                case Items.SpeedHelmet:
                    //Create the object
                    newItem = Instantiate(speed);
                    child = newItem.GetComponentInChildren<Rigidbody2D>().gameObject;

                    //Give the effect to the player and set rarity
                    SpeedObject speedScript= child.gameObject.GetComponent<SpeedObject>();
                    speedScript.SetRarity(rare);
                    playerMovement.AddSpeed(speedScript.GetUpgrade());

                    
                    newItem.SetActive(false);
                    break;
                case Items.ScaleHelmet:
                    newItem = Instantiate(scale);
                    child = newItem.GetComponentInChildren<Rigidbody2D>().gameObject;

                    //Give the effect to the player and set rarity
                    ScaleObject scaleScript = child.gameObject.GetComponent<ScaleObject>();
                    scaleScript.SetRarity(rare);
                    playerMovement.ReduceScale(scaleScript.GetUpgrade());

                    newItem.SetActive(false);
                    break;
            } 

            //Give the object ot the player
            _itemManager.ChangeItem(child, itemType);

            return true;
        }
    }
}
