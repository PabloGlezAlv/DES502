using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorGenerator : MonoBehaviour
{
    [Header("Generation parameters")]
    [SerializeField]
    public int nRooms = 10;

    [SerializeField]
    private GameObject[] rooms;


    private int endRoom;

    // Start is called before the first frame update
    void Start()
    {
        endRoom = Random.Range(1, nRooms);


        Instantiate(rooms[0]);
    }
}
