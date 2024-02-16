using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

enum items { none, speedHelmet}

public class CheckItems : MonoBehaviour
{
    private GameObject player;

    items myItem = items.none;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinObject coin;
        SpeedObject speed;
        if (coin = collision.gameObject.GetComponent<CoinObject>())
        {
            Debug.Log("New coin");

            player.GetComponent<PlayerData>().addCoins(coin.getCoins());

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(speed = collision.gameObject.GetComponent<SpeedObject>())
        {
            Debug.Log("Speed Object");
            //Remove Actual effect
            removeActualEffect(collision.gameObject);

            //Add new Effect
            myItem = items.speedHelmet;
            player.GetComponent<PlayerMovement>().AddSpeed(speed.GetUpgrade());

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
    }

    private void removeActualEffect(GameObject collision)
    {
        switch (myItem)
        {
            case items.none:
                break;
            case items.speedHelmet:
                collision.GetComponent<PlayerMovement>().ResetSpeed();
                break;
             default: 
                break;
                
        }
    }
}
