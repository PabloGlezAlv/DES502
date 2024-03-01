using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using UnityEngine;


public enum Direction { None, Top, Bottom, Left, Right }

public struct doorsInfo
{
    public Vector2Int position;
    public bool leftRightPosition;

    public Direction dir;
}

public class RoomCreator : MonoBehaviour
{
    [SerializeField]
    TileMapVisualizer visualizer;
    [SerializeField]
    GameObject player;
    [SerializeField]
    ChangeRoom changeRoom;

    [SerializeField]
    SpikeLogic spikeTrap;

    [Header("Room Parameters")]
    [SerializeField]
    private int width = 20;
    [SerializeField]
    private int height = 10;
    [SerializeField]
    private Vector2Int startPosition = new(0, 0);

    [Header("Rooms GameObjects")]
    [SerializeField]
    private GameObject shop;
    [SerializeField]
    private GameObject hatch;

    [Header("Procedural Parameters")]
    [SerializeField]
    private int stepsPerWalk = 10;
    [SerializeField]
    private int numberWalk = 10;

    //Room center - Doors
    private Dictionary<Vector2Int, List<Direction>> roomsInfo = new();

    //Room center - Doors blocks
    private Dictionary<Vector2Int, List<doorsInfo>> doorsRoomsBlocks = new();

    //Room center - enemies
    private Dictionary<Vector2Int, GameObject> enemiesInstances = new();

    // Save rooms already visited
    List<Vector2Int> roomsVisited = new();

    List<Vector2Int> holesFloor = new();
    List<typeHole> holesSprite = new();


    private PlayerMovement playerMovement;

    private Vector2Int finalRoomCenter;

    private Vector2Int shopRoomCenter = new Vector2Int(3, 3);

    private RoomsPrefabs roomsPrefabs;

    private Vector2Int actualRoomCenter;
    private Vector2Int previousRoomCenter;

    private GameObject shopInstance;
    private GameObject nextFloorHatchInstance;

    void Start()
    {
        shopInstance = Instantiate(shop);
        nextFloorHatchInstance = Instantiate(hatch);

        visualizer.Clear();

        roomsPrefabs = GetComponent<RoomsPrefabs>();

        playerMovement = player.GetComponent<PlayerMovement>();

        GenerateDungeon();
    }

    private void GenerateDungeon()
    {
        shopInstance.transform.position = new UnityEngine.Vector3(1000,1000,0);

        nextFloorHatchInstance.transform.position = new UnityEngine.Vector3(-1000, 1000, 0);
        //Draw Rooms
        roomsInfo = GenerationAlgorithm.SimpleRandomWalk(startPosition, width, height, stepsPerWalk, numberWalk, ref finalRoomCenter, ref shopRoomCenter);
        DrawRoom();

        //Set player
        player.transform.position = new UnityEngine.Vector3() { x = startPosition.x, y = startPosition.y };

        //Set camera
        Camera.main.transform.position = new UnityEngine.Vector3() { x = startPosition.x, y = startPosition.y, z = -10 };


        actualRoomCenter = startPosition;
        previousRoomCenter = actualRoomCenter;

        SetDoorRoom(true);
    }

    public void generateNewLevel()
    {
        //Clean saved data
        visualizer.Clear();
        Clear();

        GenerateDungeon();
    }

    private void Clear()
    {
        roomsInfo.Clear();
        doorsRoomsBlocks.Clear();
        foreach (var room in enemiesInstances)
        {
            GameObject enemy = room.Value;
            if (enemy != null)
            {
                Destroy(enemy);
            }
        }
        enemiesInstances.Clear ();
        roomsVisited.Clear ();
        holesFloor.Clear();
        holesSprite.Clear();
        spikeTrap.Clear();
    }

    private bool IsOldRoom()
    {
        Vector2Int closerCenter = new Vector2Int();
        float distance = 10000;

        Vector2Int actualCenter = playerMovement.GetCurrentRoom();
        foreach (var room in roomsInfo)
        {
            float newDistance = Vector2Int.Distance(actualCenter, room.Key);
            if(newDistance < distance)
            {
                closerCenter = room.Key;
                distance = newDistance;
            }
        }

        if(roomsVisited.Contains(closerCenter))
            return true;
        else //Add the room if is new
        {
            roomsVisited.Add(closerCenter);
            return false;
        }
    }

