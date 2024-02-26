using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeLogic : MonoBehaviour
{
    [SerializeField]
    TileMapVisualizer visualizer;
    [SerializeField]
    int damage = 2;

    [SerializeField]
    float timeChange = 1;

    [SerializeField]
    float DamageWaitTime = 1.5f;

    float timer = 0;
    float timerWait;

    bool active = false;

    List<Vector2Int> points = new List<Vector2Int>();

    bool roomLocked = true;

    void Start()
    {
        
    }

    public void AddPoints(Vector2Int p)
    {
        points.Add(p);
    }

    public void Clear()
    { points.Clear(); }

    public void SetNewRoom(bool set)
    {
        roomLocked = set;
        if(roomLocked)
        {
            timer = timeChange;
        }
        else //Dont active traps if finish or ols room
        {
            active = false;
            for (int i = 0; i < points.Count; i++)
            {
                visualizer.PaintSpike(points[i], active);
            }
        }
    }
    public void AddPoints(List<Vector2Int> p)
    {
        foreach (Vector2Int p2 in p) {  points.Add(p2); }
    }
    // Update is called once per frame
    void Update()
    {
        if(roomLocked)
        {
            timer -= Time.deltaTime;

            if (timer < 0)
            {
                timer = timeChange;
                active = !active;
                for (int i = 0; i < points.Count; i++)
                {
                    visualizer.PaintSpike(points[i], active);
                }
            }

            timerWait -= Time.deltaTime;
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if (timerWait <= 0 && (system = collision.gameObject.GetComponent<LifeSystem>()) && active)
        {
            system.GetDamage(damage);
            timerWait = DamageWaitTime;
        }
    }
}