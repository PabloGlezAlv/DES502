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
        HealObject heal;
        DamageObject damage;
        if (coin = collision.gameObject.GetComponent<CoinObject>())
        {
            player.GetComponent<PlayerData>().addCoins(coin.GetCoins());

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(speed = collision.gameObject.GetComponent<SpeedObject>())
        {
            AudioManager.instance.Play("ItemGrabbed");
            //Add new Effect
            myItem = Items.SpeedHelmet;
            player.GetComponent<PlayerMovement>().AddSpeed(speed.GetUpgrade());
            manager.ChangeItem(collision.gameObject, myItem);

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(scale = collision.gameObject.GetComponent<ScaleObject>())
        {
            AudioManager.instance.Play("ItemGrabbed");
            //Add new Effect
            myItem = Items.ScaleHelmet;
            player.GetComponent<PlayerMovement>().ReduceScale(scale.GetUpgrade());
            manager.ChangeItem(collision.gameObject, myItem);

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
        else if(heal = collision.gameObject.GetComponent<HealObject>())
        {
            AudioManager.instance.Play("ItemGrabbed");
            player.GetComponent<LifeSystem>().GetHealed(heal.GetHeal());
            Debug.Log("Healed");
            Destroy(collision.transform.parent.gameObject); 
        }
        else if(damage = collision.gameObject.GetComponent<DamageObject>())
        {
            //Add new Effect
            AudioManager.instance.Play("ItemGrabbed");
            myItem = Items.ScaleHelmet;
            player.GetComponent<PlayerMovement>().AddDamage(damage.GetUpgrade());
            manager.ChangeItem(collision.gameObject, myItem);

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
    }
}
