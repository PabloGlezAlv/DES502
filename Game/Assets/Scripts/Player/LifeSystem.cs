using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class LifeSystem : MonoBehaviour
{
    [Header("Parameters Life")]
    [SerializeField]
    private int startHearts = 5;
    [SerializeField]
    private int maxHearts = 10;
    [SerializeField]
    private int heartDistance = 10;

    [Header("Parameters Feedback")]
    [SerializeField]
    private float duration = 1f;
    [SerializeField]
    private int flashTimes = 3;


    private SpriteRenderer objectRenderer;

    private bool active = true;
    private float timer = 0;
    private int remainingChanges = 0;
    SpriteRenderer spriteRenderer;

    [Header("Images")]
    [SerializeField]
    private GameObject[] emptyHearts;
    [SerializeField]
    private GameObject[] halfHearts;

    private int actualHearts;
    private int maxLife;
    private int actualLife;
    void Start()
    {
        actualHearts = startHearts;

        ShowAllHearts();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        objectRenderer = GetComponentInChildren<ItemManager>().gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.L)) { GetDamage(3); }
        if (Input.GetKeyDown(KeyCode.H)) { GetHealed(2); }

        if(remainingChanges > 0) 
        {
            timer -= Time.deltaTime;

            if(timer < 0)
            {
                active = !active;
                spriteRenderer.enabled = active;
                objectRenderer.enabled = active;

                timer = duration / (flashTimes * 2);
                remainingChanges--;
            }
        }

    }

    private void ShowAllHearts()
    {
        maxLife = actualHearts * 2;
        actualLife = maxLife;

        for (int i = 0; i < actualHearts; i++)
        {
            emptyHearts[i].SetActive(true);

            halfHearts[i * 2].SetActive(true);
            halfHearts[(i * 2) + 1].SetActive(true);
        }
    }

    public void GetHealed(int amount)
    {
        int previousLife = actualLife;
        actualLife += amount;

        if(actualLife > maxLife) { actualLife = maxLife; }

        for (int i = previousLife; i < actualLife; i++)
        {
            halfHearts[i ].SetActive(true);
        }
    }

    public void GetDamage(int damage)
    {
        int previousLife = actualLife;
        actualLife -= damage;
        if (actualLife <= 0) //Dead
        {
            Debug.Log("Dead"); 
        }
        else
        {
            for (int i = previousLife; i > actualLife; i--)
            {
                halfHearts[i - 1].SetActive(false);
            }
        }

        remainingChanges = flashTimes * 2;

        timer = duration / remainingChanges;
    }
}
