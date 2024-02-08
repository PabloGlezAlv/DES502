using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField]
    private int width = 22;
    [SerializeField]
    private int height = 10;

    private CameraMovement cam;

    private void Start()
    {
        cam = Camera.main.gameObject.GetComponent<CameraMovement>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement mov;

        if (collision.gameObject.TryGetComponent(out mov))
        {
            Vector2Int center = mov.GetCurrentRoom();

            direction dir = mov.GetCurrentDirection();

            Vector2Int finalPositon = new Vector2Int();

            switch (dir)
            {
                case direction.none:
                    Debug.Log("Error direction none");
                    break;
                case direction.top:
                    finalPositon = new Vector2Int(center.x, center.y + height + 3);
                    break;
                case direction.bottom:
                    finalPositon = new Vector2Int(center.x, center.y - height - 3);
                    break;
                case direction.left:
                    finalPositon = new Vector2Int(center.x - width - 2, center.y);
                    break;
                case direction.right:
                    finalPositon = new Vector2Int(center.x + width + 2, center.y);
                    break;
                default:
                    Debug.Log("No direction error");
                    break;
            }


            if (cam.SetPosition(finalPositon))
            {
                Debug.Log("NEW CENTER " + finalPositon);
                mov.SetDoorMovement(finalPositon);
            }
        }
    }
}
