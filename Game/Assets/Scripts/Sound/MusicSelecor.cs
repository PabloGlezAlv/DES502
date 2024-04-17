using UnityEngine;

public enum GameTracks
{
    Title,
    Dungeon,
    Market,
    End,
    Death,
}

public class MusicSelecor : MonoBehaviour
{
    public GameTracks tracks;
    private string TuneName;
    private string IntroName;
    private float IntroTime;
    public bool MusicStarted;
    public static int LastArea = 0;//0 for dungeon, 1 for market

    private void Start()
    {
        if (MusicStarted) ChangeMusic(tracks);
    }

    public void ChangeMusic(GameTracks track)
    {
        CheckTracks(track);
        PlayMusic();
    }

    public void CheckTracks(GameTracks track)
    {
        switch (track)
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
                IntroName = "";
                TuneName = "End";
                IntroTime = 0f;
                break;
            case GameTracks.Death:
                IntroName = "";
                TuneName = "Death";
                IntroTime = 0f;
                break;
        }
    }

    public void PlayMusic()
    {
        //Idea is we wait for the intro to complete then we play the track we want
        if (IntroName != "") AudioManager.instance.SetVolume(IntroName, 0);
        AudioManager.instance.Play(IntroName);
        AudioManager.instance.SetVolume(TuneName, AudioManager.MusicVolume);
        StartCoroutine(AudioManager.instance.FadeAudio(IntroName, -.125f, AudioManager.MusicVolume));
        AudioManager.instance.PlayDelayed(TuneName, IntroTime);
    }

    public void FadeMusic()
    {
        if (IntroName != "") StartCoroutine(AudioManager.instance.FadeAudio(IntroName, 1f, AudioManager.MusicVolume));
        StartCoroutine(AudioManager.instance.FadeAudio(TuneName, 1f, AudioManager.MusicVolume));
    }
}
