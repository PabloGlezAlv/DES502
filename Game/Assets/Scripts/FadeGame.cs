using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeGame : MonoBehaviour
{
    [SerializeField]
    UnityEngine.UI.Image fadeInOutImage;

    Vector3 startPos;
    bool fading = false;

    // Start is called before the first frame update
    void Awake()
    {
        startPos = fadeInOutImage.transform.position;
    }

    public void SetFade(bool fade = true)
    {
        fading = fade;

    }

    // Update is called once per frame
    void Update()
    {
        if(fading)
        {
            fadeInOutImage.gameObject.transform.Translate(new Vector3(0, -startPos.y * Time.deltaTime, 0));
            if (fadeInOutImage.gameObject.transform.position.y >= 0) // Change scene
            {
                GameObject.FindGameObjectWithTag("AudioManager").GetComponent<MusicSelecor>().ChangeMusic(GameTracks.Dungeon);
                SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
            }
        }
    }
}
