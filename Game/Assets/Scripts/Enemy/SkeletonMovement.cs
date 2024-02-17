using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class SkeletonMovement : EnemyBase
{
    [Header("Specific Parameters")]

    [SerializeField]
    private float changeDirection = 0.5f;
    [SerializeField]
    private float changeDirPosAttack = 1.0f;

    [Range(0f, 1f)]
    private float focusPlayer = 0.4f; 

    [Range(0f, 1f)]
    private float sidePlayer = 0.3f;

    [SerializeField]
    private float attackDistance = 2f;

    [Header("Directions: Right, left, top, bottom")]
    [SerializeField]
    protected List<GameObject> attackDir = new List<GameObject>(4);

    GameObject actualDir;

    private Transform player;

    private float movementTimer = 0f;



    bool justAttacked = false;

    private SkeletonAttack attack;

    private float timerAttack = 0f;


    private void Start()
    {
        movementTimer = changeDirection;

        attack = GetComponentInChildren<SkeletonAttack>();

        player = GameObject.FindGameObjectWithTag("Player").transform;

        actualDir = attackDir[0];
    }

    void FixedUpdate()
    {
        //Check to attack
        if (Vector3.Distance(transform.position, player.position) < attackDistance && !justAttacked)
        {
            direction = (player.position - transform.position).normalized;
            direction = -direction;

            justAttacked = true;

            attack.Attack();

            movementTimer = changeDirPosAttack;
            timerAttack = changeDirPosAttack;
        }
        else if (movementTimer < 0) //Change actualdirection
        {
            // Calcula la dirección hacia el jugador
            direction = (player.position - transform.position).normalized;

            float rndDir = Random.Range(0.0f, 1.0f);

            if (rndDir >= focusPlayer) // Ir a los lados
            {
                float angle = Mathf.Clamp(RandomGaussian(45, 15), 0f, 90f);

                if (Random.Range(0.0f, 1.0f) <= sidePlayer) // Moverse hacia el lado izquierdo
                    angle = -angle;

                Quaternion rotation = Quaternion.Euler(0, 0, angle);

                // Rotar el vector en todas las dimensiones
                direction = (Vector2)(rotation * direction);
            }



            movementTimer += changeDirection;
        }

        if (justAttacked && timerAttack < 0)
        {
            justAttacked = false;
        }

        SelectAttackArea();

        // Mueve al enemigo
        rb.velocity = direction * speed;

        movementTimer -= Time.fixedDeltaTime;
        timerAttack -= Time.fixedDeltaTime;
    }

    private void SelectAttackArea()
    {
        if (direction.x > 0f && direction.x > direction.y)
        {
            actualDir.SetActive(false);
            if (justAttacked)
            {
                actualDir = attackDir[1];
            }
            else
            {
                actualDir = attackDir[0];
            }
            actualDir.SetActive(true);
        }
        else if (direction.x < 0f && direction.y > direction.x)
        {
            actualDir.SetActive(false);
            if (justAttacked)
            {
                actualDir = attackDir[0];
            }
            else
            {
                actualDir = attackDir[1];
            }
            actualDir.SetActive(true);
        }
        else if (direction.y > 0 && direction.y >= direction.x)
        {
            actualDir.SetActive(false);
            if (justAttacked)
            {
                actualDir = attackDir[3];
            }
            else
            {
                actualDir = attackDir[2];
            }
            actualDir.SetActive(true);
        }
        else
        {
            actualDir.SetActive(false);
            if (justAttacked)
            {
                actualDir = attackDir[2];
            }
            else
            {
                actualDir = attackDir[3];
            }
            actualDir.SetActive(true);
        }
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

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }
}
