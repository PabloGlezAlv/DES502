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

enum typeWall{top, topInside, bottomInside, leftInside, rightInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner, rightBottomCorner, 
    bottom, insideLeftTopCorner, insideRightTopCorner, insideLeftBottomCorner ,insideRightBottomCorner}

public enum typeSpike { off, small, high}

public enum typeHole {
    holeTile, holeTop, holeBottom, holeRight, holeLeft, holeTopRightConer, holeTopLeftConer, holeBottomLeftConer, holeBottomRightConer, holeInsideLeftBottom,
    holeInsideLeftTop, holeInsideRightBottom, holeInsideRightTop, holeWithoutTop, holeWithoutBottom, holeWithoutRight, holeWithoutLeft, holeAlone, holeWithoutLeftRight, 
    holeWithoutTopBottom, holeTopLeftConerWInside, holeBottomLeftConerWInside, holeBottomRightConerWInside, holeInsideLeftBottomWInside, holeTopRightInside, holeTopLeftInside,
    holeBottomLeftInside, holeBottomRightInside
}
public class TileMapVisualizer : MonoBehaviour
{
    [SerializeField]
    private GameObject floorTilemapGameObject, wallTilemapGameObject, doorsTileGameObject, trapGameObject, holesGameObject;
    [Header("Floor")]
    [SerializeField]
    private TileBase floorTile, floorCorridorTile;

    [Header("Walls")]
    [SerializeField]
    private TileBase top, topInside, bottomInside, leftInside, rightInside, left, leftTopCorner, leftBottomCorner, right, rightTopCorner,
        rightBottomCorner, bottom, insideLeftTopCorner, insideRightTopCorner, insideLeftBottomCorner, insideRightBottomCorner;

    [Header("Doors")]
    [SerializeField]
    private TileBase closeDoorTileTopLeft, openDoorTileTopLeft, closeDoorTileTopRight, openDoorTileTopRight,
                    closeDoorTileBottomLeft, openDoorTileBottomLeft, closeDoorTileBottomRight, openDoorTileBottomRight,
                    closeDoorTileLeftTop, openDoorTileLeftTop, closeDoorTileLeftBottom, openDoorTileLeftBottom,
                    closeDoorTileRightTop, openDoorTileRightTop, closeDoorTileRightBottom, openDoorTileRightBottom;

    [Header("Tramp")]
    [SerializeField]
    private TileBase spikeOff, spikeSmall, spikeHigh;

    [Header("Hole")]
    [SerializeField]
    private TileBase holeTile, holeTop, holeBottom, holeRight, holeLeft, holeTopRightConer, holeTopLeftConer, holeBottomLeftConer, holeBottomRightConer, holeInsideLeftBottom,
        holeInsideLeftTop, holeInsideRightBottom, holeInsideRightTop, holeWithoutTop, holeWithoutBottom, holeWithoutRight, holeWithoutLeft, holeAlone, holeWithoutLeftRight,
    holeWithoutTopBottom, holeTopLeftConerWInside, holeBottomLeftConerWInside, holeBottomRightConerWInside, holeInsideLeftBottomWInside, holeTopRightInside, holeTopLeftInside,
    holeBottomLeftInside, holeBottomRightInside;


    private Tilemap floorTilemap, wallTilemap, doorsTile, trapTilemap, holeTilemap;

    private TilemapCollider2D doorsTileCollider;

    private void Awake()
    {
        floorTilemap = floorTilemapGameObject.GetComponent<Tilemap>();
        wallTilemap = wallTilemapGameObject.GetComponent<Tilemap>();
        doorsTile = doorsTileGameObject.GetComponent<Tilemap>();
        trapTilemap = trapGameObject.GetComponent<Tilemap>();
        holeTilemap = holesGameObject.GetComponent<Tilemap>();

        doorsTileCollider = doorsTileGameObject.GetComponent<TilemapCollider2D>();
    }

    public void PaintSpike(Vector2Int position, typeSpike type)
    {
        switch (type)
        {
            case typeSpike.off:
                PaintSingleTile(trapTilemap, spikeOff, position);
                break;
            case typeSpike.small:
                PaintSingleTile(trapTilemap, spikeSmall, position);
                break;
            case typeSpike.high:
                PaintSingleTile(trapTilemap, spikeHigh, position);
                break;
        }
    }

