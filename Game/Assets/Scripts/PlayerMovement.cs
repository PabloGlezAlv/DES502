using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f; // Player movement speed

    private Rigidbody2D rb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Check Input if key pressed
        if(Input.anyKey)
        {
            float horizontalMovement = 0f;
            float verticalMovement = 0f;

            if (Input.GetKey(KeyCode.A))
            {
                horizontalMovement = -1f;
            }
            else if (Input.GetKey(KeyCode.D))
            {
                horizontalMovement = 1f;
            }

            if (Input.GetKey(KeyCode.W))
            {
                verticalMovement = 1f;
            }
            else if (Input.GetKey(KeyCode.S))
            {
                verticalMovement = -1f;
            }

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
}
