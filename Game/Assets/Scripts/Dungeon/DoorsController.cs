using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField]
    RoomCreator roomCreator;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            ChangeDoorsState(true);
        }
    }


    public void ChangeDoorsState(bool openDoors)
    {
        roomCreator.SetDoorRoom(openDoors);
    }
}
