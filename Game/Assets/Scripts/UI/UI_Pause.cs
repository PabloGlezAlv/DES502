using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UI_Pause : MonoBehaviour
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

    GameObject container;

    [SerializeField]
    Slider MusicSlider;
    [SerializeField]
    Slider SFXSlider;

    bool paused = false;

    private void Awake()
    {
        container = transform.GetChild(0).gameObject;
        /*
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
        */
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
            if (paused) AudioManager.instance.Play("UIOpen");
            else AudioManager.instance.Play("UIClose");
            if(paused) Time.timeScale = 0f;
            else Time.timeScale = 1f;
            container.SetActive(paused);

            Debug.Log(Time.timeScale);
        }
        UpdateArrows();
    }

    public void Continue()
    {
        Time.timeScale = 1f;
        paused = false;
        container.SetActive(false);
    }

    public void Exit()
    {
        Time.timeScale = 1f;
        paused = false;
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
        if (container.active)
        {
            if (Input.GetKeyDown(KeyCode.S))
            {
                AudioManager.instance.Play("Select");
                UIIndex++;
                if (UIIndex > 3)
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
                    UIIndex = 3;
                }
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
                Continue();
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
            if (Input.GetKeyDown(KeyCode.A))
            {
                MusicSlider.value -= Slideinc;
                SetMusicVolume();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                MusicSlider.value += Slideinc;
                SetMusicVolume();
            }
        }
        else //SFX slider
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                SFXSlider.value -= Slideinc;
                SetMusicVolume();
            }
            else if (Input.GetKeyDown(KeyCode.D))
            {
                SFXSlider.value += Slideinc;
                SetMusicVolume();
            }
        }
    }
}
