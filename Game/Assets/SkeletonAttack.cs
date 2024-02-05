using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkeletonAttack : MonoBehaviour
{
    [SerializeField]
    private float AttackDuration = 0;

    private float timer = 0;
    private bool attacking = false;
    private Collider2D collider2D;


    private void Start()
    {
        collider2D = GetComponent<Collider2D>();
        collider2D.enabled = false;
        GetComponent<SpriteRenderer>().color = Color.blue;
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
            }
        }
    }

    public void Attack()
    {
        Debug.Log("Attack");
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
        Debug.Log("Collision");
        if(collision.gameObject.GetComponent<PlayerMovement>()) 
        {
            Debug.Log("With player");
        }
    }
}
