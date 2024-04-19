using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiggerAttack : MonoBehaviour
{
    [SerializeField]
    int damage = 1;

    bool damaged = false;

    private void OnEnable()
    {
        damaged = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if ((system = collision.gameObject.GetComponent<LifeSystem>()) && !damaged )
        {
            Debug.Log("Digger do damage");
            damaged = true;
            system.GetDamage(damage);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if ((system = collision.gameObject.GetComponent<LifeSystem>()) && !damaged)
        {
            Debug.Log("Digger do damage");
            damaged = true;
        }
    }

    private void OnDisable()
    {
       
    }
}
