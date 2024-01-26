using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public abstract class AbstractDungeonGenerator : MonoBehaviour
{

    [SerializeField]
    protected TileMapVisualizer tilemapVisualizer = null;
    [SerializeField]
    protected Vector2Int startPosition = Vector2Int.zero;
    
    public void GenerateDungeon()
    {
        tilemapVisualizer.Clear();
        RunProceduralGeneration();
    }

    protected abstract void RunProceduralGeneration();
}
