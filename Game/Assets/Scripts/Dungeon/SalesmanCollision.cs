using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class SalesmanCollision : MonoBehaviour
{
    int count = 0;
    Sprite spriteNormal;
    [SerializeField]
    Sprite spriteHostile;

    SpriteRenderer spriteRenderer;
    Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();

        //spriteNormal = spriteRenderer.sprite;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.GetComponent<PlayerAttack>() != null)
        {
            count++;
            GetComponentInChildren<SalesArea>().setHostile();
            anim.SetTrigger("Hide");
            //spriteRenderer.sprite = spriteHostile;
        }
    }

    public void ChangePosition(Vector3 position)
    {
        transform.position = position;

        //spriteRenderer.sprite = spriteNormal;
    }
}
