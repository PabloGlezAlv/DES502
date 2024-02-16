using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using UnityEngine;


public enum direction { none, top, bottom, left, right }


public class RoomCreator : MonoBehaviour
{
    [SerializeField]
    TileMapVisualizer visualizer;
    [SerializeField]
    GameObject player;

    [SerializeField]
    SpikeLogic spikeTrap;

    [Header("Room Parameters")]
    [SerializeField]
    private int width = 20;
    [SerializeField]
    private int height = 10;
    [SerializeField]
    private Vector2Int startPosition = new Vector2Int(0, 0);

    [Header("Procedural Parameters")]
    [SerializeField]
    private int stepsPerWalk = 10;
    [SerializeField]
    private int numberWalk = 10;

    struct doorsInfo
    {
        public List<Vector2Int> position;

        public direction dir;
    }

    //Room center - Doors
    private Dictionary<Vector2Int, List<direction>> roomsInfo = new Dictionary<Vector2Int, List<direction>>();

    //Room center - Doors blocks
    private Dictionary<Vector2Int, List<doorsInfo>> doorsRoomsBlocks = new Dictionary<Vector2Int, List<doorsInfo>>();

    // Save rooms already visited
    List<Vector2Int> roomsVisited = new List<Vector2Int>();

    private PlayerMovement playerMovement;

    private Vector2Int finalRoomCenter;

    void Start()
    {
        visualizer.Clear();

        //Draw Rooms
        roomsInfo = GenerationAlgorithm.SimpleRandomWalk(startPosition, width, height, stepsPerWalk, numberWalk, ref finalRoomCenter);
        DrawRoom();
        Debug.Log(finalRoomCenter);
        //Set player
        player.transform.position = new UnityEngine.Vector3() { x = startPosition.x, y = startPosition.y };

        //Set camera
        Camera.main.transform.position = new UnityEngine.Vector3() { x = startPosition.x, y = startPosition.y, z = -10 };

        playerMovement = player.GetComponent<PlayerMovement>();


        //Trap to test
        spikeTrap.AddPoints(new Vector2Int(-7,0));
    }

    private bool isOldRoom()
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
        //Check if havent been in room yet old room 
        if (isOldRoom() && !open)
            return;

        foreach (var room in doorsRoomsBlocks)
        {
            foreach (var roomInfo in room.Value)
            {
                foreach (var roomBlocks in roomInfo.position)
                    visualizer.PaintDoorTiles(roomBlocks, open, roomInfo.dir);
            }
        }

