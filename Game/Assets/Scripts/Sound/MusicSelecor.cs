using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicSelecor : MonoBehaviour
{
    [SerializeField]
    private string TuneName;

    private void Start()
    {
        AudioManager.instance.Play(TuneName);
        AudioManager.instance.SetVolume(TuneName, 0);
        StartCoroutine(AudioManager.instance.FadeAudio(TuneName, -1, 0.3f));
    }
}
