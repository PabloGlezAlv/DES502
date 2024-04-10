using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : EnemyBase, IEnemy
{
    [Header("BAT PARAMETERS")]
    [SerializeField]
    Animator anim;
    SpriteRenderer sr;
    [SerializeField]
    private float changeDir = 2f;
    [SerializeField]
    private float waitTime = 0.5f;
    [SerializeField]
    private float crashTime = 1.5f;

    private float timer = 0;

    private Vector2 dir;

    batAttack attack;

    private bool FacingLeft = false;//the bat starts out facing right
    private bool FacingUp = false;//the bat starts out facing down
    private bool DeathAnimPlayed = false;//the bat starts out facing down

    void Awake()
    {
        base.Awake();
        timer = changeDir + Random.Range(-1f, 1f);

        attack = GetComponentInChildren<batAttack>();
    }
    void Start()
    {
        base.Start();

        sr = GetComponentInChildren<SpriteRenderer>();
        attack.setDamage(damage);
        DeathAnimPlayed = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if (rb.velocity == Vector2.zero)
        {
            anim.SetBool("BatChargeSide", false);
            anim.SetBool("BatChargeTop", false);
            FacingUp = false;
        }

        anim.SetFloat("FlapSpeed", 3 - timer);

        if(timer <= 0)
        {
            rb.velocity = Vector3.zero;
            Invoke("DashMovement", waitTime);
            timer = changeDir;
        }
    }

    private void DashMovement()
    {
        FacingUp = false;
        FacingLeft = false;
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        rb.AddForce(dir * speed, ForceMode2D.Impulse);
        if (dir.x < 0)
        {
            FacingLeft = false;
        }
        else if (dir.x >= 0)
        {
            FacingLeft = true;
        }
        if (dir.y < 0)
        {
            FacingUp = false;
        }
        else if (dir.y >= 0)
        {
            FacingUp = true;
        }

        if (dir.x > dir.y || dir.x > -dir.y)
        {
            FacingUp = false;
            anim.SetBool("BatChargeSide", true);
        }
        else if (dir.y > dir.x || dir.y < -dir.x)
        {
            FacingLeft = false;
            anim.SetBool("BatChargeTop", true);
        }

        sr.flipX = FacingLeft;
        sr.flipY = FacingUp;
        AudioManager.instance.Play("BatSwoop");
    }

    private void BounceMovement()
    {
        dir = -dir;
        anim.Play("BatWallHit");
        anim.SetBool("ChargeTop", false);
        anim.SetBool("ChargeSide", false);
        rb.AddForce(dir * speed, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector3.zero;
        Invoke("BounceMovement", crashTime);
        timer = changeDir;
    }

    public void GetDamage(int damage, Vector3 playerPos)
    {
        AudioManager.instance.Play("BatCry");
        int previousLife = actualLife;
        actualLife -= damage;
        if (actualLife <= 0) //Dead
        {
            if (!DeathAnimPlayed)
            {
                anim.Play("BatDie");
                DeathAnimPlayed = true;
            }
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
            }
        }
    }

    public void Die()
    {
        float randomValue = Random.value;

        if (randomValue < dropCoinChances)
        {
            GameObject coin = Instantiate(coinPrefab);
            coin.transform.position = transform.position;
        }
        doorsController.KillEntity();
        this.gameObject.SetActive(false);
    }
}
