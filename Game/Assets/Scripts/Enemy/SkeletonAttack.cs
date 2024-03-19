using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering;

public class SkeletonAttack : MonoBehaviour
{
    [SerializeField]
    private float AttackDuration = 0.2f;

    private int damage = 0;

    private float timer = 0;
    private bool attacking = false;
    private Collider2D collider2D;

    Animator animator;

    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        GetComponent<SpriteRenderer>().color = Color.blue;

        animator = GetComponentInParent<Animator>();
    }

    void Update()
    {
        if (attacking)
        {
            timer += Time.deltaTime;

            if (timer >= AttackDuration)
            {
                attacking = false;
                collider2D.enabled = false;
                GetComponent<SpriteRenderer>().color = Color.blue;


                animator.SetBool("isAttacking", false);
            }
        }
    }

    public void SetDamage(int dam)
    { 
        damage = dam;
    }
    public void Attack()
    {
        if (!attacking)
        {
            attacking = true;
            collider2D.enabled = true;
            timer = 0;
            GetComponent<SpriteRenderer>().color = Color.black;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if (system = collision.gameObject.GetComponent<LifeSystem>()) 
        {
            system.GetDamage(damage);
        }
    }
}
