using System.ComponentModel.Design;
using UnityEngine;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer.Internal;
using System.Linq;
using System.Threading.Tasks.Sources;
using Unity.VisualScripting;

public class SlimeEnemy : EnemyBase
{
    [Header("Slimen parameters")]
    protected float ThinkTime = 0;
    protected float WalkTime = 0;

    [SerializeField]
    protected bool DEBUG;

    [SerializeField]
    protected float MaxThinkTime = 2;
    
    [SerializeField]
    protected int damage;

    [SerializeField]
    protected List<Vector2Int> Floormap = new();

    [SerializeField]
    private bool[] ValidDirections = {true, true, true, true };
    
    private Vector2 CurrentPos = Vector2.zero;
    private Vector2 TargetPos = Vector2.zero;
    
    private float centerx = 0;
    private float centery = 0;

    protected void Awake()
    {
        base.Awake();
    }
    private void Start()
    {
        ThinkTime += Random.Range(-0.5f, 0.5f);
        CurrentPos = transform.position;
        TargetPos = CurrentPos;
        direction = Vector2.zero;

        Vector2 RoomOffset = transform.parent.position;
        Vector2Int CastOffset = Vector2Int.RoundToInt(RoomOffset);

        //Stores the position of the 
        List<Vector2Int> HoleMap = GetComponentInParent<TrapLocations>().GetHolesRelative();
        List<Vector2Int> SpikeMap = GetComponentInParent<TrapLocations>().GetSpikesRelative();
        List<Vector2Int> WallMap = new();

        for (int x = -11; x < 11; x++)
        {
            WallMap.Add(new Vector2Int(x, 5) + CastOffset);
            WallMap.Add(new Vector2Int(x, -6) + CastOffset);
        }
        
        for (int y = -5; y < 5; y++) 
        {
            WallMap.Add(new Vector2Int(-12, y) + CastOffset);
            WallMap.Add(new Vector2Int(11, y) + CastOffset);
        }

        foreach(Vector2Int h in HoleMap)
        {
            Floormap.Add(h);
        }
        Floormap.AddRange(SpikeMap);
        Floormap.AddRange(WallMap);
    }

    private void Update()
    {
        base.Update();

        centerx = transform.position.x;
        centery = transform.position.y;
        ThinkTime += Time.deltaTime;
        WalkTime += Time.deltaTime * speed;
        if (ThinkTime > MaxThinkTime)
        {
            for (int i = 0; i < 4; i++)
            {
                ValidDirections[i] = true;
            }

            for (int x = 0; x < Floormap.Count; x++)
            {
                if (centerx - 1 == Floormap[x].x + 0.5f && centery == Floormap[x].y + 0.5f)
                {
                    ValidDirections[(int)Direction.Left - 1] = false;
                }
                if (centerx + 1 == Floormap[x].x + 0.5f && centery == Floormap[x].y + 0.5f)
                {
                    ValidDirections[(int)Direction.Right - 1] = false;
                }
                if (centerx == Floormap[x].x + 0.5f && centery + 1 == Floormap[x].y + 0.5f)
                {
                    ValidDirections[(int)Direction.Top - 1] = false;
                }
                if (centerx == Floormap[x].x + 0.5f && centery - 1 == Floormap[x].y + 0.5f)
                {
                    ValidDirections[(int)Direction.Bottom - 1] = false;
                }
            }

            //Choose the destination for the slime to go to
            Direction dir = (Direction)Mathf.FloorToInt(Random.Range(1, 5));
            CurrentPos = transform.position;
            switch (dir)
            {
                case Direction.Top://Up
                    if (ValidDirections[(int)Direction.Top - 1] == true)
                    {
                        TargetPos = new Vector2(CurrentPos.x, CurrentPos.y + 1);
                        ThinkTime = 0;
                    }
                    else
                    {
                        TargetPos = CurrentPos;
                    }
                    break;
                case Direction.Bottom://Down
                    if (ValidDirections[(int)Direction.Bottom - 1] == true)
                    {
                        TargetPos = new Vector2(CurrentPos.x, CurrentPos.y - 1);
                        ThinkTime = 0;
                    }
                    else
                    {
                        TargetPos = CurrentPos;
                    }
                    break;
                case Direction.Left://left
                    if (ValidDirections[(int)Direction.Left - 1] == true)
                    {
                        TargetPos = new Vector2(CurrentPos.x - 1, CurrentPos.y);
                        ThinkTime = 0;
                    }
                    else
                    {
                        TargetPos = CurrentPos;
                    }
                    break;
                case Direction.Right://Right
                    if (ValidDirections[(int)Direction.Right - 1] == true)
                    {
                        TargetPos = new Vector2(CurrentPos.x + 1, CurrentPos.y);
                        ThinkTime = 0;
                    }
                    else
                    {
                        TargetPos = CurrentPos;
                    }
                    break;
                default:
                    break;
            }
            WalkTime = 0;
        }
    }

    private void FixedUpdate()
    {
        transform.position = Vector3.Lerp(CurrentPos, TargetPos, WalkTime);
    }

    public override void GetDamage(int damage)
    {
        base.GetDamage(damage);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if (system = collision.gameObject.GetComponent<LifeSystem>())
        {
            system.GetDamage(damage);
        }
    }

    [ExecuteAlways]
    private void OnDrawGizmos()
    {
        if (DEBUG)
        {
            for (int a = 0; a < 4; a++)
            {
                if (!ValidDirections[(int)Direction.Left] == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawWireCube(new Vector2(centerx - 1, centery), Vector3.one);
                if (!ValidDirections[(int)Direction.Right] == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawWireCube(new Vector2(centerx + 1, centery), Vector3.one);
                if (!ValidDirections[(int)Direction.Bottom] == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawWireCube(new Vector2(centerx, centery - 1), Vector3.one);
                if (!ValidDirections[(int)Direction.Top] == true)
                {
                    Gizmos.color = Color.red;
                }
                else
                {
                    Gizmos.color = Color.white;
                }
                Gizmos.DrawWireCube(new Vector2(centerx, centery + 1), Vector3.one);
            }

            for (int x = 0; x < Floormap.Count; x++)
            {
                Gizmos.color = Color.green;
                Gizmos.DrawWireCube(new Vector2(Floormap[x].x + 0.5f, Floormap[x].y + 0.5f), Vector3.one);
            }
        }
    }
}