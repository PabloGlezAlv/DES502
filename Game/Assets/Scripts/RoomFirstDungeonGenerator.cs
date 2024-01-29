using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public enum direction { right, left, top, bottom }
public class RoomFirstDungeonGenerator : RandomWalkDungeon
{

    [SerializeField]
    private int minRoomWidth = 4, minRoomHeight = 4;
    [SerializeField]
    private int dungeonWidth = 20, dungeonHeight = 20;
    [SerializeField]
    [Range(0, 10)]
    private int offset = 1;
    [SerializeField]
    private bool randomWalkRooms = false;


    //Data
    //KEY - room center, VALUE - List of point of the room
    private Dictionary<Vector2Int, HashSet<Vector2Int>> roomsInfo =
        new Dictionary<Vector2Int, HashSet<Vector2Int>>();

    private HashSet<Vector2Int> floorPosition, corridorPosition;

    //KEY - door Position, VALUE - Location in the room
    private Dictionary<Vector2Int, direction> doorPosition =
        new Dictionary<Vector2Int, direction>();
    protected override void RunProceduralGeneration()
    {
        CreateRooms();
    }

    private void CreateRooms()
    {
        var roomsList = GenerationAlgorithm.BinarySpacePartitioning(new BoundsInt((Vector3Int)startPosition, new Vector3Int(dungeonWidth, dungeonHeight, 0)), minRoomWidth, minRoomHeight);

        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();

        if (randomWalkRooms)
        {
            floor = CreateRoomsRandomly(roomsList);
        }
        else
        {
            floor = CreateSimpleRooms(roomsList);
        }
        //Save only room positions
        floorPosition = floor;

        //Create corridors
        List<Vector2Int> roomCenters = new List<Vector2Int>();
        foreach (var room in roomsList)
        {
            roomCenters.Add((Vector2Int)Vector3Int.RoundToInt(room.center));
        }
        HashSet<Vector2Int> corridors = ConnectRooms(roomCenters);

        //Save corridors data
        corridorPosition = new HashSet<Vector2Int>(corridors);
        SaveCorridorData(); //Fix corridor inside room and see where are the doors

        //Draw corridors floor
        tilemapVisualizer.PaintCorridorTiles(corridorPosition);

        //Draw rooms floor
        tilemapVisualizer.PaintFloorTiles(floor);

        //Draw doors
        tilemapVisualizer.PaintDoorTiles(doorPosition);

        //Draw walls
        floor.UnionWith(corridorPosition);
        WallGenerator.CreateWalls(floor, tilemapVisualizer);

        //Set player middle first room
        player.transform.position = roomsList.First().center;

        Camera.main.transform.position = new Vector3(roomsList.First().center.x, roomsList.First().center.y, Camera.main.transform.position.z);
    }

    private void SaveCorridorData()
    {
        //Clean data from previous dungeon
        HashSet<Vector2Int> onlyCorridor = new HashSet<Vector2Int>();
        doorPosition.Clear();

        // Itera sobre cada elemento en corridorPosition
        foreach (Vector2Int position in corridorPosition)
        {
            // Comprueba si el elemento no está en floorPosition
            if (floorPosition != null && !floorPosition.Contains(position))
            {
                onlyCorridor.Add(position);

                SaveDoorPosition(position);
            }
        }

        Debug.Log("Doors: " + doorPosition.Count());

        corridorPosition = onlyCorridor;
    }

    private void SaveDoorPosition(Vector2Int position)
    {
        Vector2Int nextPosition = position;
        nextPosition.x++;
        if (floorPosition.Contains(nextPosition))
        {
            doorPosition.Add(position, direction.left);
            return;
        }
        nextPosition = position;
        nextPosition.x--;
        if (floorPosition.Contains(nextPosition))
        {
            doorPosition.Add(position, direction.right);
            return;
        }
        nextPosition = position;
        nextPosition.y++;
        if (floorPosition.Contains(nextPosition))
        {
            doorPosition.Add(position, direction.bottom);
            return;
        }
        nextPosition = position;
        nextPosition.y--;
        if (floorPosition.Contains(nextPosition))
        {
            doorPosition.Add(position, direction.top);
            return;
        }
    }

