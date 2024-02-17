using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;


public class CheckItems : MonoBehaviour
{
    [SerializeField]
    ItemManager manager;

    private GameObject player;

    Items myItem = Items.none;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinObject coin;
        SpeedObject speed;
        ScaleObject scale;
        if (coin = collision.gameObject.GetComponent<CoinObject>())
        {

            player.GetComponent<PlayerData>().addCoins(coin.GetCoins());

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(speed = collision.gameObject.GetComponent<SpeedObject>())
        {
            //Remove Actual effect
            removeActualEffect(collision.gameObject);

            //Add new Effect
            myItem = Items.SpeedHelmet;
            player.GetComponent<PlayerMovement>().AddSpeed(speed.GetUpgrade());
            manager.ChangeItem(collision.gameObject, myItem);

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(scale = collision.gameObject.GetComponent<ScaleObject>())
        {
            //Remove Actual effect
            removeActualEffect(collision.gameObject);
            //Add new Effect
            myItem = Items.ScaleHelmet;
            player.GetComponent<PlayerMovement>().ReduceScale(scale.GetUpgrade());
            manager.ChangeItem(collision.gameObject, myItem);

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
    }

    private void removeActualEffect(GameObject collision)
    {
        switch (myItem)
        {
            case Items.none:
                break;
            case Items.SpeedHelmet:
                transform.parent.gameObject.GetComponent<PlayerMovement>().ResetSpeed();
                break;
            case Items.ScaleHelmet:
                transform.parent.gameObject.GetComponent<PlayerMovement>().ResetScale();
                break;
             default: 
                break;
                
        }
    }
}
