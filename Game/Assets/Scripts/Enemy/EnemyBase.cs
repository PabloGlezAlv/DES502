using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("General Parameters")]
    [SerializeField]
    protected float speed = 5f;
    [SerializeField]
    protected int maxLife = 5;
    [SerializeField]
    protected float timeWhiteDamage = 1;
    [SerializeField]
    protected float objectChances = 0.5f;

    protected int actualLife;

    protected Vector3 direction;

    protected Rigidbody2D rb;

    SpriteRenderer objectRenderer;

    protected SpriteRenderer spriterenderer;
    private float timerWhite;
    private bool damageReceived = false;

    private Color normal;
    protected void Awake()
    {
        rb = GetComponent<Rigidbody2D>();

        actualLife = maxLife;
        spriterenderer = GetComponent<SpriteRenderer>();

        normal = spriterenderer.color;

        objectRenderer = GetComponentInChildren<SpriteRenderer>();
    }

    protected void Update()
    {
        if(damageReceived)
        {
            timerWhite -= Time.deltaTime;
            if (timerWhite <= 0)
            {
                spriterenderer.color = normal;
                damageReceived = false;
            }
        }
    }

    public virtual void GetDamage(int damage)
    {
        int previousLife = actualLife;
        actualLife -= damage;
        if (actualLife <= 0) //Dead
        {
            this.gameObject.SetActive(false);
        }
        else // Life feedback ?
        {
            timerWhite = timeWhiteDamage;
            damageReceived = true;

            spriterenderer.color = Color.white;
        }
    }
}
