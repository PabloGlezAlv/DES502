using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InHouseFadeIn : MonoBehaviour
{
    SpriteRenderer renderer;

    [SerializeField]
    Transform zoomPosition;

    bool fadingIn = true;
    bool fadeOut = false;
    void Start()
    {
        renderer = GetComponent<SpriteRenderer>();  
    }

    // Update is called once per frame
    void Update()
    {
        if(fadingIn)
        {
            Color c = renderer.color;

            c.a -= Time.deltaTime;

            renderer.color = c;
            if(c.a<=0)
            {
                fadingIn = false;
            }

        }
        else if(fadeOut)
        {
            //Fade
            Color c = renderer.color;

            c.a += Time.deltaTime/2;

            renderer.color = c;
            if (c.a >= 1.0)
            {
                c.a = 1;
                renderer.color = c;
                fadeOut = false;
                Invoke("GoCredits", 0.5f);
            }

            //Zoom
            
            if(c.a < 1)
            {
                Camera.main.orthographicSize -= Time.deltaTime * 2;

                Vector3 dir = zoomPosition.position - Camera.main.transform.position;

                dir.Normalize();

                Camera.main.transform.Translate(dir * Time.deltaTime * 5);
            }
        }

    }

    public void GoCredits()
    {
        SceneManager.LoadScene("credits");
    }

    public void StartFadeOut()
    {
        fadeOut = true;
    }

}
