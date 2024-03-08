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

    private Vector2 CenterPos;
    private float Phase;
    private float TimePoint;
    private bool PingPongFlag = false;

    [SerializeField]
    private float Speed;
    [SerializeField]
    private float FlameSpeed;
    [SerializeField]
    private GameObject Flame;
    private float FlameTime;
    [SerializeField]
    private MovementType MType;
    [SerializeField]
    private LoopType LType;
    
    [SerializeField]
    private Vector2[] Points;

    private bool Active = true;

    private int LineIndex;

    private void Start()
    {
        for (int i = 0; i < Points.Length; i++)
        {
            Points[i] += (Vector2)transform.parent.position;
        }

        transform.position = Points[0];
        CenterPos = transform.position;
        Flame.active = false;
        if (Points.Length == 0) MType = MovementType.NA;
        controller = GameObject.FindGameObjectWithTag("DoorController").GetComponent<DoorsController>();
    }

    private void FixedUpdate()
    {
        Active = controller.roomEntities > 0;
        if (Active)
        {
            TimePoint += Time.deltaTime * (Speed / 10);
            FlameTime += Time.deltaTime * FlameSpeed;
        }
        else
        {
            FlameTime = 0;
        }
        if (FlameTime > 1f)
        {
            Flame.active = !Flame.active;
            FlameTime = 0;
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
        Gizmos.color = Color.green;
        for (int i = 0; i < Points.Length-1; i++)
        {
            Gizmos.DrawLine(Points[i], Points[i+1]);
        }
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
