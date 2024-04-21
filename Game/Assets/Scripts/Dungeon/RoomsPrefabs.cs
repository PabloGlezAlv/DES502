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

        //----------Room 17---------------------
        List<Direction> room17 = new List<Direction>();
        emptyDoor.Add(room17);

        //----------Room 18---------------------
        List<Direction> room18 = new List<Direction>();
        emptyDoor.Add(room18);

        //----------Room 19---------------------
        List<Direction> room19 = new List<Direction>();
        emptyDoor.Add(room19);

        //----------Room 20---------------------
        List<Direction> room20 = new List<Direction>();
        emptyDoor.Add(room20);

        //----------Room 21---------------------
        List<Direction> room21 = new List<Direction>();
        emptyDoor.Add(room21);

        //----------Room 22---------------------
        List<Direction> room22 = new List<Direction>();
        emptyDoor.Add(room22);

        //----------Room 23---------------------
        List<Direction> room23 = new List<Direction>();
        emptyDoor.Add(room23);

        //----------Room 24---------------------
        List<Direction> room24 = new List<Direction>();
        emptyDoor.Add(room24);

        //----------Room 25---------------------
        List<Direction> room25 = new List<Direction>();
        emptyDoor.Add(room25);

        //----------Room 26---------------------
        List<Direction> room26 = new List<Direction>();
        emptyDoor.Add(room26);

        //----------Room 27---------------------
        List<Direction> room27 = new List<Direction>();
        emptyDoor.Add(room27);

        //----------Room 28---------------------
        List<Direction> room28 = new List<Direction>();
        emptyDoor.Add(room28);

        //----------Room 29---------------------
        List<Direction> room29 = new List<Direction>();
        emptyDoor.Add(room29);

        //----------Room 30---------------------
        List<Direction> room30 = new List<Direction>();
        emptyDoor.Add(room30);

        //----------Room 31---------------------
        List<Direction> room31 = new List<Direction>();
        emptyDoor.Add(room31);

        //----------Room 32---------------------
        List<Direction> room32 = new List<Direction>();
        emptyDoor.Add(room32);

        //----------Room 33---------------------
        List<Direction> room33 = new List<Direction>();
        emptyDoor.Add(room33);

        //----------Room 34---------------------
        List<Direction> room34 = new List<Direction>();
        emptyDoor.Add(room34);

        //----------Room 35---------------------
        List<Direction> room35 = new List<Direction>();
        emptyDoor.Add(room35);

        //----------Room 36---------------------
        List<Direction> room36 = new List<Direction>();
        emptyDoor.Add(room36);

        //----------Room 37---------------------
        List<Direction> room37 = new List<Direction>();
        emptyDoor.Add(room37);

        //----------Room 38---------------------
        List<Direction> room38 = new List<Direction>();
        emptyDoor.Add(room38);

        //----------Room 39---------------------
        List<Direction> room39 = new List<Direction>();
        emptyDoor.Add(room39);

        //----------Room 40---------------------
        List<Direction> room40 = new List<Direction>();
        emptyDoor.Add(room40);
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
        if (emptyDoor[index].Count == 0) return false; //No door specification -> Room can go everywhere

        for (int i = 0; i < emptyDoor[index].Count; i++)
        {
            if (roomsDirections.Contains(emptyDoor[index][i]))
                return true;
        }

        return false;
    }
}
