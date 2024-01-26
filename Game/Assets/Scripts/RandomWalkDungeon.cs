using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEditor.VersionControl;
using UnityEngine;

public class RandomWalkDungeon : AbstractDungeonGenerator
{
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    [SerializeField]
    protected int iterations = 10;
    [SerializeField]
    protected int walkLength = 10;
    [SerializeField]
    protected bool startRandomlyEachIteration = true;

    private void Start()
    {
        RunProceduralGeneration();
    }

    protected override void RunProceduralGeneration()
    {
        HashSet<Vector2Int> floorPositions = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, startPosition);

        tilemapVisualizer.Clear();
        tilemapVisualizer.PaintFloorTiles(floorPositions);

        WallGenerator.CreateWalls(floorPositions, tilemapVisualizer);
    }

    protected HashSet<Vector2Int> RunRandomWalk(int iterations_, int walkLength_, bool startRandomlyEachIteration_, Vector2Int position)
    {

        var currentPosition = position;
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();

        for (int i = 0; i < iterations_; i++)
        {
            var path = GenerationAlgorithm.SimpleRandomWalk(currentPosition, walkLength_);
            floorPositions.UnionWith(path);
            if (startRandomlyEachIteration_)
                currentPosition = floorPositions.ElementAt(UnityEngine.Random.Range(0, floorPositions.Count));
        }

        return floorPositions;
    }
}
