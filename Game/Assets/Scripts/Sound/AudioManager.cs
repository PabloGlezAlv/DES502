using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public AudioMixerGroup audioMixer;

    public Sound[] sounds;

    public static AudioManager instance;

    public static float MusicVolume = 0.4f;
    public static float SFXVolume = 0.5f;
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = audioMixer;
        }
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.Play();
    }

    public void Stop(string name)
    {
        if (name == "")
        {
            Debug.LogWarning("SOUND: " + name + " IS NOT FOUND");
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.Stop();
        }
    }

    public void SetVolume(string name,float Volume)
    {
        if (name == "")
        {
            Debug.LogWarning("SOUND: " + name + " IS NOT FOUND");
        }
        else
        {
            Sound s = Array.Find(sounds, sound => sound.name == name);
            s.source.volume = Volume;
        }
    }

    public bool CheckIfStopped(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return !s.source.isPlaying;
    }

    public float GetClipLength(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        return s.source.clip.length;
    }

    public void ChangePitch(string name, float Pitch)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.pitch = Pitch;
    }
    public void PlayDelayed(string name, float Time)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        s.source.PlayDelayed(Time);
    }

    public IEnumerator FadeAudio(string name, float Increment, float MaxVolume)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);

        while (s.source.volume > -1f && s.source.volume < MaxVolume)
        {
            s.source.volume -= Increment * Time.deltaTime;
            if (Increment < 0)//Assume it is a fade in
            {
                if (s.source.volume >= MaxVolume)
                {
                    s.source.volume = MaxVolume;
                    break;
                }
            }
            else//Assume it is a fade out
            {
                if (s.source.volume <= 0f)
                {
                    s.source.Stop();
                    break;
                }
            }

            yield return null;
        }
        if (s.source.volume <= 0f)
        {
            s.source.Stop();
        }

    }

    public void SetVolumes()
    {
        foreach (Sound sound in sounds)
        {
            if (sound.Type == Sound.AudioType.Music)
            {
                sound.source.volume = MusicVolume;
            }            
            else if (sound.Type == Sound.AudioType.SFX)
            {
                sound.source.volume = SFXVolume;
            }
        }
    }

}
