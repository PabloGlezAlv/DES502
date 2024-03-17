using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField]
    RoomCreator roomCreator;

    public int roomEntities = 0;

    public void AddEntity() { roomEntities++; }
    
    public void KillEntity()
    {
        roomEntities--;

        if (roomEntities == 0)
            ChangeDoorsState(true);
    }


    public void ChangeDoorsState(bool openDoors)
    {
        if (openDoors) AudioManager.instance.Play("OpenDoor");
        else AudioManager.instance.Play("CloseDoor");
        roomCreator.SetDoorRoom(openDoors);
    }
}
