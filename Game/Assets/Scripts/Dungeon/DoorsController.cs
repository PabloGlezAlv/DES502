using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField]
    RoomCreator roomCreator;

    int roomEntities = 0;


    // Start is called  the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AddEntity() { roomEntities++; }
    
    public void KillEntity()
    {
        roomEntities--;

        if (roomEntities == 0)
            ChangeDoorsState(true);
    }


    public void ChangeDoorsState(bool openDoors)
    {
        roomCreator.SetDoorRoom(openDoors);
    }
}
