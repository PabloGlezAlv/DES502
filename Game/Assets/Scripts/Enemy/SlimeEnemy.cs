using System.ComponentModel.Design;
using UnityEngine;

public class SlimeEnemy : EnemyBase
{
    [Header("Slimen parameters")]
    protected float ThinkTime = 0;
    [SerializeField]
    protected float MaxThinkTime = 2;
    [SerializeField]
    protected float MovementDistance;
    [SerializeField]
    protected int damage;

    private Vector2 CurrentPos = Vector2.zero;
    private void Start()
    {
        speed = 1;
        CurrentPos = transform.position;
        direction = Vector2.zero;
    }

    private new void Update()
    {
        ThinkTime += Time.deltaTime;
        if (ThinkTime > MaxThinkTime)
        {
            //Choose the destination for the slime to go to
            int Des = Mathf.FloorToInt(Random.Range(0, 4));
            CurrentPos = transform.position;
            switch (Des)
            {
                case 0://Up
                    direction = new Vector2(0, MovementDistance);
                    break;
                case 1://Down
                    direction = new Vector2(0, -MovementDistance);
                    break;
                case 2://Left
                    direction = new Vector2(MovementDistance, 0);
                    break;
                case 3://Right
                    direction = new Vector2(MovementDistance, 0);
                    break;
                default:
                    direction = Vector2.zero;
                    break;
            }
            ThinkTime = 0;
        }
    }

    private void FixedUpdate()
    {
        //Prevents the slime from trying to get outside the walls
        /*
        if (TargetPos.x > 11)
        {
            TargetPos.x = 11;
        }
        else if (TargetPos.x < -11)
        {
            TargetPos.x = -11;
        }
        if (TargetPos.y > 5)
        {
            TargetPos.x = 5;
        }
        else if (TargetPos.x < -5)
        {
            TargetPos.x = -5;
        }

        if (CurrentPos.x > 11)
        {
            CurrentPos.x = 11;
        }
        else if (CurrentPos.x < -11)
        {
            CurrentPos.x = -11;
        }
        if (CurrentPos.y > 5)
        {
            CurrentPos.x = 5;
        }
        else if (CurrentPos.x < -5)
        {
            CurrentPos.x = -5;
        }
        */
        transform.Translate(direction * Time.deltaTime * speed);
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
}