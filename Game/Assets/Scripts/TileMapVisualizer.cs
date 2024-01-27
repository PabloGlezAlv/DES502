using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

public class TileMapVisualizer : MonoBehaviour
{
    enum wallType { Top, Left, Right, Column, Bottom}

    [SerializeField]
    private Tilemap floorTilemap, wallTilemap;
    [SerializeField]
    private TileBase floorTile, wallTop, wallSideRight, wallSiderLeft, wallBottom, wallFull,
        wallInnerCornerDownLeft, wallInnerCornerDownRight,
        wallDiagonalCornerDownRight, wallDiagonalCornerDownLeft, wallDiagonalCornerUpRight, wallDiagonalCornerUpLeft;
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    private void PaintTiles(IEnumerable<Vector2Int> positions, Tilemap tilemap, TileBase tile)
    {

        foreach (var position in positions)
            PaintSingleTile(tilemap, tile, position);
    }

    private void PaintSingleTile(Tilemap tilemap, TileBase tile, Vector2Int position)
    {
        var tilePosition = tilemap.WorldToCell((Vector3Int)position);
        tilemap.SetTile(tilePosition, tile);
    }

    public void Clear()
    {
        floorTilemap.ClearAllTiles();
        wallTilemap.ClearAllTiles();
    }

    internal void PaintSingleBasicWall(Vector2Int position, string binaryType)
    {
        int typeAsInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;
        if (WallTypes.wallTop.Contains(typeAsInt))
        {
            tile = wallTop;
        }
        else if (WallTypes.wallSideRight.Contains(typeAsInt))
        {
            tile = wallSideRight;
        }
        else if (WallTypes.wallSideLeft.Contains(typeAsInt))
        {
            tile = wallSiderLeft;
        }
        else if (WallTypes.wallBottm.Contains(typeAsInt))
        {
            tile = wallBottom;
        }
        else if (WallTypes.wallFull.Contains(typeAsInt))
        {
            tile = wallFull;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }

    internal void PaintSingleCornerWall(Vector2Int position, string binaryType)
    {
        int typeASInt = Convert.ToInt32(binaryType, 2);
        TileBase tile = null;

        if (WallTypes.wallInnerCornerDownLeft.Contains(typeASInt))
        {
            tile = wallInnerCornerDownLeft;
        }
        else if (WallTypes.wallInnerCornerDownRight.Contains(typeASInt))
        {
            tile = wallInnerCornerDownRight;
        }
        else if (WallTypes.wallDiagonalCornerDownLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownLeft;
        }
        else if (WallTypes.wallDiagonalCornerDownRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerDownRight;
        }
        else if (WallTypes.wallDiagonalCornerUpRight.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpRight;
        }
        else if (WallTypes.wallDiagonalCornerUpLeft.Contains(typeASInt))
        {
            tile = wallDiagonalCornerUpLeft;
        }
        else if (WallTypes.wallFullEightDirections.Contains(typeASInt))
        {
            tile = wallFull;
        }
        else if (WallTypes.wallBottmEightDirections.Contains(typeASInt))
        {
            tile = wallBottom;
        }

        if (tile != null)
            PaintSingleTile(wallTilemap, tile, position);
    }
}
