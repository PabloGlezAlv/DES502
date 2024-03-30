using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : EnemyBase, IEnemy
{
    [Header("BAT PARAMETERS")]
    [SerializeField]
    Animator anim;
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

    void Awake()
    {
        base.Awake();
        timer = changeDir;

        attack = GetComponentInChildren<batAttack>();
    }
    void Start()
    {
        base.Start();

        attack.setDamage(damage);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if(timer <= 0)
        {
            rb.velocity = Vector3.zero;
            Invoke("DashMovement", waitTime);
            timer = changeDir;
        }
    }

    private void DashMovement()
    {
        dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

        rb.AddForce(dir * speed, ForceMode2D.Impulse);

        AudioManager.instance.Play("BatSwoop");
    }

    private void BounceMovement()
    {
        dir = -dir;

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

                // Normalizar el vector para obtener solo la dirección
                dir.Normalize();
            }
        }
    }
}
