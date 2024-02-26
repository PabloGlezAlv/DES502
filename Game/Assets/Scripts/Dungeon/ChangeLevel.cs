using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            ChangeFloorMovement changeFloor = collision.gameObject.GetComponent<ChangeFloorMovement>();

            playerMovement.enabled = false;
            changeFloor.enabled = true;
        }
    }
}
