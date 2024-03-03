using UnityEngine;
using UnityEngine.Audio;

[System.Serializable]

public class Sound
{
    public enum AudioType
    {
        SFX,
        Music,
    }

    public string name;

    public AudioClip clip;

    [Range(0f,1f)]
    public float volume;

    [Range(0.1f, 5f)]
    public float pitch;

    public bool loop;

    public AudioType Type;

    [HideInInspector]
    public AudioSource source;

}
