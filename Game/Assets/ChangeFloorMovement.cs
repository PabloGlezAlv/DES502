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

    private void Awake()
    {
        cameraMov = Camera.main.gameObject.GetComponent<CameraMovement>();
        playerMovement = gameObject.GetComponent<PlayerMovement>();
    }


    private void OnEnable()
    {
        EndTransition();
    }
    // Update is called once per frame
    void Update()
    {

    }



    void EndTransition()
    {
        cameraMov.SetPosition(new Vector2Int());
        playerMovement.enabled = true;

        creator.generateNewLevel();
    }
}
