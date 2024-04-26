using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InHouseFadeIn : MonoBehaviour
{
    SpriteRenderer renderer;

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

        }

    }

    public void StartFadeOut()
    {
        fadeOut = true;
    }

}
