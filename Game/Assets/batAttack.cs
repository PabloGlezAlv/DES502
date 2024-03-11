using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class batAttack : MonoBehaviour
{
    private int damage = 0;


    public void setDamage(int dam)
    {
        damage = dam;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("BAT collider");

        //Collision  with player
        LifeSystem system;
        if (system = collision.gameObject.GetComponent<LifeSystem>())
        {
            system.GetDamage(damage);
            Debug.Log("BAT attack player");
        }
    }
}
