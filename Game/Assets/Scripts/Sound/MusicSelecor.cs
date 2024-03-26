using System.Collections;
using UnityEngine;

public class MusicSelecor : MonoBehaviour
{
    public enum GameTracks
    {
        Title,
        Dungeon,
        Market,
        End,
        Death,
    }
    public GameTracks tracks;
    private string TuneName;
    private string IntroName;
    private float IntroTime;

    private void Start()
    {
        CheckTracks();
        PlayMusic();
    }

    public void CheckTracks()
    {
        switch (tracks)
        {
            case GameTracks.Title:
                IntroName = "TitleIntro";
                TuneName = "Title";
                IntroTime = 8.533f;
                break;
            case GameTracks.Dungeon:
                IntroName = "DungeonIntro";
                TuneName = "Dungeon";
                IntroTime = 9.601f;
                break;
            case GameTracks.Market:
                IntroName = "MarketIntro";
                TuneName = "Market";
                IntroTime = 8.534f;
                break;
            case GameTracks.End:
                break;
            case GameTracks.Death:
                break;
        }
    }

    public void PlayMusic()
    {
        //Idea is we wait for the intro to complete then we play the track we want
        AudioManager.instance.Play(IntroName);
        AudioManager.instance.SetVolume(IntroName, 0);
        AudioManager.instance.SetVolume(TuneName, AudioManager.MusicVolume);
        StartCoroutine(AudioManager.instance.FadeAudio(IntroName, -1, AudioManager.MusicVolume));
        AudioManager.instance.PlayDelayed(TuneName, IntroTime);
    }
}
