using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public AudioSource music;
    public AudioSource sFX;
    public AudioClip mainMusic;
    public AudioClip gameMusic;
    public AudioClip shootSFX;
    public AudioClip deathSFX;
    public AudioMixer musicMixer;

    public void SetMusicVolume(string gameState)
    {
        if(gameState == "MainMenu")
        {
            musicMixer.SetFloat("Music", -20);
        }
        else if(gameState == "Gameplay")
        {
            musicMixer.SetFloat("Music", -35);
        }
    }
}
