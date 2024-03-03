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
            if (changeFloor == null)
            {
                Debug.LogError("Change Floor component not grabbed");
            }
            else
            {
                playerMovement.SetNormalMove(false);
                changeFloor.enabled = true;
                changeFloor.setTargetPostion(transform.position);
            }
        }
    }
}
