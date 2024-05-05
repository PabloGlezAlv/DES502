using Unity.VisualScripting;
using UnityEngine;
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
            AudioManager.instance.Play("Select");
            UIIndex++;
            if (UIIndex >= UIArrows.Length)
            {
                UIIndex = 0;
            }
        }
        else if (Input.GetKeyDown(KeyCode.W))
        {
            AudioManager.instance.Play("Select");
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

                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().ChangeMusic(GameTracks.Dungeon);
                Restart();
            }
            else
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().ChangeMusic(GameTracks.Title);
                Exit();
            }
        }
    }
}

