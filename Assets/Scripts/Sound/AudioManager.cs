using UnityEngine;
using UnityEngine.Audio;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;

    public static AudioManager audioManager;
    
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
    
    private void Start()
    {
        Play("Music1");
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