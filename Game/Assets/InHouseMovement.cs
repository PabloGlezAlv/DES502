using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHouseMovement : MonoBehaviour
{
    [SerializeField]
    Transform topPoint;
    [SerializeField]
    Transform chairPoint;

    bool fade = true;
    bool move = false;
    SpriteRenderer renderer;
    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();

        Color c = new Color(0, 0, 0, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if(fade)
        {
            Color c = renderer.color;
            c.a += Time.deltaTime * 0.75f;
            
            if(c.a > 1)
            {
                fade = false;
                move = true;
            }
        }
        else if(move)
        {
            if( transform.position.y < topPoint.position.y)
            {
                transform.Translate(Vector3.up * Time.deltaTime);
            }
            else if(chairPoint.position.x< transform.position.x)
            {
                transform.Translate(-Vector3.right * Time.deltaTime);
            }
        }
    }
}
