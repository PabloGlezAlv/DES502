using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;
using UnityEngine.WSA;

enum typeWall{top, topInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner, rightBottomCorner, bottom }
public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject floorTilemapGameObject, wallTilemapGameObject, wallTopmapGameObject, doorsTileGameObject, doorsTopGameObject, nextLevelGameObject, trampGameObject;
    [Header("Floor")]
    [SerializeField]
    private TileBase floorTile, floorCorridorTile;

    [Header("Walls")]
    [SerializeField]
    private TileBase top, topInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner, rightBottomCorner, bottom;

    [Header("Doors")]
    [SerializeField]
    private TileBase closeDoorTileTop, openDoorTileTop;

    [Header("NextLevel")]
    [SerializeField]
    private TileBase topleftEnd, toprightEnd, bottomleftEnd, bottomrightEnd;

    [Header("Tramp")]
    [SerializeField]
    private TileBase spikeOff, spikeOn;


    private Tilemap floorTilemap, wallTilemap, wallTopmap, doorsTile, doorsTop, nextLevel, trap;

    private TilemapCollider2D doorsTileCollider, doorsTopCollider;

    private void Awake()
    {
        floorTilemap = floorTilemapGameObject.GetComponent<Tilemap>();
        wallTilemap = wallTilemapGameObject.GetComponent<Tilemap>();
        wallTopmap = wallTopmapGameObject.GetComponent<Tilemap>();
        doorsTile = doorsTileGameObject.GetComponent<Tilemap>();
        doorsTop = doorsTopGameObject.GetComponent<Tilemap>();
        nextLevel = nextLevelGameObject.GetComponent<Tilemap>();
        trap = trampGameObject.GetComponent<Tilemap>();

        doorsTileCollider = doorsTileGameObject.GetComponent<TilemapCollider2D>();
        doorsTopCollider = doorsTopGameObject.GetComponent<TilemapCollider2D>();
    }

    public void PaintSpike(Vector2Int position, bool on)
    {
        if (on)
            PaintSingleTile(trap, spikeOn, position);
        else
            PaintSingleTile(trap, spikeOff, position);
    }

    public void PaintNextFloorDoor(Vector2Int positionTopRight)
    {
        PaintSingleTile(nextLevel, toprightEnd, positionTopRight);

        Vector2Int otherPos = positionTopRight;
        otherPos.x--;
        PaintSingleTile(nextLevel, topleftEnd, otherPos);

        otherPos = positionTopRight;
        otherPos.y--;
        PaintSingleTile(nextLevel, bottomleftEnd, otherPos);
        otherPos.x--;
        PaintSingleTile(nextLevel, bottomrightEnd, otherPos);
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void setDoorCollider(bool open)
    {
        if(open)
        {
            doorsTileCollider.isTrigger = true;
            doorsTopCollider.isTrigger = true;
        }
        else
        {
            doorsTileCollider.isTrigger = false;
            doorsTopCollider.isTrigger = false;
        }
    }

    public void PaintDoorTiles(Vector2Int floorPositions, bool open, direction dir)
    {
        TileBase tile;
        if (open)
            tile = openDoorTileTop;
        else
            tile = closeDoorTileTop;

        if (dir == direction.top)
        {
            PaintSingleTile(doorsTop, tile, floorPositions );
        }
        else
        {
            PaintSingleTile(doorsTile, tile, floorPositions);
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
        nextLevel.ClearAllTiles();
        trap.ClearAllTiles();
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
