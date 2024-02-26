using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeLevel : MonoBehaviour
{
    [SerializeField]
    CameraMovement cameraMov;

    [SerializeField]
    RoomCreator creator;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        PlayerMovement playerMovement = collision.gameObject.GetComponent<PlayerMovement>();
        if (playerMovement != null)
        {
            Debug.Log("Siguiente nivel");

            cameraMov.SetPosition(new Vector2Int());
            playerMovement.ResetPlayer();

            creator.generateNewLevel();
        }
    }
}
