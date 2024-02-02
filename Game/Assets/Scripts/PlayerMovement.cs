using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Player movement speed

    [SerializeField]
    private DoorsController doorsController;

    private Rigidbody2D rb;

    private Vector2Int currentRoom = new Vector2Int(0, 0);

    direction movDirection = direction.none;

    bool doorMovement = false;

    float doorsTime = 1; //Number seconds
    float timer = 0;
    public Vector2Int GetCurrentRoom()
    {
        return currentRoom;
    }
    public void SetDoorMovement(Vector2Int value)
    {
        currentRoom = value;
        doorMovement = true;
    }

    public direction GetCurrentDirection()
    {
        return movDirection;
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        float horizontalMovement = 0f;
        float verticalMovement = 0f;

        if (doorMovement)
        {
            switch (movDirection)
            {
                case direction.none:
                    Debug.Log("Error direction none in player");
                    break;
                case direction.top:
                    verticalMovement = 1f;
                    break;
                case direction.bottom:
                    verticalMovement = -1f;
                    break;
                case direction.left:
                    horizontalMovement = -1f;
                    break;
                case direction.right:
                    horizontalMovement = 1f;
                    break;
                default:

                    break;
            }

            timer += Time.deltaTime;

            if (timer > doorsTime)
            {
                doorsController.ChangeDoorsState(false); //CloseDoors
                doorMovement = false;
                timer = 0;
            }
        }
        else
        {
            //Check Input if key pressed
            if (Input.anyKey)
            {
                if (Input.GetKey(KeyCode.A))
                {
                    horizontalMovement = -1f;
                    movDirection = direction.left;
                }
                else if (Input.GetKey(KeyCode.D))
                {
                    horizontalMovement = 1f;
                    movDirection = direction.right;
                }

                if (Input.GetKey(KeyCode.W))
                {
                    verticalMovement = 1f;
                    movDirection = direction.top;
                }
                else if (Input.GetKey(KeyCode.S))
                {
                    verticalMovement = -1f;
                    movDirection = direction.bottom;
                }
            }
        }

        MovePlayer(horizontalMovement, verticalMovement);
    }

    private void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        Vector3 movement = new Vector3(horizontalMovement, verticalMovement, 0f).normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + movement);

        if (horizontalMovement < 0)
        {
            transform.localScale = new Vector3(-transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }
        else if (horizontalMovement > 0)
        {
            transform.localScale = new Vector3(transform.localScale.y, transform.localScale.y, transform.localScale.z);
        }
    }
}
