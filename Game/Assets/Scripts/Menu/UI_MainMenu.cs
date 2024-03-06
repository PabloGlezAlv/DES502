using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Antlr3.Runtime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_MainMenu : MonoBehaviour
{
    [SerializeField]
    GameObject playButton;
    [SerializeField]
    GameObject endlessButton;
    [SerializeField]
    GameObject exitButton;

    private void Awake()
    {
        UserInformation.LoadInformation();

        playButton.GetComponent<Button_UI>().ClickFunc = () => {
            UserInformation.gameMode = gamemode.history;
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        };

        endlessButton.GetComponent<Button_UI>().ClickFunc = () => {
            UserInformation.gameMode = gamemode.endless;
            SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
        };

        exitButton.GetComponent<Button_UI>().ClickFunc = () => {
            Application.Quit();
        };
    }
}
