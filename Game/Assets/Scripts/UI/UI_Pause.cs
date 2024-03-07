using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_Pause : MonoBehaviour
{
    [SerializeField]
    GameObject continueButton;
    [SerializeField]
    GameObject exitButton;

    GameObject container;

    bool paused = false;

    private void Awake()
    {
        container = transform.GetChild(0).gameObject;

        continueButton.GetComponent<Button_UI>().ClickFunc = () => {
            Time.timeScale = 1f;
            paused = false;
            container.SetActive(false);
            Debug.Log("bACK GAME");
        };

        exitButton.GetComponent<Button_UI>().ClickFunc = () => {
            SceneManager.LoadScene("Menu", LoadSceneMode.Single);
            Time.timeScale = 1f;
            paused = false;
        };
    }
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            paused = !paused;

            if(paused) Time.timeScale = 0f;
            else Time.timeScale = 1f;
            container.SetActive(paused);
        }
    }
}
