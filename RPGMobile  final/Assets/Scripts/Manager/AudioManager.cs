using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    [SerializeField] AudioSource[] SFX, backgroundMusic;

    // Start is called before the first frame update
    void Start()
    {
        if(instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;
        }
        DontDestroyOnLoad(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.T))
        {
            PlaySFX(1);
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            PlayBackgroundMusic(1);
        }
    }

    public void PlaySFX(int soundToPlay)
    {
        if(soundToPlay <  SFX.Length)
        {
            SFX[soundToPlay].Play();
        }
    }

    public void PlayBackgroundMusic(int musicToPlay)
    {
        StopMusic();

        if(musicToPlay < backgroundMusic.Length)
        {
            backgroundMusic[musicToPlay].Play();
        }
    }

    private void StopMusic()
    {
        foreach(AudioSource song in backgroundMusic)
        {
            song.Stop();
        }
    }
}
