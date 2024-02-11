using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Player movement speed

    [SerializeField]
    private DoorsController doorsController;

    [Header("Directions: Right, left, top, bottom")]
    [SerializeField]
    private List<GameObject> directions = new List<GameObject>(4);

    private List<direction> inputs = new List<direction>(4);

    private Rigidbody2D rb;

    private Vector2Int currentRoom = new Vector2Int(0, 0);

    direction movDirection = direction.none;
    direction attackDir = direction.right;

    GameObject currentAttack;


    bool doorMovement = false;

    float doorsTime = 1; //Number seconds
    float timer = 0;

    float horizontalMovement = 0f;
    float verticalMovement = 0f;

    int horizontalKeysDown = 0;
    int verticalKeysDown = 0;

    bool pressingSomething = false;
    bool firstAD = false;
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

        currentAttack = directions[0]; //First direction right
    }

    private void ChangeAttack(direction newDir)
    {
        currentAttack.SetActive(false);
        attackDir = newDir;
        switch (newDir)
        {
            case direction.none:
                break;
            case direction.right:
                currentAttack = directions[0];
                break;
            case direction.left:
                currentAttack = directions[1];
                break;
            case direction.top:
                currentAttack = directions[2];
                break;
            case direction.bottom:
                currentAttack = directions[3];
                break;
        }

        currentAttack.SetActive(true);
    }

    private void Update()
    {
        if (doorMovement)
        {
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
            bool actualpressing = pressingSomething;

            bool change = false;
            if (Input.anyKey)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    horizontalMovement += -1f;
                    horizontalKeysDown++;
                    inputs.Add(direction.left);
                    if (horizontalKeysDown == 2)
                    {
                        horizontalMovement += -1f;
                        inputs.Remove(direction.right); //Reset postion in list
                        inputs.Add(direction.right);
                    }
                    if (!pressingSomething)
                    {
                        firstAD = true;
                        ChangeAttack(direction.left);
                    }
                    pressingSomething = true;

                    change = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    horizontalMovement += 1f;
                    horizontalKeysDown++;
                    inputs.Add(direction.right);
                    if (horizontalKeysDown == 2)
                    { 
                        horizontalMovement += 1f;

                        inputs.Remove(direction.left); //Reset postion in list
                        inputs.Add(direction.left);
                    }
                    if (!pressingSomething)
                    {
                        firstAD = true;
                        ChangeAttack(direction.right);
                    }
                    pressingSomething = true;
                    change = true;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    verticalMovement += 1f;
                    verticalKeysDown++;
                    inputs.Add(direction.top);
                    if (verticalKeysDown == 2)
                    {
                        verticalMovement += 1f;

                        inputs.Remove(direction.bottom); //Reset postion in list
                        inputs.Add(direction.bottom);
                    }
                    if (!pressingSomething)
                    {
                        firstAD = false;
                        ChangeAttack(direction.top);
                    }
                    pressingSomething = true;
                    change = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    verticalMovement += -1f;
                    verticalKeysDown++;
                    inputs.Add(direction.bottom);
                    if (verticalKeysDown == 2)
                    {
                        verticalMovement += -1f;

                        inputs.Remove(direction.top); //Reset postion in list
                        inputs.Add(direction.top);
                    }
                    if (!pressingSomething)
                    {
                        firstAD = false;
                        ChangeAttack(direction.bottom);
                    }
                    pressingSomething = true;
                    change = true;
                }
            }
            else pressingSomething = false;

            //Check if some key not pressed
            if (Input.GetKeyUp(KeyCode.A))
            {
                change = true;
                horizontalMovement -= -1f;
                horizontalKeysDown--;
                if (horizontalKeysDown == 0)
                {
                    horizontalMovement = 0;
                }
                else if (horizontalKeysDown == 1)
                {
                    horizontalMovement = 1f;
                }
                inputs.Remove(direction.left);
            }
            if (Input.GetKeyUp(KeyCode.D))
            {
                change = true;
                horizontalMovement -= 1f;
                horizontalKeysDown--;
                if (horizontalKeysDown == 0)
                {
                    horizontalMovement = 0;
                }
                else if (horizontalKeysDown == 1)
                {
                    horizontalMovement = -1f;
                }
                inputs.Remove(direction.right);
            }
            if (Input.GetKeyUp(KeyCode.W))
            {
                verticalMovement -= 1f;

                change = true;
                verticalKeysDown--;
                if (verticalKeysDown == 0)
                {
                    verticalMovement = 0;
                }
                else if (verticalKeysDown == 1)
                {
                    verticalMovement = -1f;
                }
                inputs.Remove(direction.top);
            }
            if (Input.GetKeyUp(KeyCode.S))
            {
                verticalMovement -= -1f;

                change = true;
                verticalKeysDown--;
                if (verticalKeysDown == 0)
                {
                    verticalMovement = 0;
                }
                else if (verticalKeysDown == 1)
                {
                    verticalMovement = 1f;
                }
                inputs.Remove(direction.bottom);
            }

            if (change && inputs.Count > 0)
            {
                ChangeAttack(inputs[0]);
            }
        }
    }
    void FixedUpdate()
    {
        MovePlayer(horizontalMovement, verticalMovement);
    }

    private void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(horizontalMovement, verticalMovement, 0f).normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + movement);
    }
}
