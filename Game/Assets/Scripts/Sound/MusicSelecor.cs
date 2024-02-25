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

    private float MaxVolume = 0.4f;

    private bool IntroDone = false;

    private void Start()
    {
        //Idea is we wait for the intro to complete then we play the track we want
        AudioManager.instance.Play(IntroName);
        AudioManager.instance.SetVolume(IntroName, 0);
        AudioManager.instance.SetVolume(TuneName, MaxVolume);
        StartCoroutine(AudioManager.instance.FadeAudio(IntroName, -1, MaxVolume));
        AudioManager.instance.PlayDelayed(TuneName, IntroTime);
    }
    /*
    private void Update()
    {
        double time = AudioSettings.dspTime;
        if (!IntroDone && time + 1f > nextEventTime)
        {
            nextEventTime += AudioManager.instance.GetClipLength(IntroName);

            AudioManager.instance.PlaySchedueled(TuneName, nextEventTime);      
            IntroDone = true;
        }
    }
    */
}
