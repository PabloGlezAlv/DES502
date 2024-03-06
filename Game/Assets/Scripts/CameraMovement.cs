using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;
using UnityEngine.UI;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    float changePositionTime = 1.0f; //Second
    [SerializeField]
    float fadeInBlack = 0.2f; 
    [SerializeField]
    float fadeOutBlack = 0.2f;
    [SerializeField]
    float marginNewRoom = 0.1f;

    [SerializeField]
    Image blackFade;

    private Vector3 endPosition;

    bool changingPosition = false;

    float elapsedTime = 0;

    float alpha = 0;
    // Update is called once per frame
    void Update()
    {
        if(changingPosition)
        {
            elapsedTime += Time.deltaTime;
            
            if (elapsedTime > changePositionTime)
            {
                //Reset parameters
                changingPosition = false;
                elapsedTime = 0;
                alpha = 0;
            }
            else if(elapsedTime > changePositionTime / 2)
            {
                // Ensure the final position is reached
                transform.position = endPosition;
            }

            if(elapsedTime < fadeInBlack)
            {
                alpha += Time.deltaTime / fadeInBlack;
                Color c = blackFade.color;
                c.a = alpha;
                blackFade.color = c;
            }
            else if(elapsedTime > changePositionTime - fadeOutBlack - marginNewRoom) 
            {
                alpha -= Time.deltaTime / fadeOutBlack;
                Color c = blackFade.color;
                c.a = alpha;
                blackFade.color = c;
            }
        }
    }

    public void SetPosition(Vector2Int newPosition)
    {
        transform.position = new Vector3(newPosition.x, newPosition.y, transform.position.z);
    }

    public bool SetPositionSmooth(Vector2Int newPosition)
    {
        if(!changingPosition)
        {
            endPosition = new Vector3(newPosition.x, newPosition.y, -10);
            changingPosition = true;

            return true;
        }
        return false;
    }
}
