using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    items playerItem = items.none;

    GameObject playerObject;

    SpriteRenderer spriteRenderer;
    private void Start()
    {
        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
    }

    public void ChangeItem(GameObject newItem, items type)
    {
        //Remove old item
        if(playerItem != items.none)
        {
            //playerObject.SetActive(true);
            //playerObject.transform.position = new Vector3(gameObject.transform.position.x - 7, gameObject.transform.position.y, gameObject.transform.position.z);

            //Move it

            
        }

        playerObject = newItem;
        playerItem = type;

        //Change the sprite to the one of the new item
        
        switch (playerItem)
        {
            case items.none:
                break;
            case items.speedHelmet:
                spriteRenderer.sprite = playerObject.GetComponent<SpriteRenderer>().sprite;
                break;
            default:
                break;

        }
    }
}
