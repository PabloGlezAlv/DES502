using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using static UnityEditor.SceneView;

public class ChangeFloorMovement : MonoBehaviour
{
    [SerializeField]
    RoomCreator creator;

    [SerializeField]
    TilemapRenderer floor;

    [SerializeField]
    float force = 5;

    [SerializeField]
    Image fadeInOutImage;

    CameraMovement cameraMov;
    PlayerMovement playerMovement;

    Rigidbody2D rb;

    bool goMiddle = false;
    bool changedSpriteLayer = false;

    Vector3 targetPosition;

    Vector3 jumpPosition;

    bool fadingIn = false;
    bool fadingOut = false;

    private void Awake()
    {
        cameraMov = Camera.main.gameObject.GetComponent<CameraMovement>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();

        rb = gameObject.GetComponent<Rigidbody2D>();
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
                jumpPosition = transform.position;
                goMiddle = false;
                rb.velocity = new Vector3();

                rb.gravityScale = 1;
                
                rb.AddForce(Vector2.up * force, ForceMode2D.Impulse);

                fadingIn = true;
            }
        }
        else if(!changedSpriteLayer && transform.position.y < jumpPosition.y)
        {
            changedSpriteLayer = true;
            floor.sortingLayerName = "AbovePlayer";
        }
        else if (changedSpriteLayer && transform.position.y < jumpPosition.y - 2)
        {
            rb.velocity = new Vector2();
            rb.gravityScale = 0;
        }

        if(fadingIn) 
        {
            Color c = new Color(0,0,0, fadeInOutImage.color.a + Time.deltaTime / 2);
            fadeInOutImage.color = c;
            if(c.a >= 1)
            {
                Invoke("GenerateNextLevel", 0.3f);

                fadingIn = false;

            }
        }
        else if(fadingOut)
        {
            Color c = new Color(0, 0, 0, fadeInOutImage.color.a - Time.deltaTime / 2);
            fadeInOutImage.color = c;

            if(c.a <= 0)
            {
                StartNextLevel();
            }
        }

    }

    void GenerateNextLevel()
    {
        fadingOut = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector3();
        floor.sortingLayerName = "Floor";
        cameraMov.SetPosition(new Vector2Int());
        playerMovement.ResetPlayer();

        creator.generateNewLevel();
    }

    void StartNextLevel()
    {
        fadingOut = false;
        playerMovement.SetNormalMove(true);

        this.enabled = false;
    }
}
