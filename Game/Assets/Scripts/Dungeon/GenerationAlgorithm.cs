using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;


//Inspired: https://www.youtube.com/watch?v=nbi88hY9hcw&ab_channel=SunnyValleyStudio

public class GenerationAlgorithm : MonoBehaviour
{
    public static Dictionary<Vector2Int, List<direction>> SimpleRandomWalk(Vector2Int startPosition, int width, int height, int walkLength, int numberWalks)
    {
        Dictionary<Vector2Int, List<direction>> path = new Dictionary<Vector2Int, List<direction>>();
        path.Add(startPosition, new List<direction>());
        var previousPosition = startPosition;
        for(int i = 0; i < numberWalks;i++)
        {
            for (int j = 0; j < walkLength; j++)
            {
                Vector2Int newDir = Direction2D.GetRandomCardinalDirection();

                direction opposite = direction.none;
                bool included = true;
                if(path.ContainsKey(previousPosition))
                {
                    List<direction> existingVector = path[previousPosition];
                    if(newDir.x == 1 && !existingVector.Contains(direction.right)) //Right
                    {
                        existingVector.Add(direction.right);
                        opposite = direction.left;
                        included = false;
                    }
                    else if (newDir.x == -1 && !existingVector.Contains(direction.left)) // Left
                    {
                        existingVector.Add(direction.left);
                        opposite = direction.right;
                        included = false;
                    }
                    else if (newDir.y == 1 && !existingVector.Contains(direction.top)) // Up
                    {
                        existingVector.Add(direction.top);
                        opposite = direction.bottom;
                        included = false;
                    }
                    else if (newDir.y == -1 && !existingVector.Contains(direction.bottom)) // Down
                    {
                        existingVector.Add(direction.bottom);
                        opposite = direction.top;
                        included = false;
                    }

                    path.Remove(previousPosition);
                    path.Add(previousPosition, existingVector);
                }
                else
                {
                    included = false;
                    List<direction> newVector = new List<direction>();
                    if (newDir.x == 1 ) //Right
                    {
                        newVector.Add(direction.right);
                        path.Add(previousPosition, newVector);
                    }
                    else if (newDir.x == -1 ) // Left
                    {
                        newVector.Add(direction.left);
                        path.Add(previousPosition, newVector);
                    }
                    else if (newDir.y == 1 ) // Up
                    {
                        newVector.Add(direction.top);
                        path.Add(previousPosition, newVector);
                    }
                    else if (newDir.y == -1) // Down
                    {
                        newVector.Add(direction.bottom);
                        path.Add(previousPosition, newVector);
                    }
                }

                int wallsValueX = 0; //Right left +- 2 top buttom +- 3 
                int wallsValueY = 0;
                if (newDir.x == 1)
                    wallsValueX = 2;
                else if (newDir.y == 1)
                    wallsValueY = 3;
                else if (newDir.x == -1)
                    wallsValueX = -2;
                else
                    wallsValueY = -3;


                var newPosition = previousPosition + new Vector2Int((width * newDir.x) + wallsValueX, (height * newDir.y) + wallsValueY);
                previousPosition = newPosition;

                if (path.ContainsKey(previousPosition) && !included)
                {
                    List<direction> existingVector = path[previousPosition];
                    
                    if(!existingVector.Contains(opposite))
                    {
                        existingVector.Add(opposite);

                        path.Remove(previousPosition);
                        path.Add(previousPosition, existingVector);
                    }
                }
                else if(!included)
                {
                    List<direction> newVector = new List<direction>();
                    newVector.Add(opposite);
                    path.Add(previousPosition, newVector);
                }
            }

            previousPosition = startPosition;
        }

        return path;
    }

    public static List<Vector2Int> RandomWalkCorridor(Vector2Int startPosition, int corridorLength)
    {
        List<Vector2Int > corridor = new List<Vector2Int>();
        var direction = Direction2D.GetRandomCardinalDirection();
        var currentPosition = startPosition;
        corridor.Add(currentPosition);
        for (int i = 0; i < corridorLength; i++)
        {
            currentPosition += direction;
            corridor.Add(currentPosition);

        }
        return corridor; 
    }

    public static List<BoundsInt> BinarySpacePartitioning(BoundsInt spaceToSplit, int minWidth, int minHeight)
    {
        Queue<BoundsInt> roomsQueue = new Queue<BoundsInt>();
        List<BoundsInt> roomsList = new List<BoundsInt>();
        roomsQueue.Enqueue(spaceToSplit);
        while (roomsQueue.Count > 0)
        {
            var room = roomsQueue.Dequeue();
            if (room.size.y >= minHeight && room.size.x >= minWidth)
            {
                if (UnityEngine.Random.value < 0.5f)
                {
                    if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
                else
                {
                    if (room.size.x >= minWidth * 2)
                    {
                        SplitVertically(minWidth, roomsQueue, room);
                    }
                    else if (room.size.y >= minHeight * 2)
                    {
                        SplitHorizontally(minHeight, roomsQueue, room);
                    }
                    else if (room.size.x >= minWidth && room.size.y >= minHeight)
                    {
                        roomsList.Add(room);
                    }
                }
            }
        }
        return roomsList;
    }

    private static void SplitVertically(int minWidth, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var xSplit = UnityEngine.Random.Range(1, room.size.x);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(xSplit, room.size.y, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x + xSplit, room.min.y, room.min.z),
            new Vector3Int(room.size.x - xSplit, room.size.y, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }

    private static void SplitHorizontally(int minHeight, Queue<BoundsInt> roomsQueue, BoundsInt room)
    {
        var ySplit = UnityEngine.Random.Range(1, room.size.y);
        BoundsInt room1 = new BoundsInt(room.min, new Vector3Int(room.size.x, ySplit, room.size.z));
        BoundsInt room2 = new BoundsInt(new Vector3Int(room.min.x, room.min.y + ySplit, room.min.z),
            new Vector3Int(room.size.x, room.size.y - ySplit, room.size.z));
        roomsQueue.Enqueue(room1);
        roomsQueue.Enqueue(room2);
    }
}

public static class Direction2D
{
    public static List<Vector2Int> cardinalDirectionslist = new List<Vector2Int>
    {
        new Vector2Int(0, 1), //UP
        new Vector2Int(1, 0), //RIGHT
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, 0) //LEFT
    };

    public static List<Vector2Int> diagonalDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 1) //LEFT-UP
    };

    public static List<Vector2Int> eightDirectionsList = new List<Vector2Int>
    {
        new Vector2Int(0,1), //UP
        new Vector2Int(1,1), //UP-RIGHT
        new Vector2Int(1,0), //RIGHT
        new Vector2Int(1,-1), //RIGHT-DOWN
        new Vector2Int(0, -1), // DOWN
        new Vector2Int(-1, -1), // DOWN-LEFT
        new Vector2Int(-1, 0), //LEFT
        new Vector2Int(-1, 1) //LEFT-UP

    };
    public static Vector2Int GetRandomCardinalDirection()
    {

        return cardinalDirectionslist[UnityEngine.Random.Range(0, cardinalDirectionslist.Count)];
    }
}
