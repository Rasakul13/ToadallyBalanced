using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{   

    [SerializeField] Slider soundVolumeSlider; 
    [SerializeField] Slider musicVolumeSlider; 
    
    public Sound[] sounds, music;
    public AudioSource soundSource, musicSource;

    void Awake()
    {
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.playOnAwake = false;
        }
    }

    void Start()
    {
        LoadSoundVolume();
        LoadMusicVolume();
    }

    public void Play(String name) 
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        
        if(s == null)
        {
            Debug.Log("Sound clip not found");
        }
        else 
        {
            soundSource.clip = s.clip;
            soundSource.Play();
        }
    }


    public void ChangeSoundVolume()
    {   
        soundSource.volume = soundVolumeSlider.value;
        SaveSoundVolume();
    }

    public void LoadSoundVolume()
    {
        if(!PlayerPrefs.HasKey("soundVolume"))
        {
            PlayerPrefs.SetFloat("soundVolume", 0.5f);
        }
        soundVolumeSlider.value = PlayerPrefs.GetFloat("soundVolume");
        soundSource.volume = soundVolumeSlider.value;
    }

    private void SaveSoundVolume()
    {
        PlayerPrefs.SetFloat("soundVolume", soundVolumeSlider.value);
    }


    public void ChangeMusicVolume()
    {   
        musicSource.volume = musicVolumeSlider.value;
        SaveMusicVolume();
    }

    public void LoadMusicVolume()
    {
        if(!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 0.05f);
        }
        musicVolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
        musicSource.volume = musicVolumeSlider.value;
    }

    private void SaveMusicVolume()
    {
        PlayerPrefs.SetFloat("musicVolume", musicVolumeSlider.value);
    }
}
