using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    [SerializeField]
    private float AttackDuration = 0.2f;
    [SerializeField]
    private int damage = 2;

    private float timer = 0;
    private bool attacking = false;
    private new Collider2D collider2D;
    private Vector3 playerCenter;

    private int startDamage;

    private void Awake()
    {
        playerCenter = transform.parent.transform.parent.transform.position;

        startDamage = damage;
    }

    public void addDamage(int add)
    {
        damage += add;
    }

    public void resetDamage()
    {
        damage = startDamage;
    }

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        GetComponent<SpriteRenderer>().color = Color.green;
    }

    void Update()
    {
        if (!attacking && Input.GetMouseButtonDown(0))
        {
            attacking = true;
            collider2D.enabled = true;
            timer = 0;
            GetComponent<SpriteRenderer>().color = Color.red;
        }
        else if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= AttackDuration)
            {
                attacking = false;
                collider2D.enabled = false;
                GetComponent<SpriteRenderer>().color = Color.green;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy move = collision.GetComponent<IEnemy>();
        if (move != null)
        {
            move.GetDamage(damage, playerCenter);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        IEnemy move = collision.GetComponent<IEnemy>();
        if (move != null)
        {
            move.GetDamage(damage, playerCenter);
        }
    }
}
