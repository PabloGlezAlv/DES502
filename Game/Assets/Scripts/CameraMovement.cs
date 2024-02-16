using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{

    [SerializeField]
    float changePositionTime = 1.0f; //Second

    private Vector3 startPosition;

    private Vector3 endPosition;

    private Vector2Int movementDirection = new Vector2Int();

    bool changingPosition = false;

    float elapsedTime = 0;

    // Update is called once per frame
    void Update()
    {
        //if((Input.GetKeyDown(KeyCode.M)))
        //{
        //    SetPosition(new Vector2Int(0,12));
        //}

        if(changingPosition)
        {
            if (elapsedTime < changePositionTime)
            {
                // Interpolate between startPosition and endPosition over changePositionTime
                transform.position = Vector3.Lerp(startPosition, endPosition, elapsedTime / changePositionTime);

                // Increment elapsed time
                elapsedTime += Time.deltaTime;
            }
            else
            {
                // Ensure the final position is reached
                transform.position = endPosition;
                startPosition = endPosition;
                changingPosition = false;
                elapsedTime = 0;
            }
        }
    }

    public bool SetPosition(Vector2Int newPosition)
    {
        if(!changingPosition)
        {
            endPosition = new Vector3(newPosition.x, newPosition.y, -10);
            startPosition = transform.position;
            changingPosition = true;

            return true;
        }
        return false;
    }
}
