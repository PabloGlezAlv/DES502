using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DiggerMovement : EnemyBase, IEnemy
{
    [Header("Specific Parameters")]

    [SerializeField]
    private float BeforeAttack = 0.5f;
    [SerializeField]
    private float attackingTime = 1.0f;
    [SerializeField]
    private float hideTime = 1.0f; 
    [SerializeField]
    private float beforeAttackingUp = 0.2f;
    [SerializeField]
    private float afterAttackingUp = 0.5f;
    [SerializeField]
    private Sprite hideSprite;


    PlayerMovement player;

    private DiggerAttack attack; 

    private float hideTimer = 0f;
    private float waitingTimer = 0f;
    private float upBeforeAttackTimer = 0f;
    private float upAfterAttackTimer = 0f;
    private float AttakingTimer = 0f;
    private Collider2D collider;

    private List<Vector2Int> blocksFull = new List<Vector2Int>();

    Vector2Int center;

    private  Animator animator;

    protected void Awake()
    {
        base.Awake();

        attack = GetComponentInChildren<DiggerAttack>();

        animator = GetComponent<Animator>();

        collider = GetComponent<Collider2D>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        hideTimer = hideTime;

        spriteRenderer.sprite = null;


    }

    private void Start()
    {
        base.Start();
        center = new Vector2Int((int)transform.parent.position.x, (int)transform.parent.position.y);

        //Stores the position of the 
        List<Vector2Int> HoleMap = GetComponentInParent<TrapLocations>().GetHolesRelative();
        List<Vector2Int> SpikeMap = GetComponentInParent<TrapLocations>().GetSpikesRelative();
        List<Vector2Int> torretMap = GetComponentInParent<TrapLocations>().GetTorretsFinalLocation();

        blocksFull.AddRange(HoleMap);
        blocksFull.AddRange(SpikeMap);
        blocksFull.AddRange(torretMap);
    }

    void FixedUpdate()
    {
        if(hideTimer > 0)
        {
            hideTimer -= Time.fixedDeltaTime;
            if(hideTimer <= 0)
            {
                //Direction playerDir = player.GetCurrentDirection();
                Vector3 playerpos = new Vector3(((int)player.transform.position.x) + 0.5f, ((int)player.transform.position.y) + 0.5f, player.transform.position.z);
 
                Vector2Int startPoint = new Vector2Int((int)playerpos.x, (int)playerpos.y);
                collider.enabled = true;
                do
                {
                    int rngX = UnityEngine.Random.Range(-5, 5);
                    int rngY = UnityEngine.Random.Range(-3, 3);

                    playerpos.x = startPoint.x ;
                    playerpos.y = startPoint.y;

                    playerpos.x += rngX;
                    playerpos.y += rngY;
                } while (blocksFull.Contains(new Vector2Int((int)playerpos.x, (int)playerpos.y)));

                

                if(playerpos.x > center.x + 10 )
                {
                    playerpos.x = center.x + 10;
                }
                else if(playerpos.x < center.x -12)
                {
                    playerpos.x = center.x - 12;
                }

                if(playerpos.y > center.y + 4 )
                {
                    playerpos.y = center.y + 4;
                }
                else if(playerpos.y < center.y - 5)
                {
                    playerpos.y = center.y - 5;
                }

                playerpos.x += 0.5f;
                playerpos.y += 0.5f;

                transform.position = playerpos;

                waitingTimer = BeforeAttack;

                spriteRenderer.color = Color.white;
                animator.SetBool("hide", false);
                animator.SetBool("appear", true);
            }
        }
        else if(waitingTimer > 0)
        {
            waitingTimer -= Time.fixedDeltaTime;
            if(waitingTimer <= 0)
            {
                animator.SetBool("appear", false);
                animator.SetBool("stand", true);
                upBeforeAttackTimer = beforeAttackingUp;
                objectRenderer.color = Color.white;
            }
        }
        else if (upBeforeAttackTimer > 0)
        {
            upBeforeAttackTimer -= Time.fixedDeltaTime;
            if (upBeforeAttackTimer <= 0)
            {
                AttakingTimer = attackingTime;
                attack.enabled = true;
                animator.SetBool("attack", true);
                animator.SetBool("stand", false);
                Debug.Log("Digger Attacking");
            }
        }
        else if(AttakingTimer > 0)
        {
            AttakingTimer -= Time.fixedDeltaTime;
            if (AttakingTimer <= 0)
            {
                upAfterAttackTimer = afterAttackingUp;
                attack.enabled = false;

                animator.SetBool("attack", false);
                animator.SetBool("stand", true);
                Debug.Log("Digger No Attacking");
            }
        }
        else if (upAfterAttackTimer > 0)
        {
            upAfterAttackTimer -= Time.fixedDeltaTime;
            if (upAfterAttackTimer <= 0)
            {
                hideTimer = hideTime + UnityEngine.Random.Range(0, 0.5f); //Variety to the diggers
                collider.enabled = false;

                animator.SetBool("stand", false);
                animator.SetBool("hide", true);
                spriteRenderer.color = new Color(0,0,0,0);
                objectRenderer.color = new Color(0, 0, 0, 0);
                Debug.Log("Hide");
            }
        }
    }

    public void GetDamage(int damage, Vector3 playerPos)
    {
        int previousLife = actualLife;
        actualLife -= damage;
        if (actualLife <= 0) //Dead
        {
            float randomValue = UnityEngine.Random.value;

            if (randomValue < dropCoinChances)
            {
                GameObject coin = Instantiate(coinPrefab);
                coin.transform.position = transform.position;
            }
            doorsController.KillEntity();

            animator.SetBool("stand", true);
            this.gameObject.SetActive(false);
        }
        else
        {
            timerWhite = timeWhiteDamage;
            damageReceived = true;

            spriteRenderer.color = Color.black;

            if (pushable)
            {

                Vector2 dir = playerPos - transform.position;

                // Normalizar el vector para obtener solo la dirección
                dir.Normalize();

                rb.AddForce(-dir * pushForce, ForceMode2D.Impulse);
            }
        }
    }
}
