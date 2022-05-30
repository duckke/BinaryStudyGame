using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SoundEnum
{
    Card,
    CardLoop,
    Click,
    Fail,
    GameClear,
    Success,

    Count,
}

public class AudioManager : MonoSingleton<AudioManager>
{
    public AudioSource source;
    public AudioClip[] clips;

    private void Awake()
    {
        source = gameObject.AddComponent<AudioSource>();
        clips = new AudioClip[(int)SoundEnum.Count];
        clips[0] = Resources.Load<AudioClip>("Sounds/card");
        clips[1] = Resources.Load<AudioClip>("Sounds/cardloop");
        clips[2] = Resources.Load<AudioClip>("Sounds/click");
        clips[3] = Resources.Load<AudioClip>("Sounds/fail");
        clips[4] = Resources.Load<AudioClip>("Sounds/gameclear");
        clips[5] = Resources.Load<AudioClip>("Sounds/success");

    }

    public void PlayClickSound(float volumeScale = 0.5f)
    {
        PlaySound(SoundEnum.Click, volumeScale);
    }

    public void PlaySound(SoundEnum val, float volumeScale = 1f, float delay = 0)
    {
        if (delay > 0)
        {
            source.clip = clips[(int)val];
            source.PlayDelayed(delay);
        }
        else
            source.PlayOneShot(clips[(int)val], volumeScale);
    }
}
