using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeRoom : MonoBehaviour
{
    [SerializeField]
    private int width = 22;
    [SerializeField]
    private int height = 10;

    private CameraMovement cam;

    List<doorsInfo> doors;

    private void Start()
    {
        cam = Camera.main.gameObject.GetComponent<CameraMovement>();
    }

    public void SaveDoors(List<doorsInfo> d)
    {
        doors = d;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PlayerMovement mov;

        if (collision.gameObject.TryGetComponent(out mov))
        {
            Vector2Int center = mov.GetCurrentRoom();

            Direction dir = GetCloserDoorDirection(new Vector2Int((int)collision.transform.position.x, (int)collision.transform.position.y));

            Vector2Int finalPositon = new();

            switch (dir)
            {
                case Direction.None:
                    Debug.Log("Error direction none");
                    break;
                case Direction.Top:
                    finalPositon = new Vector2Int(center.x, center.y + height + 4);
                    break;
                case Direction.Bottom:
                    finalPositon = new Vector2Int(center.x, center.y - height - 4);
                    break;
                case Direction.Left:
                    finalPositon = new Vector2Int(center.x - width - 4, center.y);
                    break;
                case Direction.Right:
                    finalPositon = new Vector2Int(center.x + width + 4, center.y);
                    break;
                default:
                    Debug.Log("No direction error");
                    break;
            }


            if (cam.SetPosition(finalPositon))
            {
                mov.SetDoorMovement(finalPositon, dir);
            }
        }
    }

    private Direction GetCloserDoorDirection(Vector2Int playerPos)
    {
        Direction dir = Direction.None;

        float distance = 9999;

        foreach(doorsInfo door in doors)
        {
            float newDist = Vector2Int.Distance(playerPos, door.position);
            if(newDist < distance)
            {
                distance = newDist;
                dir = door.dir;

                if(distance < 2) //Means the block is one of the closers
                {
                    break;
                }
            }
        }

       // Debug.Log("Door direction " + dir);
        return dir;
    }
}