        visualizer.setDoorCollider(open);
    }

    private void DrawRoom()
    {
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

            //Save door Blocks
            doorsRoomsBlocks.Add(center, doorsBlocks);

            //Draw floor
            visualizer.PaintFloorTiles(floorPositions);

            //Draw corners top missing
            visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 2 + center.x, height / 2 + center.y), typeWall.left);
            visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 1 + center.x, height / 2 + center.y + 1), typeWall.top);

            visualizer.PaintSingleWall(new Vector2Int(width / 2 + center.x + 1, height / 2 + center.y), typeWall.right);
            visualizer.PaintSingleWall(new Vector2Int(width / 2 + center.x, height / 2 + center.y + 1), typeWall.top);

            //Draw corner walls
            visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 2 + center.x, height / 2 + center.y + 1), typeWall.leftTopCorner);
            visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 1 + center.x, height / 2 + center.y), typeWall.leftTopCorner); //NEW BLOCK INTERIOR CORNERA

            visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x + 1), (height / 2) + center.y + 1), typeWall.rightTopCorner);
            visualizer.PaintSingleWall(new Vector2Int((width / 2 + center.x), (height / 2) + center.y), typeWall.rightTopCorner);//NEW INTERIOR BLOCK CORNERA

            Vector2Int bottomLeftCorner = new Vector2Int((-width / 2) - 1 + center.x, (-height / 2) - 1 + center.y);
            visualizer.PaintSingleWall(bottomLeftCorner, typeWall.leftBottomCorner);
            Vector2Int bottomRightCorner = new Vector2Int((width / 2 + center.x), (-height / 2) - 1 + center.y);
            visualizer.PaintSingleWall(bottomRightCorner, typeWall.rightBottomCorner);

        }

        visualizer.PaintNextFloorDoor(finalRoomCenter);
    }

    private Vector2Int DrawWalls(KeyValuePair<Vector2Int, List<direction>> room, Vector2Int center, List<doorsInfo> doorsBlocks, int i, int j)
    {
        List<Vector2Int> corridorFloor = new List<Vector2Int>();

        //Draw normal Walls
        if (j == (height / 2) - 1 + +center.y) //Top walls
        {
            //Door
            if ((i == center.x || i == center.x - 1) && room.Value.Contains(direction.top))
            {
                visualizer.PaintDoorTiles(new Vector2Int(i, j + 2), false, direction.top);
                visualizer.PaintDoorTiles(new Vector2Int(i, j + 1), false, direction.top);

                doorsInfo doorData = new doorsInfo();
                doorData.position = new List<Vector2Int>();
                doorData.dir = direction.top;

                doorData.position.Add(new Vector2Int(i, j + 2));
                doorData.position.Add(new Vector2Int(i, j + 1));

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i, j + 2));
                corridorFloor.Add(new Vector2Int(i, j + 1));
            }
            else //Wall
            {
                visualizer.PaintSingleWall(new Vector2Int(i, j + 2), typeWall.top);
                visualizer.PaintSingleWall(new Vector2Int(i, j + 1), typeWall.topInside);
            }
        }
        else if (j == -height / 2 + center.y) //Bottom Wall
        {
            //Door
            if ((i == center.x || i == center.x - 1) && room.Value.Contains(direction.bottom))
            {
                visualizer.PaintDoorTiles(new Vector2Int(i, j - 1), false, direction.bottom);
                visualizer.PaintDoorTiles(new Vector2Int(i, j - 2), false, direction.bottom);

                doorsInfo doorData = new doorsInfo();
                doorData.position = new List<Vector2Int>();
                doorData.dir = direction.bottom;

                doorData.position.Add(new Vector2Int(i, j - 1));
                doorData.position.Add(new Vector2Int(i, j - 2));

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i, j - 2));
                corridorFloor.Add(new Vector2Int(i, j - 1));
            }
            else //Wall
            {
                visualizer.PaintSingleWall(new Vector2Int(i, j - 1), typeWall.topInside);
                visualizer.PaintSingleWall(new Vector2Int(i, j - 2), typeWall.bottom);
            }
        }

        if (i == -width / 2 + center.x) //Left Wall
        {
            if ((j == center.y || j == center.y - 1) && room.Value.Contains(direction.left))
            {
                visualizer.PaintDoorTiles(new Vector2Int(i - 1, j), false, direction.left);
                visualizer.PaintDoorTiles(new Vector2Int(i - 2, j), false, direction.left);

                doorsInfo doorData = new doorsInfo();
                doorData.position = new List<Vector2Int>();
                doorData.dir = direction.left;

                doorData.position.Add(new Vector2Int(i - 1, j));
                doorData.position.Add(new Vector2Int(i - 2, j));

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i - 1, j));
                corridorFloor.Add(new Vector2Int(i - 2, j));
            }
            else
            {
                visualizer.PaintSingleWall(new Vector2Int(i - 1, j), typeWall.topInside);
                visualizer.PaintSingleWall(new Vector2Int(i - 2, j), typeWall.left);
            }
        }
        else if (i == width / 2 - 1 + center.x) //Right Wall
        {
            if ((j == center.y || j == center.y - 1) && room.Value.Contains(direction.right))
            {
                visualizer.PaintDoorTiles(new Vector2Int(i + 1, j), false, direction.right);
                visualizer.PaintDoorTiles(new Vector2Int(i + 2, j), false, direction.right);

                doorsInfo doorData = new doorsInfo();
                doorData.position = new List<Vector2Int>();
                doorData.dir = direction.right;

                doorData.position.Add(new Vector2Int(i + 1, j));
                doorData.position.Add(new Vector2Int(i + 2, j));

                doorsBlocks.Add(doorData);

                corridorFloor.Add(new Vector2Int(i + 1, j));
                corridorFloor.Add(new Vector2Int(i + 2, j));
            }
            else
            {
                visualizer.PaintSingleWall(new Vector2Int(i + 1, j), typeWall.topInside);
                visualizer.PaintSingleWall(new Vector2Int(i + 2, j), typeWall.right);
            }
        }

        visualizer.PaintFloorTiles(corridorFloor);

        return center;
    }
}
