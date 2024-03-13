using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField]
    List<GameObject> arrow;
    [SerializeField]
    float fadeTime;
    int activeArrow = 0;

    float timer;

    bool active = true;
    private void Awake()
    {
        UserInformation.LoadInformation();

        timer = fadeTime;

        foreach (var item in arrow)
        {
            item.SetActive(false);
        }

        arrow[activeArrow].SetActive(active);

    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))//Play
        {
            if (activeArrow == 0)
                PlayHistory();
            else if (activeArrow == 1)
                PlayEndless();
            else if (activeArrow == 2)
                Quit();
        }
        if(Input.GetKeyDown(KeyCode.S))
        {
            arrow[activeArrow].SetActive(false);

            activeArrow++;
            if(activeArrow >= arrow.Count) activeArrow = 0;
            active = true;
            arrow[activeArrow].SetActive(active);
            timer = fadeTime;
        }
        else
        {
            timer -= Time.deltaTime;
            if(timer < 0)
            {
                active = !active;
                arrow[activeArrow].SetActive(active);
                timer = fadeTime;
            }
        }
    }


    private void PlayHistory()
    {
        UserInformation.gameMode = gamemode.history;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void PlayEndless()
    {
        UserInformation.gameMode = gamemode.endless;
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void Quit()
    {
        UserInformation.SaveInformation();
        Application.Quit();
    }
}
