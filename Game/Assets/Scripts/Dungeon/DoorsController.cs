using UnityEngine;

public class DoorsController : MonoBehaviour
{
    [SerializeField]
    RoomCreator roomCreator;

    public int roomEntities = 0;
    public static int CurrentArea = 0;

    public void AddEntity() { roomEntities++; }
    
    public void KillEntity()
    {
        roomEntities--;

        if (roomEntities == 0)
            ChangeDoorsState(true);
    }


    public void ChangeDoorsState(bool openDoors)
    {
        if (CurrentArea == (int)TilemapType.Castle)
        {
            if (openDoors) AudioManager.instance.Play("OpenDoor");
            else AudioManager.instance.Play("CloseDoor");
        }
        else if (CurrentArea == (int)TilemapType.Market)
        {
            if (openDoors) AudioManager.instance.Play("MarketDoorOpen");
            else AudioManager.instance.Play("MarketDoorClose");
        }
        roomCreator.SetDoorRoom(openDoors);
    }
}
