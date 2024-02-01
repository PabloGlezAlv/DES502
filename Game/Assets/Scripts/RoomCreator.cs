using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Numerics;
using UnityEngine;


public enum direction { none, top, bottom, left, right }


public class RoomCreator : MonoBehaviour
{
    [SerializeField]
    TileMapVisualizer visualizer;
    [SerializeField]
    GameObject player;

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

    //Room center - Doors
    private Dictionary<Vector2Int, List<direction>> roomsInfo = new Dictionary<Vector2Int, List<direction>>();

    void Start()
    {
        visualizer.Clear();

        //Draw Rooms
        roomsInfo = GenerationAlgorithm.SimpleRandomWalk(startPosition, width, height, stepsPerWalk, numberWalk);
        DrawRoom();

        //Set player
        player.transform.position = new UnityEngine.Vector3() { x = startPosition.x, y = startPosition.y };

        //Set camera
        Camera.main.transform.position = new UnityEngine.Vector3() { x = startPosition.x, y = startPosition.y, z = -10 };
    }


    private void DrawRoom()
    {
        foreach (var room in roomsInfo)
        {
            Vector2Int center = room.Key;

            //Draw floor
            List<Vector2Int> floorPositions = new List<Vector2Int>();

            for (int i = -width / 2 + center.x; i < width / 2 + center.x; i++)
            {
                for (int j = -height / 2 + center.y; j < height / 2 + center.y; j++)
                {
                    floorPositions.Add(new Vector2Int(i, j));

                    //Draw normal Walls
                    if (j == (height / 2) - 1 + +center.y) //Top walls
                    {
                        if((i == center.x || i == center.x - 1) && room.Value.Contains(direction.top))
                        {

                        }
                        else
                        {
                            visualizer.PaintSingleWall(new Vector2Int(i, j + 2), typeWall.top);
                            visualizer.PaintSingleWall(new Vector2Int(i, j + 1), typeWall.topInside);
                        }
                    }
                    else if (j == -height / 2 + center.y) //Bottom Wall
                    {
                        if ((i == center.x || i == center.x - 1) && room.Value.Contains(direction.bottom))
                        {

                        }
                        else
                        {
                            visualizer.PaintSingleWall(new Vector2Int(i, j - 1), typeWall.bottom);
                        }
                    }

                    if (i == -width / 2 + center.x) //Left Wall
                    {
                        if ((j == center.y || j == center.y - 1) && room.Value.Contains(direction.left))
                        {

                        }
                        else
                        {
                            visualizer.PaintSingleWall(new Vector2Int(i - 1, j), typeWall.left);
                        }
                    }
                    else if (i == width / 2 - 1 + center.x) //Right Wall
                    {
                        if ((j == center.y || j == center.y - 1) && room.Value.Contains(direction.right))
                        {

                        }
                        else
                        {
                            visualizer.PaintSingleWall(new Vector2Int(i + 1, j), typeWall.right);
                        }
                    }
                }
            }

            visualizer.PaintFloorTiles(floorPositions);

            //Draw corners top missing
            visualizer.PaintSingleWall(new Vector2Int(-width / 2 - 1 + center.x, height / 2 + center.y), typeWall.left);
            visualizer.PaintSingleWall(new Vector2Int(width / 2 + center.x, height / 2 + center.y), typeWall.right);

            //Draw corner walls
            Vector2Int topLeftCorner = new Vector2Int((-width / 2) - 1 + center.x, (height / 2) + 1 + center.y);
            visualizer.PaintSingleWall(topLeftCorner, typeWall.leftTopCorner);
            Vector2Int topRightCorner = new Vector2Int((width / 2 + center.x), (height / 2) + 1 + center.y);
            visualizer.PaintSingleWall(topRightCorner, typeWall.rightTopCorner);

            Vector2Int bottomLeftCorner = new Vector2Int((-width / 2) - 1 + center.x, (-height / 2) - 1 + center.y);
            visualizer.PaintSingleWall(bottomLeftCorner, typeWall.leftBottomCorner);
            Vector2Int bottomRightCorner = new Vector2Int((width / 2 + center.x), (-height / 2) - 1 + center.y);
            visualizer.PaintSingleWall(bottomRightCorner, typeWall.rightBottomCorner);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
