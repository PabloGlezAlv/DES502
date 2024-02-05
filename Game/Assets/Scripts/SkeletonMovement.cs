using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5f;
    [SerializeField]
    private float changeDirection = 0.5f;

    [Range(0f, 1f)]
    private float focusPlayer = 0.4f; 

    [Range(0f, 1f)]
    private float sidePlayuer = 0.3f;

    [SerializeField]
    private Transform player;

    private float movementTimer = 0f;

    private Vector3 direction;
    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        //Change actualdirection
        if (movementTimer > changeDirection)
        {
            // Calcula la dirección hacia el jugador
            direction = (player.position - transform.position).normalized;

            float rndDir = Random.Range(0.0f, 1.0f);

            if (rndDir >= focusPlayer) // Ir a los lados
            {
                float angle = Mathf.Clamp(RandomGaussian(45, 15), 0f, 90f);

                if (Random.Range(0.0f, 1.0f) <= sidePlayuer) // Moverse hacia el lado izquierdo
                    angle = -angle;

                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                // Rotar el vector en todas las dimensiones
                direction = (Vector2)(rotation * direction);
            }

            movementTimer = 0;
        }

        // Mueve al enemigo
        rb.velocity = direction * speed;

        movementTimer += Time.fixedDeltaTime;
    }

    // Gaussian function, play with parameters different results
    private float RandomGaussian(float media, float desviacionEstandar)
    {
        float u1 = 1f - Random.value;
        float u2 = 1f - Random.value;
        float randStdNormal = Mathf.Sqrt(-2f * Mathf.Log(u1)) * Mathf.Sin(2f * Mathf.PI * u2);
        float randNormal = media + desviacionEstandar * randStdNormal; 
        return randNormal;
    }
}
