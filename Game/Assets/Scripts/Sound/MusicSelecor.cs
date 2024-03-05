using System.Collections;
using UnityEngine;

public class MusicSelecor : MonoBehaviour
{
    [SerializeField]
    private string TuneName;
    [SerializeField]
    private string IntroName;

    [SerializeField]
    private float IntroTime;

    private void Start()
    {
        //Idea is we wait for the intro to complete then we play the track we want
        AudioManager.instance.Play(IntroName);
        AudioManager.instance.SetVolume(IntroName, 0);
        AudioManager.instance.SetVolume(TuneName, AudioManager.MusicVolume);
        StartCoroutine(AudioManager.instance.FadeAudio(IntroName, -1, AudioManager.MusicVolume));
        AudioManager.instance.PlayDelayed(TuneName, IntroTime);
    }
}
