using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider; // access slider

    private void Start()
    {

        SetMusicVolume();
    }

    //allow slider
    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20); // gives us control to change with slider
        //PlayerPrefs.SetFloat("musicVolume", volume); // Sets player preference to save settings
    }

    //Saves player pref
    private void LoadVolume()
    {
        //musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SetMusicVolume();
    }


}