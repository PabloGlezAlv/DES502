using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    // Start is called before the first frame update
    Image img;
    [SerializeField]
    float fadeTime = 1;

    bool start = true;

    bool end = false;

    bool moveCamera = false;
    float timeMoving = 1.5f;
    void Start()
    {
        img = GetComponent<Image>();
    }

    public void StartEnd() { 
        end = true;
        moveCamera = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(start)
        {
            Color color = img.color;

            color.a -= Time.deltaTime * fadeTime;

            img.color = color;

            if (color.a <= 0)
            {
                start = false;
                moveCamera = true;
            }
        }
        else if (end)
        {
            Color color = img.color;

            color.a += Time.deltaTime * fadeTime;

            img.color = color;
            if (color.a >= 1.3) SceneManager.LoadScene("EndGame Inside"); 
        }
        else if(moveCamera)
        {
            Camera.main.transform.Translate(new Vector3(0, -Time.deltaTime * 2.2f, 0));
            timeMoving-=Time.deltaTime;
            if(timeMoving <= 0) { moveCamera = false; }
        }

    }
}
