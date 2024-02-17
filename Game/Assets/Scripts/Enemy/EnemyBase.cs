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
    [SerializeField]
    protected SpriteRenderer objectRenderer;
    protected float baseSpeed;

    protected SpriteRenderer spriteRenderer;
    private float timerWhite;
    private bool damageReceived = false;

    private Color normal;

    protected Items itemID = Items.none;
    protected Rarity itemRarity = Rarity.Common;

    protected void Awake()
    {
        baseSpeed = speed;

        rb = GetComponent<Rigidbody2D>();

        actualLife = maxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();

        normal = spriteRenderer.color;

        SetObject();
    }

    private void SetObject()
    {
        float randomValue = UnityEngine.Random.value;

        if(randomValue < 0.5 )
        {
            itemID = (Items)UnityEngine.Random.Range(1, (int)Items.SpeedHelmet);
            itemRarity = (Rarity)UnityEngine.Random.Range(0, (int)Rarity.Legendary);
            Debug.Log("Enemy with object" + itemID + itemRarity);
        }
    }

    protected void Update()
    {
        if(damageReceived)
        {
            timerWhite -= Time.deltaTime;
            if (timerWhite <= 0)
            {
                spriteRenderer.color = normal;
                damageReceived = false;
            }
        }
    }

    protected void ResetSpeed()
    {
        speed = baseSpeed;
    }

    protected void MultiplierSpeed(float multiplier)
    {
        speed *= multiplier;
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

            spriteRenderer.color = Color.black;
        }
    }
}
