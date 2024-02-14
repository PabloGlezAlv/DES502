using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckItems : MonoBehaviour
{
    private GameObject player;

    private void Start()
    {
        player = transform.parent.gameObject;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        CoinObject obj;
        if (obj = collision.gameObject.GetComponent<CoinObject>())
        {
            Debug.Log("New coin");

            player.GetComponent<PlayerData>().addCoins(obj.getCoins());

            //Deactivate
            collision.transform.parent.gameObject.SetActive(false);
        }
    }
}
