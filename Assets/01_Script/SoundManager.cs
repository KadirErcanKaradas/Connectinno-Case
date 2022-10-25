using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource correctSound, wrongSound, orderSound,levelCompSound,musicSound,loseSound;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    public void ActiveSound(AudioClip clip)
    {
        correctSound.PlayOneShot(clip);
        wrongSound.PlayOneShot(clip);
        orderSound.PlayOneShot(clip);
        levelCompSound.PlayOneShot(clip);
    }
    public void PlaySound()
    {
        correctSound.volume = 0.5f;
        wrongSound.volume = 0.5f;
        orderSound.volume = 0.5f;
        levelCompSound.volume = 0.5f;
    }
    public void StopSound()
    {
        correctSound.volume = 0;
        wrongSound.volume = 0;
        orderSound.volume = 0;
        levelCompSound.volume = 0;
    }

    public void PlayMusic()
    {
        musicSound.volume = 0.50f;
    }

    public void StopMusic()
    {
        musicSound.volume = 0;
    }

    public void PlayLoseSound(AudioClip clip)
    {
        loseSound.PlayOneShot(clip);
    }
}
