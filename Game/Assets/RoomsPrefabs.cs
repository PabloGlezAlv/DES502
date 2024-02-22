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

    public GameObject generateRandomRoom(Vector2Int center, ref List<Vector2Int> SpikesPositions)
    {
        //Make this random...
        int prefabSelection = Random.Range(0, prefabs.Count);


        //Spawn the object(Enemies)
        GameObject room = Instantiate(prefabs[prefabSelection]);
        room.transform.position = new Vector3(center.x, center.y, 0);

        //Spawn the traps
        TrapLocations traps = room.GetComponent<TrapLocations>();

        SpikesPositions = traps.GetSpikesFinalLocation(center);

        return room;
    }
}
