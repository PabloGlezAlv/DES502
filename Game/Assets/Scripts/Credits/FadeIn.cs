using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{
    bool SceneChanged = false;
    SpriteRenderer sr;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        sr.color = Color.black;
    }

    // Update is called once per frame
    void Update()
    {
        if (!SceneChanged)
        {
            sr.color -= new Color(0f, 0f, 0f, .5f) * Time.deltaTime;
            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                StartCoroutine(AudioManager.instance.FadeAudio("End",.5f, AudioManager.MusicVolume));
                StartCoroutine(FadeProcess());
            }
        }
        else
        {
            sr.color += new Color(0f, 0f, 0f, .5f) * Time.deltaTime;
        }

    }

    IEnumerator FadeProcess()
    {
        SceneChanged = true;
        yield return new WaitForSeconds(4);//time should be adjusted to the float multiplier for the image alpha in the update function
        AudioManager.instance.GetComponent<MusicSelecor>().ChangeMusic(GameTracks.Title);
        AudioManager.instance.Stop("End");
        SceneManager.LoadScene("Menu");
    }
}
