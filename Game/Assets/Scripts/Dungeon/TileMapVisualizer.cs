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

enum typeWall{top, topBottomInside, leftRightInside , left, leftTopCorner, leftBottomCorner, right, rightTopCorner, rightBottomCorner, 
    bottom, insideLeftTopCorner, insideRightTopCorner, insideLeftBottomCorner ,insideRightBottomCorner}
public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject floorTilemapGameObject, wallTilemapGameObject, doorsTileGameObject, nextLevelGameObject, trapGameObject, holesGameObject;
    [Header("Floor")]
    [SerializeField]
    private TileBase floorTile, floorCorridorTile;

    [Header("Walls")]
    [SerializeField]
    private TileBase top, topBottomInside, leftRightInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner,
        rightBottomCorner, bottom, insideLeftTopCorner, insideRightTopCorner, insideLeftBottomCorner, insideRightBottomCorner;

    [Header("Doors")]
    [SerializeField]
    private TileBase closeDoorTileTopLeft, openDoorTileTopLeft, closeDoorTileTopRight, openDoorTileTopRight,
                    closeDoorTileBottomLeft, openDoorTileBottomLeft, closeDoorTileBottomRight, openDoorTileBottomRight,
                    closeDoorTileLeftTop, openDoorTileLeftTop, closeDoorTileLeftBottom, openDoorTileLeftBottom,
                    closeDoorTileRightTop, openDoorTileRightTop, closeDoorTileRightBottom, openDoorTileRightBottom;

    [Header("NextLevel")]
    [SerializeField]
    private TileBase topleftEnd, toprightEnd, bottomleftEnd, bottomrightEnd;

    [Header("Tramp")]
    [SerializeField]
    private TileBase spikeOff, spikeOn;

    [Header("Hole")]
    [SerializeField]
    private TileBase holeTile;


    private Tilemap floorTilemap, wallTilemap, doorsTile, nextLevelTilemap, trapTilemap, holeTilemap;

    private TilemapCollider2D doorsTileCollider;

    private void Awake()
    {
        floorTilemap = floorTilemapGameObject.GetComponent<Tilemap>();
        wallTilemap = wallTilemapGameObject.GetComponent<Tilemap>();
        doorsTile = doorsTileGameObject.GetComponent<Tilemap>();
        nextLevelTilemap = nextLevelGameObject.GetComponent<Tilemap>();
        trapTilemap = trapGameObject.GetComponent<Tilemap>();
        holeTilemap = holesGameObject.GetComponent<Tilemap>();

        doorsTileCollider = doorsTileGameObject.GetComponent<TilemapCollider2D>();
    }

    public void PaintSpike(Vector2Int position, bool on)
    {
        if (on)
            PaintSingleTile(trapTilemap, spikeOn, position);
        else
            PaintSingleTile(trapTilemap, spikeOff, position);
    }

    public void PaintHoles(IEnumerable<Vector2Int> holesPositions)
    {
        PaintTiles(holesPositions, holeTilemap, holeTile);
    }

    public void PaintNextFloorDoor(Vector2Int positionTopRight)
    {
        PaintSingleTile(nextLevelTilemap, toprightEnd, positionTopRight);

        Vector2Int otherPos = positionTopRight;
        otherPos.x--;
        PaintSingleTile(nextLevelTilemap, topleftEnd, otherPos);

        otherPos = positionTopRight;
        otherPos.y--;
        PaintSingleTile(nextLevelTilemap, bottomleftEnd, otherPos);
        otherPos.x--;
        PaintSingleTile(nextLevelTilemap, bottomrightEnd, otherPos);
    }

    public void PaintFloorTiles(IEnumerable<Vector2Int> floorPositions)
    {

        PaintTiles(floorPositions, floorTilemap, floorTile);
    }

    public void SetDoorCollider(bool open)
    {
        if(open)
        {
            doorsTileCollider.isTrigger = true;
        }
        else
        {
            doorsTileCollider.isTrigger = false;
        }
    }

    public void PaintDoorTiles(Vector2Int floorPositions, bool open, Direction dir, bool leftTop)
    {

        TileBase tile;
        switch (dir)
        {
            case Direction.Left:
                if (open)
                {
                    if (leftTop) tile = openDoorTileLeftTop;
                    else tile = openDoorTileLeftBottom;
                }
                else
                {
                    if (leftTop) tile = closeDoorTileLeftTop;
                    else tile = closeDoorTileLeftBottom;
                }
                break;
            case Direction.Right:
                if (open)
                {
                    if (leftTop) tile = openDoorTileRightTop;
                    else tile = openDoorTileRightBottom;
                }
                else
                {
                    if (leftTop) tile = closeDoorTileRightTop;
                    else tile = closeDoorTileRightBottom;
                }
                break;
            case Direction.Top:
                if (open)
                {
                    if (leftTop) tile = openDoorTileTopLeft;
                    else tile = openDoorTileTopRight;
                }
                else
                {
                    if (leftTop) tile = closeDoorTileTopLeft;
                    else tile = closeDoorTileTopRight;
                }
                break;
            case Direction.Bottom:
                if (open)
                {
                    if (leftTop) tile = openDoorTileBottomLeft;
                    else tile = openDoorTileBottomRight;
                }
                else
                {
                    if (leftTop) tile = closeDoorTileBottomLeft;
                    else tile = closeDoorTileBottomRight;
                }
                break;
            default:
                tile = floorCorridorTile;
                break;
        }



        PaintSingleTile(doorsTile, tile, floorPositions);    
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
        doorsTile.ClearAllTiles();
        nextLevelTilemap.ClearAllTiles();
        trapTilemap.ClearAllTiles();
        holeTilemap.ClearAllTiles();
    }

    internal void PaintSingleWall(Vector2Int position, typeWall type)
    {
        TileBase tile = null;
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
                break;
            case typeWall.topBottomInside:
                tile = topBottomInside;
                break;
            case typeWall.leftRightInside:
                tile = leftRightInside;
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
            case typeWall.insideLeftTopCorner:
                tile = insideLeftTopCorner;
                break;
            case typeWall.insideRightTopCorner:
                tile = insideRightTopCorner;
                break;
            case typeWall.insideRightBottomCorner:
                tile = insideRightBottomCorner;
                break;
            case typeWall.insideLeftBottomCorner:
                tile = insideRightBottomCorner;
                break;
            default:
                // Manejar un caso por defecto si es necesario
                break;
        }


        if (tile != null)
        {
            PaintSingleTile(wallTilemap, tile, position);
        }
    }
}
