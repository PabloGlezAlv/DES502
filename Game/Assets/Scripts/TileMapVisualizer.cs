using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

enum typeWall{top, topInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner, rightBottomCorner, bottom }
public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private Tilemap floorTilemap, wallTilemap, wallTopmap, doorsTile, doorsTop;
    [Header("Floor")]
    [SerializeField]
    private TileBase floorTile, floorCorridorTile;

    [Header("Walls")]
    [SerializeField]
    private TileBase top, topInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner, rightBottomCorner, bottom;

    [Header("Doors")]
    [SerializeField]
    private TileBase closeDoorTileTop, openDoorTileTop;
    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void PaintDoorTiles(IEnumerable<Vector2Int> floorPositions, bool open, direction dir)
    {
        TileBase tile;
        if (open)
            tile = openDoorTileTop;
        else
            tile = closeDoorTileTop;

        if (dir == direction.top)
        {
            PaintTiles(floorPositions, doorsTop, tile);
        }
        else
        {
            PaintTiles(floorPositions, doorsTile, tile);
        }
    }

    public void PaintCorridorTiles(IEnumerable<Vector2Int> floorPositions)
    {
        PaintTiles(floorPositions, floorTilemap, floorCorridorTile);
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
        wallTopmap.ClearAllTiles();
        doorsTile.ClearAllTiles();
        doorsTop.ClearAllTiles();
    }

    internal void PaintSingleWall(Vector2Int position, typeWall type)
    {
        TileBase tile = null;
        bool topInRoom = false;
        switch (type)
        {
            case typeWall.left:
                tile = left;
                break;
            case typeWall.right:
                tile = right;
                break;
            case typeWall.top:
                tile = top;
                topInRoom = true;
                break;
            case typeWall.topInside:
                tile = topInside;
                topInRoom = true;
                break;
            case typeWall.bottom:
                tile = bottom;
                break;
            case typeWall.leftTopCorner:
                tile = leftTopCorner;
                break;
            case typeWall.rightTopCorner:
                tile = rightTopCorner;
                break;
            case typeWall.rightBottomCorner:
                tile = rightBottomCorner;
                break;
            case typeWall.leftBottomCorner:
                tile = leftBottomCorner;
                break;
            default:
                // Manejar un caso por defecto si es necesario
                break;
        }


        if (tile != null)
        {
            if(topInRoom)
                PaintSingleTile(wallTopmap, tile, position);
            else
                PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
