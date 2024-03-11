using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class TotemAttack : EnemyBase, IEnemy
{
    [Header("Specific Parameters")]
    [SerializeField]
    private float AttackRatio = 1f;
    [SerializeField]
    private int numberBullets = 3;
    [SerializeField]
    private float delayBullet = 0.2f;
    [SerializeField]
    private GameObject bullet;

    private List<Vector3> attackPositions = new List<Vector3>();

    private Transform player;

    private float timerAttack = 0f;

    int roundBullets = 0;

    private static List<GameObject> bullets = new List<GameObject>();

    protected void Awake()
    {
        base.Awake();
        baseSpeed = speed;

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }


    protected void Update()
    {
        base.Update();

        timerAttack += Time.deltaTime;

        if(timerAttack > AttackRatio) 
        {
            roundBullets++;
            timerAttack -= delayBullet;
            //Attack
            attackPositions.Add((player.position - transform.position).normalized);
            Invoke("SpawnBullet", delayBullet);

            //Reset attack
            if (roundBullets >= numberBullets)
            {
                timerAttack = 0;
                roundBullets = 0;
            }
        }
    }

    private GameObject FindDisabledBullet()
    {
        foreach (GameObject bullet in bullets)
        {
            if (!bullet.activeSelf)
            {
                bullet.SetActive(true);
                return bullet; 
            }
        }

        return null;
    }

    void SpawnBullet()
    {

        GameObject newBullet = FindDisabledBullet();
        if(newBullet == null)
        {
            newBullet = Instantiate(bullet);
            bullets.Add(newBullet);
        }
        BulletMovement bull = newBullet.GetComponent<BulletMovement>();
        bull.SetDamage(damage);
        bull.SetPosition(transform.position);
        bull.SetMovementDirection(attackPositions[0]);

        attackPositions.Remove(attackPositions[0]);
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

                // Normalizar el vector para obtener solo la direcci�n
                dir.Normalize();

                rb.AddForce(-dir * pushForce, ForceMode2D.Impulse);
                Debug.Log(dir);
            }
        }
    }
}
