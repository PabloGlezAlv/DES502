using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using static UnityEditor.SceneView;

public class ChangeFloorMovement : MonoBehaviour
{
    [SerializeField]
    RoomCreator creator;

    CameraMovement cameraMov;
    PlayerMovement playerMovement;

    Rigidbody2D rb;

    bool goMiddle = false;
    Vector3 targetPosition;

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
                goMiddle = false;
            }
        }
        else
        {
            EndTransition();
        }
    }



    void EndTransition()
    {
        cameraMov.SetPosition(new Vector2Int());
        playerMovement.SetNormalMove(true);

        creator.generateNewLevel();

        this.enabled = false;
    }
}
