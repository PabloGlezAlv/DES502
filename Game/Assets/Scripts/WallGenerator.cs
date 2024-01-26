using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEditor.VersionControl;
using UnityEngine;
using UnityEngine.UIElements;

public class WallGenerator : MonoBehaviour
{
    public static void CreateWalls(HashSet<Vector2Int> floorPositions, TileMapVisualizer tilemapVisualizer)
    {
        var basicWallPositons = FindWallsInDirections(floorPositions, Direction2D.cardinalDirectionslist);
        foreach (var position in basicWallPositons)
        {
            tilemapVisualizer.PaintSingleBasicWall(position);
        }
    }

    private static HashSet<Vector2Int> FindWallsInDirections(HashSet<Vector2Int> floorPositions, List<Vector2Int> directionList)
    {
        HashSet<Vector2Int> wallPositions = new HashSet<Vector2Int>();

        foreach (var position in floorPositions)
        {
            foreach (var direction in directionList)
            {

                var neighbourPosition = position + direction;
                if (floorPositions.Contains(neighbourPosition) == false)
                    wallPositions.Add(neighbourPosition);
            }
        }

        return wallPositions;
    }
}
