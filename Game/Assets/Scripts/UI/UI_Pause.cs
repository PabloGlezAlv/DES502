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
    int activeArrow = 0;

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
            arrow[activeArrow].SetActive(true);
        }
        if (Input.GetKeyDown(KeyCode.W))
        {
            arrow[activeArrow].SetActive(false);

            activeArrow--;
            if (activeArrow < 0) activeArrow = arrow.Count - 1;

            arrow[activeArrow].SetActive(true);
        }
    }

    private void ResetArrows()
    {
        foreach (var item in arrow)
        {
            item.SetActive(false);
        }

        arrow[activeArrow].SetActive(true);
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
