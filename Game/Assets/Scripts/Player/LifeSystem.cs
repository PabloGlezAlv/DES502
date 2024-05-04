using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LifeSystem : MonoBehaviour
{
    [Header("Parameters Life")]
    [SerializeField]
    private int startHearts = 5;
    [SerializeField]
    private int maxHearts = 10;
    [SerializeField]
    private int heartDistance = 10;
    [SerializeField]
    private float damageCoolDown = 0.5f;

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


    private float timerReceiveDamage = 0;

    void Start()
    {
        actualHearts = startHearts;
        maxLife = actualHearts * 2;
        ShowAllHearts();

        spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        objectRenderer = GetComponentInChildren<ItemManager>().gameObject.GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        timerReceiveDamage-= Time.deltaTime;
        if (remainingChanges > 0) 
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
        else
        {
            active = true;
            spriteRenderer.enabled = active;
            objectRenderer.enabled = active;
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
        //AudioManager.instance.Play("PlayerHeal");

        int previousLife = actualLife;
        actualLife += amount;

        if(actualLife > maxLife) { actualLife = maxLife; }

        for (int i = previousLife; i < actualLife; i++)
        {
            halfHearts[i].SetActive(true);
        }
    }

    public void GetDamage(int damage)
    {
        if(timerReceiveDamage > 0) { return; }

        timerReceiveDamage = damageCoolDown;
        int previousLife = actualLife;
        actualLife -= damage;
        AudioManager.instance.Play("PlayerHurt");
        if (actualLife <= 0) //Dead
        {
            Debug.Log("Dead");
            GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().ChangeMusic(GameTracks.Death);
            SceneManager.LoadScene("DeadScene", LoadSceneMode.Single);
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
