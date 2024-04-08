using CodeMonkey.Utils;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine.SceneManagement;

public class UI_Dead : MonoBehaviour
{
    [SerializeField]
    GameObject continueButton;
    [SerializeField]
    GameObject exitButton;
    [SerializeField]
    GameObject[] UIArrows;
    [SerializeField]
    int UIIndex = 0;
    float Slideinc = 0.1f;

    [SerializeField]
    Slider MusicSlider;
    [SerializeField]
    Slider SFXSlider;

    private void Awake()
    {
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
        UpdateArrows();
    }

    public void Restart()
    {
        SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void Exit()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Single);
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

    void UpdateArrows()
    {
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            UIIndex++;
            if (UIIndex > 4)
            {
                UIIndex = 4;
            }
        }
        else if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            UIIndex--;
            if (UIIndex < 0)
            {
                UIIndex = 0;
            }
        }

        switch (UIIndex)
        {
            case 0:
                UpdateSliders(0);
                break;
            case 1:
                UpdateSliders(1);
                break;
            case 2:
                ButtonInteract(0);
                break;
            case 3:
                ButtonInteract(1);
                break;
            default:
                break;
        }

        for (int x = 0; x < UIArrows.Length; x++)
        {
            if (x == UIIndex)
            {
                UIArrows[x].active = true;
            }
            else
            {
                UIArrows[x].active = false;
            }
        }
    }

    void ButtonInteract(int ButtonType)
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (ButtonType == 0)
            {
                Restart();
            }
            else
            {
                Exit();
            }
        }
    }

    void UpdateSliders(int SliderType)
    {
        if (SliderType == 0)//Music slider
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                MusicSlider.value -= Slideinc;
                SetMusicVolume();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                MusicSlider.value += Slideinc;
                SetMusicVolume();
            }
        }
        else //SFX slider
        {
            if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                SFXSlider.value -= Slideinc;
                SetMusicVolume();
            }
            else if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                SFXSlider.value += Slideinc;
                SetMusicVolume();
            }
        }
    }
}

