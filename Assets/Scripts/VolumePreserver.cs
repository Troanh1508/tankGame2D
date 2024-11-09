using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class VolumePreserver : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    // Start is called before the first frame update
    void Start()
    {
        if ((PlayerPrefs.HasKey("musicVolume")) & (PlayerPrefs.HasKey("sfxVolume")))
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            audioMixer.SetFloat("music", Mathf.Log10(Mathf.Clamp(musicVolume, 0.0001f, 1)) * 20);
            audioMixer.SetFloat("sfx", Mathf.Log10(Mathf.Clamp(sfxVolume, 0.0001f, 1)) * 20);
        }
    }
}
