using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;


public class RoomsPrefabs : MonoBehaviour
{
    [Header("Prefabs")]
    [SerializeField]
    private List<GameObject> prefabs = new List<GameObject>();
    [SerializeField]
    private List<List<Direction>> emptyDoor = new List<List<Direction>>();

    public void Awake()
    {
        //Create doors specifications

        //----------Room 1----------------------
        List<Direction> room1 = new List<Direction>();
        //room1.Add(Direction.Right);
        emptyDoor.Add(room1);

        //----------Room 2----------------------
        List<Direction> room2 = new List<Direction>();
        //room2.Add(Direction.Right);
        emptyDoor.Add(room2);

        //----------Room 3----------------------
        List<Direction> room3 = new List<Direction>();
        //room2.Add(Direction.Right);
        emptyDoor.Add(room3);
        
        //----------Room 4----------------------
        List<Direction> room4 = new List<Direction>();
        //room2.Add(Direction.Right);
        emptyDoor.Add(room4);
        
        //----------Room 5----------------------
        List<Direction> room5 = new List<Direction>();
        //room2.Add(Direction.Right);
        emptyDoor.Add(room5);
        
        //----------Room 6----------------------
        List<Direction> room6 = new List<Direction>();
        //room2.Add(Direction.Right);
        emptyDoor.Add(room6);


        //----------Room 13---------------------
        List<Direction> room13 = new List<Direction>();
        room13.Add(Direction.Top);
        room13.Add(Direction.Bottom);
        emptyDoor.Add(room13);
        //----------Room 14---------------------
        List<Direction> room14 = new List<Direction>();
        room14.Add(Direction.Top);
        room14.Add(Direction.Bottom);
        emptyDoor.Add(room14);
        //----------Room 15---------------------
        List<Direction> room15 = new List<Direction>();
        room15.Add(Direction.Right);
        room15.Add(Direction.Bottom);
        emptyDoor.Add(room15);
        //----------Room 16---------------------
        List<Direction> room16 = new List<Direction>();
        room16.Add(Direction.Bottom);
        emptyDoor.Add(room16);

    }

    public GameObject generateRandomRoom(Vector2Int center, List<Direction> roomsDirections, ref List<Vector2Int> spikesPositions, ref List<Vector2Int> holesPositions, 
        ref List<typeHole> holesSprites)
    {
        //Make this random...
        int prefabSelection;
        do
        {
            prefabSelection = Random.Range(0, prefabs.Count);
        } while (MatchesDoor(prefabSelection, roomsDirections));




        //Spawn the object(Enemies)
        GameObject room = Instantiate(prefabs[prefabSelection]);
        room.transform.position = new Vector3(center.x, center.y, 0);

        //Spawn the traps
        TrapLocations traps = room.GetComponent<TrapLocations>();

        spikesPositions = traps.GetSpikesFinalLocation(center);

        List<Vector2Int> holes = traps.GetHolesFinalLocation(center);
        for (int i = 0; i < holes.Count; i ++)
        {
            holesPositions.Add(holes[i]);
        }
        List<typeHole> roomHoleSprites = traps.GetSpriteHoles();
        holesSprites.AddRange(roomHoleSprites);

        return room;
    }

    private bool MatchesDoor(int index, List<Direction> roomsDirections)
    {
        for(int i = 0; i < emptyDoor[index].Count;i++)
        {
            if (roomsDirections.Contains(emptyDoor[index][i]))
                return true;
        }

        return false;
    }
}
