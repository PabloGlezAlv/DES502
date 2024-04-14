using System.Drawing;
using UnityEngine;

public class FlameTurret : MonoBehaviour
{
    private DoorsController controller;
    protected enum MovementType
    {
        NA,
        Line,
    }

    protected enum LoopType
    {
        NoLoop,
        Forward,
        PingPong,
    }   
    protected enum EnemyDir
    {
        Up = 0, 
        Down = 1, 
        Side = 2,
    }

    private Vector2 CenterPos;
    private float Phase;
    private float TimePoint;
    private bool PingPongFlag = false;

    [SerializeField]
    private EnemyDir Directions;
    private int ChosenDir;
    [SerializeField]
    private float Speed;
    [SerializeField]
    private float FlameSpeed;
    [SerializeField]
    private GameObject[] Flame;
    private float FlameTime;
    [SerializeField]
    private MovementType MType;
    [SerializeField]
    private LoopType LType;
    
    [SerializeField]
    private Vector2[] Points;

    Animator anim;

    [SerializeField]
    private bool Active = true;

    private int LineIndex;

    private void Start()
    {
        anim = GetComponent<Animator>();
        switch (Directions)
        {
            case EnemyDir.Up:
                anim.SetInteger("DecideDirection", 0);
                ChosenDir = 0;
                break;
            case EnemyDir.Down:
                anim.SetInteger("DecideDirection", 1);
                ChosenDir = 1;
                break;
            case EnemyDir.Side:
                anim.SetInteger("DecideDirection", 2);
                ChosenDir = 2;
                break;
            default:
                break;
        }
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] += (Vector2)transform.parent.position;
        }

        if(Points.Length > 0) transform.position = Points[0];
        CenterPos = transform.position;
        for (int i = 0; i < Flame.Length; i++)
        {
            Flame[i].active = false;
        }
        if (Points.Length == 0) MType = MovementType.NA;
        controller = GameObject.FindGameObjectWithTag("DoorController").GetComponent<DoorsController>();

    }

    private void FixedUpdate()
    {
        anim.SetBool("Fire", Flame[ChosenDir].active);
        if (controller != null)
        {
            Active = controller.roomEntities > 0;
        }
        if (Active)
        {
            TimePoint += Time.deltaTime * (Speed / 10);
            FlameTime += Time.deltaTime * FlameSpeed;
        }
        else
        {
            FlameTime = 0;
            anim.Play("FlameTurretDie");
        }
        if (FlameTime > 1f)
        {
            Flame[ChosenDir].active = !Flame[ChosenDir].active;
            FlameTime = 0;
            if (Flame[ChosenDir].active)
            {
                AudioManager.instance.Play("FlameWhoosh");
            }
        }

        switch (MType)
        {
            case MovementType.NA:
                break;
            case MovementType.Line:
                LineMovement();
                break;
        }
    }

    [ExecuteAlways]
    private void OnDrawGizmos()
    {
        Gizmos.color = UnityEngine.Color.green;
        for (int i = 0; i < Points.Length-1; i++)
        {
            Gizmos.DrawLine(Points[i], Points[i+1]);
        }
    }

    public void RemoveSelf()
    {
        gameObject.active = false;
    }

    private void LineMovement()
    {
        switch (LType)
        {
            case LoopType.NoLoop:
                break;
            case LoopType.Forward:
                if (transform.position == (Vector3)Points[LineIndex])
                {
                    CenterPos = transform.position;
                    LineIndex++;
                    if (LineIndex >= Points.Length)
                    {
                        LineIndex = 0;
                    }
                    TimePoint = 0;
                }
                transform.position = Vector3.Lerp(CenterPos, Points[LineIndex], TimePoint);
                break;
            case LoopType.PingPong:
                if (transform.position == (Vector3)Points[LineIndex])
                {
                    CenterPos = transform.position;
                    if (PingPongFlag)
                    {
                        LineIndex--;
                    }
                    else
                    {
                        LineIndex++;
                    }
                    if (LineIndex >= Points.Length)
                    {
                        Debug.Log(Points.Length + " : " + LineIndex);
                        LineIndex = Points.Length-1;
                        PingPongFlag = true;
                    }
                    else if (LineIndex < 0)
                    {
                        Debug.Log(Points.Length + " : " + LineIndex);
                        LineIndex = 0;
                        PingPongFlag = false;
                    }
                    TimePoint = 0;
                }
                transform.position = Vector3.Lerp(CenterPos, Points[LineIndex], TimePoint);

                break;
        }
    }
}
