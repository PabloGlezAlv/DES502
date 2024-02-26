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
    protected float timeWhiteDamage = 0.2f;
    [SerializeField]
    protected float objectChances = 0.5f;
    [SerializeField]
    protected float dropCoinChances = 0.5f;

    protected int actualLife;

    protected Vector3 direction;

    protected Rigidbody2D rb;
    [SerializeField]
    protected SpriteRenderer objectRenderer;
    [SerializeField]
    private GameObject coinPrefab;
    protected float baseSpeed;

    protected SpriteRenderer spriteRenderer;
    private float timerWhite;
    private bool damageReceived = false;

    private Color normal;

    protected Items itemID = Items.none;
    protected Rarity itemRarity = Rarity.Common;

    Vector3 baseScale;

    DoorsController doorsController;

    int timesActivated = 0;

    private void OnEnable()
    {
        if(timesActivated != 0)
            doorsController.AddEntity();

        timesActivated++;
    }

    protected void Awake()
    {
        baseSpeed = speed;
        baseScale = transform.localScale;

        doorsController = GameObject.Find("DoorsController").GetComponent<DoorsController>();

        rb = GetComponent<Rigidbody2D>();

        actualLife = maxLife;
        spriteRenderer = GetComponent<SpriteRenderer>();

        normal = spriteRenderer.color;

        SetObject();
    }
    private void Start()
    {
        //There is an object
        if (itemID != Items.none)
        {
            switch (itemID)
            {
                case Items.SpeedHelmet:
                    objectRenderer.sprite = ObjectsManager.instance.getSpeedSprite((int)itemRarity);
                    MultiplierSpeed(ObjectsManager.instance.getSpeedUpgrade((int)itemRarity));
                    break;
                case Items.ScaleHelmet:
                    objectRenderer.sprite = ObjectsManager.instance.getScaleSprite((int)itemRarity);
                    ChangeScale(ObjectsManager.instance.getScaleUpgrade((int)itemRarity));
                    break;
                default:
                    Console.WriteLine("Rareza desconocida.");
                    break;
            }

        }
    }
    private void SetObject()
    {
        float randomValue = UnityEngine.Random.value;

        if(randomValue < 0.5 )
        {
            itemID = (Items)UnityEngine.Random.Range(1, (int)Items.SpeedHelmet + 1);
            itemRarity = (Rarity)UnityEngine.Random.Range(0, (int)Rarity.Legendary + 1);
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
    protected void ResetScaleScale()
    {
        transform.localScale = baseScale;
    }

    protected void ChangeScale(float multiplier)
    {
        transform.localScale *= multiplier;
    }
    public virtual void GetDamage(int damage)
    {
        int previousLife = actualLife;
        actualLife -= damage;
        if (actualLife <= 0) //Dead
        {
            float randomValue = UnityEngine.Random.value;

            if (randomValue < 0.5)
            {
                GameObject coin = Instantiate(coinPrefab);
                coin.transform.position = transform.position;
            }
                doorsController.KillEntity();
            this.gameObject.SetActive(false);
        }
        else 
        {
            timerWhite = timeWhiteDamage;
            damageReceived = true;

            spriteRenderer.color = Color.black;
        }
    }
}
