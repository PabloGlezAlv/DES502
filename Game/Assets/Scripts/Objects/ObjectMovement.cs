using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectMovement : MonoBehaviour
{
    [SerializeField]
    private float amplitude = 1f; 

    [SerializeField]
    private float frequency = 1f;  

    private bool onFloor = true;  // Levitate or not

    private float originalY;  
    private new Rigidbody2D rigidbody2D;

    void Start()
    {
        originalY = transform.position.y;

        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (onFloor)
        {
            float newY = originalY + amplitude * Mathf.Sin(frequency * Time.time);

            rigidbody2D.MovePosition(new Vector2(transform.position.x, newY));
        }
    }
}
