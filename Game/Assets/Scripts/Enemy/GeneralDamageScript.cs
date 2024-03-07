using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralDamageScript : MonoBehaviour
{
    [SerializeField]
    private float IFrames;
    private float IFrameTime;
    [SerializeField]
    private int damage;

    private void Update()
    {
        IFrameTime += Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if (system = collision.gameObject.GetComponent<LifeSystem>())
        {
            if (IFrameTime > IFrames)
            {
                IFrameTime = 0;
                system.GetDamage(damage);
            }
        }
    }
}
