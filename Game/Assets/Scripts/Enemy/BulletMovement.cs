using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    private int damage = 1;

    private Vector2 direction = new Vector2(1,1);
    [SerializeField]
    private float speed = 3;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        direction.Normalize();

        rb.velocity = direction * speed;
    }

    public void SetMovementDirection(Vector2 dir) 
    {
        direction = dir;
    }
    public void SetMovementSpeed(float spe) 
    { 
        speed = spe;
    }

    public void SetPosition(Vector3 pos)
    {
        transform.position = pos;
    }

    public void SetDamage(int dam) 
    {
        damage = dam;
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        LifeSystem system;
        if (system = collision.gameObject.GetComponent<LifeSystem>())
        {
            system.GetDamage(damage);
        }
        else
        {
            this.gameObject.SetActive(false);
        }

        this.gameObject.SetActive(false);
    }
}
