using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesArea : MonoBehaviour
{
    ShopController shopController;

    private void Awake()
    {
        shopController = GameObject.Find("UI_Shop").GetComponent<ShopController>();
    }

    public void setShopPosition(Vector3 position)
    {
        transform.position = position;

        shopController.CreateNewShop();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null)
        {
            shopController.Show();
        }     
    }


    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.GetComponent<PlayerMovement>() != null)
        {
            shopController.Hide();
        }
    }
}
