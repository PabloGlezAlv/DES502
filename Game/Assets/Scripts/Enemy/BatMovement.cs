using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatMovement : EnemyBase, IEnemy
{
    private Rigidbody2D rb;
    [SerializeField]
    private float changeDir = 2f;

    private float timer = 0;

    void Awake()
    {
        base.Awake();
        timer = changeDir;
        rb = GetComponent<Rigidbody2D>();
    }
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        timer -= Time.fixedDeltaTime;

        if(timer <= 0)
        {
            Vector2 dir = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f)).normalized;

            rb.AddForce(dir * speed, ForceMode2D.Impulse);

            timer = changeDir;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        timer = 0;
    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        timer = 0;
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
