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

            playerMovement.SetNormalMove(false);
            changeFloor.enabled = true;
            Vector3 position= new Vector3(transform.position.x + 0.5f, transform.position.y, transform.position.z);
            changeFloor.setTargetPostion(position);
        }
    }
}