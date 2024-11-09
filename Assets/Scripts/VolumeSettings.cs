using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider sfxSlider;
    
    private void Start() {
        if ((PlayerPrefs.HasKey("musicVolume")) & (PlayerPrefs.HasKey("sfxVolume")))
        {
            LoadVolume();
        }
        else
        {
            musicSlider.value = musicSlider.maxValue;
            sfxSlider.value = sfxSlider.maxValue;
        }
    }

    public void SetMusicVolume(){
        float volume = Mathf.Log10(Mathf.Clamp(musicSlider.value, 0.0001f, 1)) * 20;
        audioMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("musicVolume", musicSlider.value);
    }

    public void SetSfxVolume(){
        float volume = Mathf.Log10(Mathf.Clamp(sfxSlider.value, 0.0001f, 1)) * 20;
        audioMixer.SetFloat("sfx", volume);
        PlayerPrefs.SetFloat("sfxVolume", sfxSlider.value);
    }

    public void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume", 0);
        sfxSlider.value = PlayerPrefs.GetFloat("sfxVolume", 0);
        
    }

}
