using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour
{
    [SerializeField]
    List<GameObject> arrow;
    [SerializeField]
    float fadeTime = 0.5f;
    int activeArrow = 0;

    float timer;

    bool active = true;

    GameObject container;

    bool paused = false;



    private void Awake()
    {
        container = transform.GetChild(0).gameObject;

        ResetArrows();
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;
            ResetArrows();
            if (paused) Time.timeScale = 0f;
            else Time.timeScale = 1f;
            container.SetActive(paused);
        }
        if (Input.GetKeyDown(KeyCode.Space))//Play
        {
            if (activeArrow == 0) //This is the first volume slide callback
                Debug.Log("SlideBar 1");
            else if (activeArrow == 1) //This is the second volume slide callback
                Debug.Log("SlideBar 2");
            else if (activeArrow == 2)
                Continue();
            else if (activeArrow == 3)
                Exit();
        }
        if (Input.GetKeyDown(KeyCode.S))
        {
            arrow[activeArrow].SetActive(false);

            activeArrow++;
            if (activeArrow >= arrow.Count) activeArrow = 0;
            active = true;
            arrow[activeArrow].SetActive(active);
            timer = fadeTime;
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            arrow[activeArrow].SetActive(false);

            activeArrow--;
            if (activeArrow < 0) activeArrow = arrow.Count - 1;
            active = true;
            arrow[activeArrow].SetActive(active);
            timer = fadeTime;
        }
        else
        {
            timer -= Time.deltaTime;
            if (timer < 0)
            {
                active = !active;
                arrow[activeArrow].SetActive(active);
                timer = fadeTime;
            }
        }
    }

    private void ResetArrows()
    {
        timer = fadeTime;

        foreach (var item in arrow)
        {
            item.SetActive(false);
        }

        arrow[activeArrow].SetActive(active);
    }

    private void Continue()
    {
        Time.timeScale = 1f;
        paused = false;
        container.SetActive(false);
    }

    private void Exit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
        Time.timeScale = 1f;
        paused = false;
    }
}
