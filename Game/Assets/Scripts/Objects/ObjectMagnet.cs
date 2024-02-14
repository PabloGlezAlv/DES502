using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class ObjectMagnet : MonoBehaviour
{
    [SerializeField]
    Transform playerTransform;
    [SerializeField]
    GameObject shadow;
    [SerializeField]
    float attractionDistance = 5f;
    [SerializeField]
    float attractionSpeed = 5f;
    [SerializeField]
    float speedMultiplier = 3f;

    private bool magnet = false;

    private float minDistance = 1f;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void DeactivateMovement()
    {
         ObjectMovement script = GetComponent<ObjectMovement>();
         if (script != null)
         {
             script.enabled = false;
         }

            
         shadow.SetActive(false);   
    }

    void FixedUpdate()
    {

        if (!magnet)
        {
            magnet = Vector3.Distance(transform.position, playerTransform.position) < attractionDistance;
            if(magnet)
                DeactivateMovement();
        }
        if (magnet)
        {
            Vector2 directionToPlayer = playerTransform.position - transform.position;

            float maxSpeed = attractionSpeed * speedMultiplier;


            directionToPlayer.Normalize();

            float newSpeed = (1 - Vector3.Distance(transform.position, playerTransform.position) / attractionDistance) * speedMultiplier;

            Vector2 extraVel = directionToPlayer * (attractionSpeed * newSpeed);

            rb.velocity = directionToPlayer * attractionSpeed + extraVel;
        }
    }
}
