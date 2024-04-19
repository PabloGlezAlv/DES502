using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField]
    List<GameObject> arrow;
    [SerializeField]
    float fadeTime = 0.5f;
    [SerializeField]
    FadeGame startGame;

    int activeArrow = 0;

    bool intoGame = false;

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
        if (intoGame) return;
        if(Input.GetKeyDown(KeyCode.Space))//Play
        {
            if (activeArrow == 0)
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().FadeMusic();
                PlayHistory();
            }
            else if (activeArrow == 1)
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().FadeMusic();
                PlayEndless();
            }
            else if (activeArrow == 2)
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().FadeMusic();
                Quit();
            }
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
        startGame.SetFade();
        intoGame = true;
    }

    private void PlayEndless()
    {
        UserInformation.gameMode = gamemode.endless;
        startGame.SetFade();
        intoGame = true;
    }

    private void Quit()
    {
        UserInformation.SaveInformation();
        Application.Quit();
    }
}
