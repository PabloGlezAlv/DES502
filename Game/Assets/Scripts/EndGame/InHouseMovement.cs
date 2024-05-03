using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InHouseMovement : MonoBehaviour
{
    [SerializeField]
    Transform topPoint;
    [SerializeField]
    Transform chairPoint;
    [SerializeField]
    GameObject chair;

    bool fade = true;
    bool move = false;
    SpriteRenderer renderer;

    Animator animator;

    [SerializeField]
    float waitFadeout = 0.5f;
    [SerializeField]
    private InHouseFadeIn fadeScript;



    // Start is called before the first frame update
    void Awake()
    {
        renderer = GetComponent<SpriteRenderer>();

        animator = GetComponent<Animator>();

        Color c = new Color(0, 0, 0, 0);
    }

    void destroyChair() { Destroy(chair); }
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
                if (transform.position.y > topPoint.position.y)
                    animator.SetBool("change", true);
            }
            else if(chairPoint.position.x < transform.position.x)
            {
                transform.Translate(-Vector3.right * Time.deltaTime);
                if (chairPoint.position.x > transform.position.x)
                {
                    animator.SetBool("change", false);

                    move = false;

                    Invoke("destroyChair", 0.85f);
                }
            }
        }
        else if(waitFadeout > 0)
        {
            
            
                waitFadeout -= Time.deltaTime;

                if(waitFadeout < 0)
                {
                fadeScript.StartFadeOut();
                }
            
        }
    }
}
