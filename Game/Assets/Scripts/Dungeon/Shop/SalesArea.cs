using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesArea : MonoBehaviour
{
    ShopController shopController;

    bool friendly = true;

    private void Awake()
    {
        shopController = GameObject.Find("UI_Shop").GetComponent<ShopController>();
    }

    public void setHostile()
    {
        friendly = false;

        shopController.Hide();
    }

    public void setShopPosition(Vector3 position)
    {
        transform.parent.GetComponent<SalesmanCollision>().ChangePosition(position);
        friendly = true;
        shopController.CreateNewShop();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerMovement>() != null && friendly)
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
