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
    GameObject[] UIArrows;
    [SerializeField]
    int UIIndex = 0;

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

    void UpdateArrows()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            UIIndex++;
            if (UIIndex >= UIArrows.Length)
            {
                UIIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            UIIndex--;
            if (UIIndex < 0)
            {
                UIIndex = UIArrows.Length - 1;
            }
        }

        switch (UIIndex)
        {
            case 0:
                ButtonInteract(0);
                break;
            case 1:
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
}

