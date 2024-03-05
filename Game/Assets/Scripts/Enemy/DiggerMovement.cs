using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Xml;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DiggerMovement : EnemyBase, IEnemy
{
    [Header("Specific Parameters")]

    [SerializeField]
    private float BeforeAttack = 0.5f;
    [SerializeField]
    private float attacking = 1.0f;
    [SerializeField]
    private float hideTime = 1.0f;
    [SerializeField]
    private Sprite hideSprite;

    PlayerMovement player;

    bool justAttacked = false;

    private SkeletonAttack attack;

    private Sprite attackSprite;

    private float hideTimer = 0f;
    private float waitingTimer = 0f;
    private float AttakingTimer = 0f;

    Vector2Int center;

    protected void Awake()
    {
        base.Awake();
        baseSpeed = speed;

        attack = GetComponentInChildren<SkeletonAttack>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerMovement>();
        hideTimer = hideTime;

        attackSprite = spriteRenderer.sprite;
        spriteRenderer.sprite = null;

       
    }

    private void Start()
    {
        center = new Vector2Int((int)transform.parent.position.x, (int)transform.parent.position.y);
    }

    void FixedUpdate()
    {
        if(hideTimer > 0)
        {
            hideTimer -= Time.fixedDeltaTime;
            if(hideTimer <= 0)
            {
                Direction playerDir = player.GetCurrentDirection();
                Vector3 playerpos = new Vector3(((int)player.transform.position.x) + 0.5f, ((int)player.transform.position.y) + 0.5f, player.transform.position.z);
 
                int rngX = UnityEngine.Random.Range(-5, 5);
                int rngY = UnityEngine.Random.Range(-3, 3);

                playerpos.x += rngX;
                playerpos.y += rngY;

                if(playerpos.x > center.x + 11 )
                {
                    playerpos.x = center.x + 11;
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

                transform.position = playerpos;

                waitingTimer = BeforeAttack;
               
                spriteRenderer.sprite = hideSprite;
            }
        }
        else if(waitingTimer > 0)
        {
            waitingTimer -= Time.fixedDeltaTime;
            if(waitingTimer <= 0)
            {
                AttakingTimer = attacking;
                spriteRenderer.sprite = attackSprite;
            }
        }
        else if(AttakingTimer > 0)
        {
            AttakingTimer -= Time.fixedDeltaTime;
            if (AttakingTimer <= 0)
            {
                hideTimer = hideTime;
                spriteRenderer.sprite = null;
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

                Debug.Log(playerPos);
                Debug.Log(transform.position);

                // Normalizar el vector para obtener solo la dirección
                dir.Normalize();

                rb.AddForce(-dir * pushForce, ForceMode2D.Impulse);
                Debug.Log(dir);
            }
        }
    }
}
