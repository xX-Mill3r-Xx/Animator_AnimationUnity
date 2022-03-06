using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public AudioClip sfx;
    public AudioClip anoterSfx;
    public AudioClip bgm;
    private AudioSource audio;

    public static AudioController current;

    void Start()
    {
        current = this;
        audio = GetComponent<AudioSource>();
    }

    public void PlayMusic(AudioClip clip)
    {
        audio.PlayOneShot(clip); 
    }

    public void MudarMusicaBGM()
    {
        audio.clip = sfx;
    }
}