    public void SetDoorRoom(bool open)
    {
        previousRoomCenter = actualRoomCenter;
        actualRoomCenter = playerMovement.GetCurrentRoom();

        //Check if havent been in room yet old room 
        if (IsOldRoom() && !open)
        {
            spikeTrap.SetNewRoom(false);
            return;
        }
        spikeTrap.SetNewRoom(true);

        GameObject actualRoom, previousRoom;
        //Activate enemies (Check if the rooms are not end or shop)
        if(enemiesInstances.ContainsKey(actualRoomCenter))
        {
            actualRoom = enemiesInstances[actualRoomCenter];
            actualRoom.SetActive(true);
        }
        if(enemiesInstances.ContainsKey(previousRoomCenter))
        {
            previousRoom = enemiesInstances[previousRoomCenter];
            previousRoom.SetActive(false);
        }


        foreach (var room in doorsRoomsBlocks)
        {
            foreach (var roomInfo in room.Value)
            {
                visualizer.PaintDoorTiles(roomInfo.position, open, roomInfo.dir, roomInfo.leftRightPosition); 
            }
        }

        visualizer.SetDoorCollider(open);
    }

    private void DrawRoom()
    {
        List<doorsInfo> doorsAllBlocks = new List<doorsInfo>();
        foreach (var room in roomsInfo)
        {
            Vector2Int center = room.Key;

            //Draw floor
            List<Vector2Int> floorPositions = new List<Vector2Int>();
            List<doorsInfo> doorsBlocks = new List<doorsInfo>();

            for (int i = -width / 2 + center.x; i < width / 2 + center.x; i++)
            {
                for (int j = -height / 2 + center.y; j < height / 2 + center.y; j++)
                {
                    floorPositions.Add(new Vector2Int(i, j));

                    center = DrawWalls(room, center, doorsBlocks, i, j);
                }
            }

            if(center == finalRoomCenter) //Dont draw int he spaces for the end
            {
                floorPositions.Remove(center);
                floorPositions.Remove(new Vector2Int(center.x - 1, center.y));
                floorPositions.Remove(new Vector2Int(center.x, center.y - 1));
                floorPositions.Remove(new Vector2Int(center.x - 1, center.y - 1));
            }

            //Save door Blocks
            doorsRoomsBlocks.Add(center, doorsBlocks);

            //Draw floor
            visualizer.PaintFloorTiles(floorPositions);

            //Draw normal wall corner missing
            DrawCorners(center);

            foreach (doorsInfo doorsBlock in doorsBlocks)
            {
                doorsAllBlocks.Add(doorsBlock);
            }

            //Add enemies and traps
            if(center != finalRoomCenter && center != shopRoomCenter && center != startPosition)
            {
                AddRoomData(center);
            }
        }
        //Draw holesblocks
        visualizer.PaintHoles(holesFloor, holesSprite);

        changeRoom.SaveDoors(doorsAllBlocks);


        nextFloorHatchInstance.transform.position = new UnityEngine.Vector3(finalRoomCenter.x, finalRoomCenter.y, 0);

        //Set the shop

        if (finalRoomCenter != shopRoomCenter)
        {
            shopInstance.transform.position = new UnityEngine.Vector3(shopRoomCenter.x, shopRoomCenter.y, 0);
        }

        roomsVisited.Add(shopRoomCenter);
        roomsVisited.Add(finalRoomCenter);
    }

    private void AddRoomData(Vector2Int center)
    {
        List<Vector2Int> spikesRoom = new List<Vector2Int>();

        GameObject roomEnemies = roomsPrefabs.generateRandomRoom(center, roomsInfo[center], ref spikesRoom, ref holesFloor, ref holesSprite);
        roomEnemies.SetActive(false);
        enemiesInstances.Add(center, roomEnemies);

        spikeTrap.AddPoints(spikesRoom);
    }