    private HashSet<Vector2Int> CreateRoomsRandomly(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        HashSet<Vector2Int> floorPerRoom = new HashSet<Vector2Int>();
        for (int i = 0; i < roomsList.Count; i++)
        {
            var roomBounds = roomsList[i];
            var roomCenter = new Vector2Int(Mathf.RoundToInt(roomBounds.center.x), Mathf.RoundToInt(roomBounds.center.y));

            var roomFloor = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, roomCenter);
            foreach (var position in roomFloor)
            {
                if (position.x >= (roomBounds.xMin + offset) && position.x <= (roomBounds.xMax - offset) && position.y >= (roomBounds.yMin - offset) && position.y <= (roomBounds.yMax - offset))
                {
                    floor.Add(position);
                    floorPerRoom.Add(position);
                }
            }

            //Save floor of each room
            SaveRoomData(roomCenter, floorPerRoom);

            floorPerRoom.Clear();
        }
        return floor;
    }

    private void SaveRoomData(Vector2Int roomPosition, HashSet<Vector2Int> roomFloor)
    {
        roomsInfo[roomPosition] = roomFloor;
    }

    private HashSet<Vector2Int> ConnectRooms(List<Vector2Int> roomCenters)
    {
        HashSet<Vector2Int> corridors = new HashSet<Vector2Int>();
        var currentRoomCenter = roomCenters[UnityEngine.Random.Range(0, roomCenters.Count)];
        roomCenters.Remove(currentRoomCenter);

        while (roomCenters.Count > 0)
        {
            Vector2Int closest = FindClosestPointTo(currentRoomCenter, roomCenters);
            roomCenters.Remove(closest);
            HashSet<Vector2Int> newCorridor = CreateCorridor(currentRoomCenter, closest);
            currentRoomCenter = closest;

            
            corridors.UnionWith(newCorridor);
        }
        return corridors;
    }

    private HashSet<Vector2Int> CreateCorridor(Vector2Int currentRoomCenter, Vector2Int destination)
    {
        HashSet<Vector2Int> corridor = new HashSet<Vector2Int>();
        var position = currentRoomCenter;
        corridor.Add(position);
        while (position.y != destination.y)
        {
            if (destination.y > position.y)
            {
                position += Vector2Int.up;
            }
            else if (destination.y < position.y)
            {
                position += Vector2Int.down;
            }
            corridor.Add(position);
        }
        while (position.x != destination.x)
        {
            if (destination.x > position.x)
            {
                position += Vector2Int.right;
            }
            else if (destination.x < position.x)
            {
                position += Vector2Int.left;
            }
            corridor.Add(position);
        }

        return corridor;
    }
    private Vector2Int FindClosestPointTo(Vector2Int currentRoomCenter, List<Vector2Int> roomCenters)
    {
        Vector2Int closest = Vector2Int.zero;
        float distance = float.MaxValue;
        foreach (var position in roomCenters)
        {
            float currentDistance = Vector2.Distance(position, currentRoomCenter);
            if (currentDistance < distance)
            {
                distance = currentDistance;
                closest = position;
            }
        }
        return closest;
    }

    private HashSet<Vector2Int> CreateSimpleRooms(List<BoundsInt> roomsList)
    {
        HashSet<Vector2Int> floor = new HashSet<Vector2Int>();
        HashSet<Vector2Int> floorPerRoom = new HashSet<Vector2Int>();
        foreach (var room in roomsList)
        {
            var roomCenter = new Vector2Int(Mathf.RoundToInt(room.center.x), Mathf.RoundToInt(room.center.y));

            for (int col = offset; col < room.size.x - offset; col++)
            {
                for (int row = offset; row < room.size.y - offset; row++)
                {
                    Vector2Int position = (Vector2Int)room.min + new Vector2Int(col, row);
                    floor.Add(position);
                }
            }

            //Save floor of each room
            SaveRoomData(roomCenter, floorPerRoom);

            floorPerRoom.Clear();
        }
        return floor;
    }
}
