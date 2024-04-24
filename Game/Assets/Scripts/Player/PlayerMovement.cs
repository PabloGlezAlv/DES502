using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Transactions;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Player movement speed
    [SerializeField]
    private float doorAlpha = 0.3f; // Player movement speed

    [SerializeField]
    private DoorsController doorsController;

    [Header("AttackArea")]
    [SerializeField]
    private GameObject currentAttack;

    private List<Direction> inputs = new List<Direction>(4); // enum Direction { None, Top, Bottom, Left, Right } DECIDE ANIMATIONS

    private Rigidbody2D rb;

    private Vector2Int currentRoom = new Vector2Int(0, 0);


    private float baseSpeed;

    bool doorMovement = false;

    float doorsTime = 1; //Number seconds
    float timer = 0;

    float horizontalMovement = 0f;
    float verticalMovement = 0f;

    float horizontalMovementDoor = 0f;
    float verticalMovementDoor = 0f;

    int horizontalKeysDown = 0;
    int verticalKeysDown = 0;
    new BoxCollider2D collider;

    UnityEngine.Vector3 baseScale;

    bool move = true;

    float alpha = 1;

    SpriteRenderer spriteRenderer;

    Animator animator;

    public Vector2Int GetCurrentRoom()
    {
        return currentRoom;
    }
    public void SetDoorMovement(Vector2Int value, Direction dir)
    {
        currentRoom = value;
        doorMovement = true;


        horizontalMovementDoor = 0;
        verticalMovementDoor = 0;

        collider.isTrigger = true;

        switch (dir)
        {
            case Direction.Right: horizontalMovementDoor = 1; break;
            case Direction.Left: horizontalMovementDoor = -1; break;
            case Direction.Top: verticalMovementDoor = 1; break;
            case Direction.Bottom: verticalMovementDoor = -1; break;
        }
    }

    public Direction GetCurrentDirection()
    {
        if (inputs.Count() > 0) return inputs[0];
        else return Direction.None;
    }

    public void ResetPlayer()
    {
        currentRoom = new Vector2Int(0, 0);
    }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        rb = GetComponent<Rigidbody2D>();

        collider = GetComponent<BoxCollider2D>();

        spriteRenderer = GetComponent<SpriteRenderer>();

        baseSpeed = speed;
        baseScale = transform.localScale;
    }
    public void ReduceScale(float speedMultiplier)
    {
        ResetScale();
        transform.localScale *= speedMultiplier;
    }

    public void SetNormalMove(bool set)
    {
        move = set;
    }


    private void OnEnable()
    {
        
    }
    public void ResetScale()
    {
        transform.localScale = baseScale;
    }

    public void AddDamage(int dam)
    {
        currentAttack.GetComponent<PlayerAttack>().addDamage(dam);
    }

    public void ResetDamage() 
    {
        currentAttack.GetComponent<PlayerAttack>().resetDamage();
    }

    public void AddSpeed(float speedMultiplier)
    {
        ResetSpeed();
        speed *= speedMultiplier;
    }

    public void ResetSpeed()
    {
        speed = baseSpeed;
    }

    private void ChangeAttack(Direction newDir)
    {
        //currentAttack.SetActive(false);

        currentAttack.transform.position = currentAttack.transform.parent.position;
        currentAttack.transform.rotation = currentAttack.transform.parent.rotation;

        switch (newDir)
        {
            case Direction.None:
                break;
            case Direction.Right:
                currentAttack.transform.Translate(UnityEngine.Vector3.right * transform.localScale.x, Space.World);
                currentAttack.transform.Translate(UnityEngine.Vector3.up * transform.localScale.y, Space.World);
                break;
            case Direction.Left:
                currentAttack.transform.Translate(UnityEngine.Vector3.left * transform.localScale.x, Space.World);
                currentAttack.transform.Translate(UnityEngine.Vector3.up * transform.localScale.y, Space.World);
                currentAttack.transform.Rotate(UnityEngine.Vector3.forward, 180f);
                break;
            case Direction.Top:
                currentAttack.transform.Translate(UnityEngine.Vector3.up * transform.localScale.y *2, Space.World);
                currentAttack.transform.Rotate(UnityEngine.Vector3.forward, 90f);
                break;
            case Direction.Bottom:
                currentAttack.transform.Rotate(UnityEngine.Vector3.forward, 270f);
                break;
        }

        // currentAttack.SetActive(true);
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
                collider.isTrigger = false;
                alpha = 1;
                Color c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }


            if (timer < doorAlpha) //FadeOut the player
            {
                alpha -= Time.deltaTime / doorAlpha;
                Color c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }
            else if(doorsTime - doorAlpha < timer)
            {
                alpha += Time.deltaTime / doorAlpha;
                Color c = spriteRenderer.color;
                c.a = alpha;
                spriteRenderer.color = c;
            }
        }
        
            bool change = false;
            if (Input.anyKey)
            {
                if (Input.GetKeyDown(KeyCode.A))
                {
                    horizontalMovement += -1f;
                    horizontalKeysDown++;
                    inputs.Add(Direction.Left);
                    if (horizontalKeysDown == 2)
                    {
                        horizontalMovement += -1f;
                        inputs.Remove(Direction.Right); //Reset postion in list
                        inputs.Add(Direction.Right);
                    }

                    change = true;
                }
                if (Input.GetKeyDown(KeyCode.D))
                {
                    horizontalMovement += 1f;
                    horizontalKeysDown++;
                    inputs.Add(Direction.Right);
                    if (horizontalKeysDown == 2)
                    { 
                        horizontalMovement += 1f;

                        inputs.Remove(Direction.Left); //Reset postion in list
                        inputs.Add(Direction.Left);
                    }
                    change = true;
                }

                if (Input.GetKeyDown(KeyCode.W))
                {
                    verticalMovement += 1f;
                    verticalKeysDown++;
                    inputs.Add(Direction.Top);
                    if (verticalKeysDown == 2)
                    {
                        verticalMovement += 1f;

                        inputs.Remove(Direction.Bottom); //Reset postion in list
                        inputs.Add(Direction.Bottom);
                    }
                    change = true;
                }
                if (Input.GetKeyDown(KeyCode.S))
                {
                    verticalMovement += -1f;
                    verticalKeysDown++;
                    inputs.Add(Direction.Bottom);
                    if (verticalKeysDown == 2)
                    {
                        verticalMovement += -1f;

                        inputs.Remove(Direction.Top); //Reset postion in list
                        inputs.Add(Direction.Top);
                    }
                    change = true;
                }
            }

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
                inputs.Remove(Direction.Left);
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
                inputs.Remove(Direction.Right);
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
                inputs.Remove(Direction.Top);
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
                inputs.Remove(Direction.Bottom);
            }


        if(inputs.Count > 0)
        {

            animator.SetInteger("mov", (int)inputs[0]);
        }
        if (change && inputs.Count > 0)
        {
            ChangeAttack(inputs[0]);
            animator.SetInteger("mov", (int)inputs[0]);
          //  Debug.Log(inputs[0]);
        }
        else if (inputs.Count == 0)
        {
            animator.SetInteger("mov", 0);
        }


        
    }
    void FixedUpdate()
    {
        if(move)
        {
            if (doorMovement)
                MovePlayer(horizontalMovementDoor, verticalMovementDoor);
            else
                MovePlayer(horizontalMovement, verticalMovement);
        }
    }

    private void MovePlayer(float horizontalMovement, float verticalMovement)
    {
        UnityEngine.Vector3 movement = new UnityEngine.Vector3(horizontalMovement, verticalMovement, 0f).normalized * speed * Time.fixedDeltaTime;

        rb.MovePosition(transform.position + movement);
    }

    public void setPosition(Vector2Int newPosition)
    {
        transform.position = new UnityEngine.Vector3(newPosition.x, newPosition.y, transform.position.z);
    }
}
