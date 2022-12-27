using UnityEngine;
using UnityEngine.Audio;
using System;
using UnityEngine.UI;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager audioManager;

    public Slider SliderEfectos;
    public Slider SliderMusica;
    public Slider SensibilitySlder;
    
    public float sliderEfectos;
    public float sliderMusica;
    public float sensibilitySlder;
    

    public void SetsliderEfectosValue()
    {
        sliderEfectos = SliderEfectos.value;
    }
    
    public void SetsliderMusicaValue()
    {
        sliderMusica = SliderMusica.value;
    }
    
    public void SetsensibilitySlderValue()
    {
        sensibilitySlder = SensibilitySlder.value;
    }
    
    void Awake()
    {
        if (audioManager == null)
        {
            audioManager = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.outputAudioMixerGroup = s.AudioMixer;
        }
    }
    
    public void Play(string Audioname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == Audioname);
        s.source.Play();
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + Audioname + " not found!");
            return;
        }
    }
    
    public void Stop(string Audioname)
    {
        Sound s = Array.Find(sounds, sound => sound.name == Audioname);
        s.source.Stop();
        
        if (s == null)
        {
            Debug.LogWarning("Sound: " + Audioname + " not found!");
            return;
        }
    }
}

// FindObjectOfType<AudioManager>().Play("NOMBRE_SONIDO");