    private void DrawCorners(Vector2Int center)
    {
        visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 2 + center.x, height / 2 + center.y), typeWall.left);
        visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 1 + center.x, height / 2 + center.y + 1), typeWall.top);

        visualizer.PaintSingleWall(new Vector2Int(width / 2 + center.x + 1, height / 2 + center.y), typeWall.right);
        visualizer.PaintSingleWall(new Vector2Int(width / 2 + center.x, height / 2 + center.y + 1), typeWall.top);

        visualizer.PaintSingleWall(new Vector2Int((-width / 2) - 2 + center.x, (-height / 2) - 1 + center.y), typeWall.left);
        visualizer.PaintSingleWall(new Vector2Int((-width / 2) - 1 + center.x, (-height / 2) - 2 + center.y), typeWall.bottom);

        visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x) + 1, (-height / 2) - 1 + center.y), typeWall.right);
        visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x), (-height / 2) - 2 + center.y), typeWall.bottom);

        //Draw corner walls
        visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 2 + center.x, height / 2 + center.y + 1), typeWall.leftTopCorner);
        visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 1 + center.x, height / 2 + center.y), typeWall.insideLeftTopCorner);

        visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x + 1), (height / 2) + center.y + 1), typeWall.rightTopCorner);
        visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x), (height / 2) + center.y), typeWall.insideRightTopCorner);

        visualizer.PaintSingleWall(new Vector2Int((-width / 2) - 2 + center.x, (-height / 2) - 2 + center.y), typeWall.leftBottomCorner);
        visualizer.PaintSingleWall(new Vector2Int((-width / 2) - 1 + center.x, (-height / 2) - 1 + center.y), typeWall.insideLeftBottomCorner);

        visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x), (-height / 2) - 1 + center.y), typeWall.insideRightBottomCorner); 
        visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x + 1), (-height / 2) - 2 + center.y), typeWall.rightBottomCorner);
    }

    private Vector2Int DrawWalls(KeyValuePair<Vector2Int, List<Direction>> room, Vector2Int center, List<doorsInfo> doorsBlocks, int i, int j)
    {
        List<Vector2Int> corridorFloor = new List<Vector2Int>();

        //Draw normal Walls
        if (j == (height / 2) - 1 + +center.y) //Top walls
        {
            //Door
            if ((i == center.x || i == center.x - 1) && room.Value.Contains(Direction.Top))
            {
                bool left = (i == center.x - 1);
                visualizer.PaintDoorTiles(new Vector2Int(i, j + 1), false, Direction.Top, left);

                doorsInfo doorData = new doorsInfo();
                doorData.dir = Direction.Top;

                doorData.position = new Vector2Int(i, j + 1);
                doorData.leftRightPosition = (left);

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i, j + 2));
                corridorFloor.Add(new Vector2Int(i, j + 1));
            }
            else //Wall
            {
                visualizer.PaintSingleWall(new Vector2Int(i, j + 1), typeWall.topInside);
            }

            visualizer.PaintSingleWall(new Vector2Int(i, j + 2), typeWall.top);
        }
        else if (j == -height / 2 + center.y) //Bottom Wall
        {
            //Door
            if ((i == center.x || i == center.x - 1) && room.Value.Contains(Direction.Bottom))
            {
                bool left = (i == center.x - 1);
                visualizer.PaintDoorTiles(new Vector2Int(i, j - 1), false, Direction.Bottom, left);

                doorsInfo doorData = new doorsInfo();
                doorData.dir = Direction.Bottom;

                doorData.position = new Vector2Int(i, j - 1);
                doorData.leftRightPosition = left;

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i, j - 2));
                corridorFloor.Add(new Vector2Int(i, j - 1));
            }
            else //Wall
            {
                visualizer.PaintSingleWall(new Vector2Int(i, j - 1), typeWall.bottomInside);
            }

            visualizer.PaintSingleWall(new Vector2Int(i, j - 2), typeWall.bottom);
        }

        if (i == -width / 2 + center.x) //Left Wall
        {
            if ((j == center.y || j == center.y - 1) && room.Value.Contains(Direction.Left))
            {
                bool top = (j == center.y);
                visualizer.PaintDoorTiles(new Vector2Int(i - 1, j), false, Direction.Left, top);

                doorsInfo doorData = new doorsInfo();
                doorData.dir = Direction.Left;

                doorData.position = new Vector2Int(i - 1, j);
                doorData.leftRightPosition = (top);

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i - 1, j));
                corridorFloor.Add(new Vector2Int(i - 2, j));
            }
            else
            {
                visualizer.PaintSingleWall(new Vector2Int(i - 1, j), typeWall.leftInside);
            }

            visualizer.PaintSingleWall(new Vector2Int(i - 2, j), typeWall.left);
        }
        else if (i == width / 2 - 1 + center.x) //Right Wall
        {
            if ((j == center.y || j == center.y - 1) && room.Value.Contains(Direction.Right))
            {
                bool top = (j == center.y);
                visualizer.PaintDoorTiles(new Vector2Int(i + 1, j), false, Direction.Right, top);

                doorsInfo doorData = new doorsInfo();
                doorData.dir = Direction.Right;

                doorData.position = new Vector2Int(i + 1, j);
                doorData.leftRightPosition = top;

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i + 1, j));
                corridorFloor.Add(new Vector2Int(i + 2, j));
            }
            else
            {
                visualizer.PaintSingleWall(new Vector2Int(i + 1, j), typeWall.rightInside);
            }

            visualizer.PaintSingleWall(new Vector2Int(i + 2, j), typeWall.right);
        }

        visualizer.PaintFloorTiles(corridorFloor);

        return center;
    }
}
