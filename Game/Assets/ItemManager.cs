using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    [SerializeField]
    private Sprite speedObject;

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
            playerObject.SetActive(true);
            playerObject.transform.position = gameObject.transform.position;
        }

        playerObject = newItem;
        playerItem = type;

        //Change the sprite to the one of the new item
        
        switch (playerItem)
        {
            case items.none:
                break;
            case items.speedHelmet:
                spriteRenderer.sprite = speedObject;
                break;
            default:
                break;

        }
    }
}
