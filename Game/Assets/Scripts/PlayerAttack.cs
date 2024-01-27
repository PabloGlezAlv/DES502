using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
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
        Debug.Log("Collision");
    }
}
