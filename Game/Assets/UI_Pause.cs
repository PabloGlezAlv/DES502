using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
{
    [SerializeField]
    GameObject continueButton;
    [SerializeField]
    GameObject exitButton;

    GameObject container;

    [SerializeField]
    Slider MusicSlider;
    [SerializeField]
    Slider SFXSlider;

    bool paused = false;

    private void Awake()
    {
        container = transform.GetChild(0).gameObject;

        continueButton.GetComponent<Button_UI>().ClickFunc = () => {
            Time.timeScale = 1f;
            paused = false;
            container.SetActive(false);
        };

        exitButton.GetComponent<Button_UI>().ClickFunc = () => {
            Debug.Log("EXIT GAME");
            Time.timeScale = 1f;
            paused = false;
        };

    }

    private void Start()
    {
        //Have to put this in start rather than awake due to the audio manager not
        //being loaded in at the right time.
        MusicSlider.value = AudioManager.MusicVolume;
        SFXSlider.value = AudioManager.SFXVolume;
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

    public void SetMusicVolume()
    {
        AudioManager.MusicVolume = MusicSlider.value;
        AudioManager.instance.SetVolumes();
    }

    public void SetSFXVolume()
    {
        AudioManager.SFXVolume = SFXSlider.value;
        AudioManager.instance.SetVolumes();
        AudioManager.instance.Play("Select");
    }
}
