using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DeleteStartFade : MonoBehaviour
{
    [SerializeField]
    float textTime = 0.3f;
    [SerializeField]
    int finalYpos = 1100;
    [SerializeField]
    GameObject text;
    [SerializeField]
    GameObject fadeImg;

    bool startGame = false;
    
    // Start is called before the first frame update
    void Start()
    {
        Invoke("deleteText", textTime);
    }

    void deleteText()
    {
        startGame = true;
        Destroy(text);
    }

    // Update is called once per frame
    void Update()
    {
        if (startGame)
        {
            fadeImg.gameObject.transform.Translate(new Vector3(0, finalYpos * Time.deltaTime, 0));
            if (fadeImg.gameObject.transform.position.y >= finalYpos * 2) // Change scene
            {
                Destroy(this.gameObject);
            }
        }
    }
}
