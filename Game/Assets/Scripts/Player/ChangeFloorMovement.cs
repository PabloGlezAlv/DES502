using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

public class ChangeFloorMovement : MonoBehaviour
{
    [SerializeField]
    RoomCreator creator;

    [SerializeField]
    TilemapRenderer floor;

    [SerializeField]
    float force = 5;
    [SerializeField]
    int lastLevel = 15;

    [SerializeField]
    UnityEngine.UI.Image fadeInOutImage;
    [SerializeField]
    TextMeshProUGUI text;

    CameraMovement cameraMov;
    PlayerMovement playerMovement;

    Rigidbody2D rb;

    bool goMiddle = false;
    bool changedSpriteLayer = false;

    Vector3 targetPosition;

    Vector3 jumpPosition;

    bool fadingIn = false;
    bool fadingOut = false;
    bool showText = true;

    Vector3 startPos;

    int level = 0;

    float textActive = 0;
    SpriteRenderer bottomHatch;
    SpriteRenderer topHatch;

    Animator animator;

    float startTime;
    private void Awake()
    {
        cameraMov = Camera.main.gameObject.GetComponent<CameraMovement>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        animator = gameObject.GetComponent<Animator>();

        rb = gameObject.GetComponent<Rigidbody2D>();

        startPos = fadeInOutImage.transform.position;
    }

    public void setTargetPostion(Vector3 target)
    {
        targetPosition = target;
    }

    private void OnEnable()
    {
        goMiddle = true;
        changedSpriteLayer = false;
        fadingIn = false;
        fadingOut = false;
        showText = false;
        level++;
        textActive = 0;
        fadeInOutImage.transform.position = startPos;

        animator.SetBool("jump", true);

        bottomHatch = GameObject.FindGameObjectWithTag("bottomHatch").GetComponent<SpriteRenderer>();
        topHatch = bottomHatch.transform.parent.GetComponent<SpriteRenderer>();

        startTime = Time.realtimeSinceStartup;
    }
    // Update is called once per frame
    void FixedUpdate()
    {
        if(goMiddle) //SetPlayer in the center of hatch
        {
            Vector3 direccion = (targetPosition - transform.position).normalized;
            Vector3 fuerzaMovimiento = direccion * 5;
            rb.AddForce(fuerzaMovimiento);

            if (Vector3.Distance(transform.position, targetPosition) < 0.1)
            {
                
                startTime = Time.realtimeSinceStartup;
                jumpPosition = transform.position;
                goMiddle = false;
                rb.velocity = new Vector3();

                rb.gravityScale = 1;
                
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

                Invoke("StartFadeIn", 1f);
            }
        }
        else if(!changedSpriteLayer && transform.position.y < jumpPosition.y)
        {
            changedSpriteLayer = true;
            floor.sortingLayerName = "AbovePlayer";
            bottomHatch.sortingLayerName = "AbovePlayer";
            topHatch.sortingLayerName = "AbovePlayer";
        }
        else if (changedSpriteLayer && transform.position.y < jumpPosition.y - 2)
        {
            rb.velocity = new Vector2();
            rb.gravityScale = 0;
        }

        if(fadingIn) 
        {
            fadeInOutImage.gameObject.transform.Translate(new Vector3(0, -startPos.y * Time.deltaTime,0));

            if(fadeInOutImage.gameObject.transform.position.y >= -startPos.y)
            {
                Invoke("GenerateNextLevel", 1f);
                textActive = 0.8f;
                text.text = "Level " + level;
                text.gameObject.SetActive(true);
                fadingIn = false;

            }
        }
        else if(fadingOut)
        {
            fadeInOutImage.gameObject.transform.Translate(new Vector3(0, -startPos.y * Time.deltaTime, 0));

            if (fadeInOutImage.gameObject.transform.position.y >= -startPos.y * 4)
            {
                StartNextLevel();
            }
        }

        if(textActive > 0)
        {
            textActive -= Time.fixedDeltaTime;
            if(textActive <= 0)
            {
                text.gameObject.SetActive(false);
            }
        }
    }

    void StartFadeIn()
    {
        fadingIn = true;
    }

    void GenerateNextLevel()
    {
        fadingOut = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector3();
        floor.sortingLayerName = "BelowPlayer";
        bottomHatch.sortingLayerName = "BelowPlayer";
        topHatch.sortingLayerName = "BelowPlayer";
        cameraMov.SetPosition(new Vector2Int());
        playerMovement.ResetPlayer();
        if (level != lastLevel)
        {
            creator.generateNewLevel();
        }
    }

    void StartNextLevel()
    {
        fadingOut = false;
        playerMovement.SetNormalMove(true);

        this.enabled = false;
    }
}
