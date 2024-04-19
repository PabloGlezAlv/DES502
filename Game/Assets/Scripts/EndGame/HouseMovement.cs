using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HouseMovement : MonoBehaviour
{
    Rigidbody2D rb;
    bool toDoor = false;
    [SerializeField]
    FadeOut end;

    bool camera = false;
    bool zoom = false;

    Camera cam;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        cam = Camera.main.GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if(camera)   
        {
            Camera.main.transform.Translate(new Vector3(0, Time.deltaTime, 0));
            if(zoom)
            {
                cam.orthographicSize -=Time.deltaTime;
            }
        }
    }

    private void MoveToDoor()
    {
        toDoor = true;
        rb.velocity = Vector3.up;

        Invoke("Fade", 5.5f);
        Invoke("CameraMov", 0.5f);
        Invoke("CameraZoom", 3.5f);
    }
    private void CameraZoom()
    {
        zoom = true;
    }
    private void CameraMov()
    {
        camera = true;
    }
    private void Fade()
    {
        end.StartEnd();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.gravityScale = 0;

        Invoke("MoveToDoor", 1f);
    }
}
