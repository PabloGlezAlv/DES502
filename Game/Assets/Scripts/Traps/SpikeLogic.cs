using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpikeLogic : MonoBehaviour
{
    [SerializeField]
    List<TileMapVisualizer> visualizers;
    [SerializeField]
    int damage = 2;

    [SerializeField]
    float timeHigh = 1;
    [SerializeField]
    float timeSmall = 0.4f;
    [SerializeField]
    float timeOut = 1f;

    [SerializeField]
    float DamageWaitTime = 1.5f;

    float timer = 0;
    float timerSmall = 0;
    float timerWait;

    typeSpike active = typeSpike.off;

    List<Vector2Int> points = new List<Vector2Int>();

    bool roomLocked = true;

    int visualizerIndex = 0;

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
            timer = timeOut;
        }
        else //Dont active traps if finish or ols room
        {
            active = typeSpike.off;
            for (int i = 0; i < points.Count; i++)
            {
                visualizers[visualizerIndex].PaintSpike(points[i], active);
            }
        }
    }
    public void AddPoints(List<Vector2Int> p, int type)
    {
        visualizerIndex = type;
        foreach (Vector2Int p2 in p) {  points.Add(p2); }
    }
    // Update is called once per frame
    void Update()
    {
        if(roomLocked)
        {
            timer -= Time.deltaTime;

            if(timer < 0 && active == typeSpike.off)
            {
                active = typeSpike.small;
                for (int i = 0; i < points.Count; i++)
                {
                    visualizers[visualizerIndex].PaintSpike(points[i], active);
                }
                timer = timeSmall;
            }
            else if(timer < 0 && active == typeSpike.small)
            {
                active = typeSpike.high;
                timer = timeHigh;
                for (int i = 0; i < points.Count; i++)
                {
                    visualizers[visualizerIndex].PaintSpike(points[i], active);
                }
            }
            else if(timer < 0 && active == typeSpike.high)
            {
                active = typeSpike.off;
                for (int i = 0; i < points.Count; i++)
                {
                    visualizers[visualizerIndex].PaintSpike(points[i], active);
                }
                timer = timeOut;
            }

            timerWait -= Time.deltaTime;
        }
       
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        //Collision  with player
        LifeSystem system;
        if (timerWait <= 0 && (system = collision.gameObject.GetComponent<LifeSystem>()) && active == typeSpike.high)
        {
            system.GetDamage(damage);
            timerWait = DamageWaitTime;
        }
    }
}