using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UIElements;

public class CorridorDungeonGenerator : RandomWalkDungeon
{
    [SerializeField]
    private int corridorLength = 14, corridorCount = 5;
    [SerializeField]
    [Range(0.1f, 1)]
    private float roomPercent = 0.8f;
    protected override void RunProceduralGeneration()
    {
        CorridorFirstGeneration();
    }

    private void CorridorFirstGeneration()
    {
        HashSet<Vector2Int> floorPositions = new HashSet<Vector2Int>();
        HashSet<Vector2Int> potentialRoomsPositions = new HashSet<Vector2Int>();


        List<List<Vector2Int>> corridors = CreateCorridors(floorPositions, potentialRoomsPositions);

        HashSet<Vector2Int> roomPositions = CreateRooms(potentialRoomsPositions);

        List<Vector2Int > deadEnds = FindAllDeadEnds(floorPositions);

        CreateRoomsDeadEnds(deadEnds, roomPositions);

        floorPositions.UnionWith(roomPositions);

        //Make longer corridors
        for (int i = 0; i < corridors.Count; i++)
        {
            corridors[i] = IncreaseCorridorBrush3by3(corridors[i]);
            floorPositions.UnionWith(corridors[i]);
        }

        tilemapVisualizer.PaintFloorTiles(floorPositions);
        WallGenerator.CreateWalls(floorPositions,tilemapVisualizer);
    }

    private List<Vector2Int> IncreaseCorridorBrush3by3(List<Vector2Int> corridor)
    {
        List<Vector2Int> newCorridor = new List<Vector2Int>();
        for(int i = 1; i < corridor.Count; i++)
        {
            for (int x = -1; x < 2; x++)
            {
                for (int y = -1; y < 2; y++)
                {
                    newCorridor.Add(corridor[i - 1] + new Vector2Int(x, y));
                }
            }
        }
        return newCorridor;
    }

    private void CreateRoomsDeadEnds(List<Vector2Int> deadEnds, HashSet<Vector2Int> roomFloors)
    {
        foreach (var position in deadEnds)
        {
            if (roomFloors.Contains(position) == false)
            {
                var room = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, position);
                roomFloors.UnionWith(room);

            }

        }
    }

    private List<Vector2Int> FindAllDeadEnds(HashSet<Vector2Int> floorPositions)
    {
        List<Vector2Int> deadEnds = new List<Vector2Int>();
        foreach (var position in floorPositions)
        {
            int neighboursCount = 0;

            foreach (var direction in Direction2D.cardinalDirectionslist)
            {

                if (floorPositions.Contains(position + direction))
                    neighboursCount++;
            }

            if (neighboursCount == 1)
                deadEnds.Add(position);
        }
        return deadEnds;
    }

    private HashSet<Vector2Int> CreateRooms(HashSet<Vector2Int> potentialRoomsPositions)
    {
        HashSet<Vector2Int> roomPositions = new HashSet<Vector2Int>();
        int roomToCreateCount = Mathf.RoundToInt(potentialRoomsPositions.Count * roomPercent);
        List<Vector2Int> roomsToCreate = potentialRoomsPositions.OrderBy(x => Guid.NewGuid()).Take(roomToCreateCount).ToList();

        foreach (var roomPosition in roomsToCreate)
        {
            var roomFloor = RunRandomWalk(iterations, walkLength, startRandomlyEachIteration, roomPosition);


            roomPositions.UnionWith(roomFloor);
        }
        return roomPositions;
    }

    private List<List<Vector2Int>> CreateCorridors(HashSet<Vector2Int> floorPositions, HashSet<Vector2Int> potentialRoomsPositions)
    {
        var currentPosition = startPosition;
        potentialRoomsPositions.Add(currentPosition);
        List<List<Vector2Int>> corridorsList = new List<List<Vector2Int>>();

        for (int i = 0; i < corridorCount; i++)
        {
            var corridor = GenerationAlgorithm.RandomWalkCorridor(currentPosition, corridorLength);

            corridorsList.Add(corridor);

            currentPosition = corridor[corridor.Count - 1];

            potentialRoomsPositions.Add(currentPosition);
            floorPositions.UnionWith(corridor);
        }

        return corridorsList;
    }
}
