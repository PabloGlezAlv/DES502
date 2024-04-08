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

    Animator animator;

    private void Awake()
    {
        playerCenter = transform.parent.transform.parent.transform.position;

        startDamage = damage;

        animator = transform.parent.transform.parent.GetComponent<Animator>();
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
        if (!attacking && Input.GetKeyDown(KeyCode.Space))
        {
            attacking = true;
            collider2D.enabled = true;
            timer = 0;
            GetComponent<SpriteRenderer>().color = Color.red;

            animator.SetBool("attack", true);

            //Swing sound here !!!
            //AudioManager.instance.Play("Swing");
        }
        else if(attacking)
        {
            timer += Time.deltaTime;

            if(timer >= AttackDuration)
            {
                attacking = false;
                collider2D.enabled = false;
                GetComponent<SpriteRenderer>().color = Color.green;
                animator.SetBool("attack", false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IEnemy move = collision.GetComponent<IEnemy>();
        if (move != null)
        {
            move.GetDamage(damage, playerCenter);
            AudioManager.instance.Play("Clash");
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
