using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class SetVolume : MonoBehaviour
{

    public AudioMixer Mixer;
    
    public void SetLevelMusic(float sliderValue)
    {
        Mixer.SetFloat("MusicVol", Mathf.Log10 (sliderValue) * 20);
    }
    
    public void SetLevelEffects(float sliderValue)
    {
        Mixer.SetFloat("EffectsVol", Mathf.Log10 (sliderValue) * 20);
    }
}
