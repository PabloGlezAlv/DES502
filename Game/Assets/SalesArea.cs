using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalesArea : MonoBehaviour
{
    [SerializeField]
    GameObject shopUI;

    ShopController shopController;

    private void Awake()
    {
        shopController = shopUI.GetComponent<ShopController>();
    }

    private void Start()
    {
       
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
