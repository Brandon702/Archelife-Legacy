using UnityEngine;
using UnityEngine.Audio;

public enum eAudioType
{
    SFX,
    BGM,
    AMB
}
[System.Serializable]
public class Sound
{
    public string name; //Names are the names of the objects, this allows for calling audio clips in script if needed.
    //public string tag; //tags are for specific groups of objects
    public AudioClip clip; //The actual audio clip

    [Range(0f,1f)]public float volume;
    [Range(0.1f, 3f)]public float pitch = 1;

    public bool loop;

    [HideInInspector]
    public AudioSource source;

    public eAudioType audioType;
}