    public void PaintHoles(List<Vector2Int> holesPositions, List<typeHole> holesSprites)
    {
        for(int i  = 0; i < holesPositions.Count; i++)
        {
            Debug.Log(holesSprites[i]);
            switch (holesSprites[i])
            {
                case typeHole.holeAlone:
                    PaintSingleTile(holeTilemap, holeAlone, holesPositions[i]);
                    break;
                case typeHole.holeTile:
                    PaintSingleTile(holeTilemap, holeTile, holesPositions[i]);
                    break;
                case typeHole.holeTop:
                    PaintSingleTile(holeTilemap, holeTop, holesPositions[i]);
                    break;
                case typeHole.holeBottom:
                    PaintSingleTile(holeTilemap, holeBottom, holesPositions[i]);
                    break;
                case typeHole.holeRight:
                    PaintSingleTile(holeTilemap, holeRight, holesPositions[i]);
                    break;
                case typeHole.holeLeft:
                    PaintSingleTile(holeTilemap, holeLeft, holesPositions[i]);
                    break;
                case typeHole.holeTopRightConer:
                    PaintSingleTile(holeTilemap, holeTopRightConer, holesPositions[i]);
                    break;
                case typeHole.holeTopLeftConer:
                    PaintSingleTile(holeTilemap, holeTopLeftConer, holesPositions[i]);
                    break;
                case typeHole.holeBottomLeftConer:
                    PaintSingleTile(holeTilemap, holeBottomLeftConer, holesPositions[i]);
                    break;
                case typeHole.holeBottomRightConer:
                    PaintSingleTile(holeTilemap, holeBottomRightConer, holesPositions[i]);
                    break;
                case typeHole.holeInsideLeftBottom:
                    PaintSingleTile(holeTilemap, holeInsideLeftBottom, holesPositions[i]);
                    break;
                case typeHole.holeInsideLeftTop:
                    PaintSingleTile(holeTilemap, holeInsideLeftTop, holesPositions[i]);
                    break;
                case typeHole.holeInsideRightBottom:
                    PaintSingleTile(holeTilemap, holeInsideRightBottom, holesPositions[i]);
                    break;
                case typeHole.holeInsideRightTop:
                    PaintSingleTile(holeTilemap, holeInsideRightTop, holesPositions[i]);
                    break;
                case typeHole.holeWithoutTop:
                    PaintSingleTile(holeTilemap, holeWithoutTop, holesPositions[i]);
                    break;
                case typeHole.holeWithoutBottom:
                    PaintSingleTile(holeTilemap, holeWithoutBottom, holesPositions[i]);
                    break;
                case typeHole.holeWithoutRight:
                    PaintSingleTile(holeTilemap, holeWithoutRight, holesPositions[i]);
                    break;
                case typeHole.holeWithoutLeft:
                    PaintSingleTile(holeTilemap, holeWithoutLeft, holesPositions[i]);
                    break;
                case typeHole.holeWithoutLeftRight:
                    PaintSingleTile(holeTilemap, holeWithoutLeftRight, holesPositions[i]);
                    break;
                case typeHole.holeWithoutTopBottom:
                    PaintSingleTile(holeTilemap, holeWithoutTopBottom, holesPositions[i]);
                    break;
                case typeHole.holeTopLeftConerWInside:
                    PaintSingleTile(holeTilemap, holeTopLeftConerWInside, holesPositions[i]);
                    break;
                case typeHole.holeBottomLeftConerWInside:
                    PaintSingleTile(holeTilemap, holeBottomLeftConerWInside, holesPositions[i]);
                    break;
                case typeHole.holeBottomRightConerWInside:
                    PaintSingleTile(holeTilemap, holeBottomRightConerWInside, holesPositions[i]);
                    break;
                case typeHole.holeInsideLeftBottomWInside:
                    PaintSingleTile(holeTilemap, holeInsideLeftBottomWInside, holesPositions[i]);
                    break;
                case typeHole.holeTopRightInside:
                    PaintSingleTile(holeTilemap, holeTopRightInside, holesPositions[i]);
                    break;
                case typeHole.holeTopLeftInside:
                    PaintSingleTile(holeTilemap, holeTopLeftInside, holesPositions[i]);
                    break;
                case typeHole.holeBottomLeftInside:
                    PaintSingleTile(holeTilemap, holeBottomLeftInside, holesPositions[i]);
                    break;
                case typeHole.holeBottomRightInside:
                    PaintSingleTile(holeTilemap, holeBottomRightInside, holesPositions[i]);
                    break;
            }
        }
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
            case typeWall.topInside:
                tile = topInside;
                break;
            case typeWall.rightInside:
                tile = rightInside;
                break;
            case typeWall.bottomInside:
                tile = bottomInside;
                break;
            case typeWall.leftInside:
                tile = leftInside;
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
                tile = insideLeftBottomCorner;
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
