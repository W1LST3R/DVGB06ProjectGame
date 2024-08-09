using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using System;

public class OptionsScript : MonoBehaviour
{
    [SerializeField] AudioMixer myMixer;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider SFXSlider;
    [SerializeField] Button muteButton;
    public static OptionsScript audioOptions;
    private bool isMute;
    private float tempMaster;
    private bool isMuting = false;

    private void Awake()
    {
        if (audioOptions == null) {
            audioOptions = this;
        }
    }
    private void Start()
    {
        //If the player has premade setting, then they will be set
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            loadVolume();
        }
        else
        {
            setMusic();
            setSFX();
            setMaster();
        }

        if (PlayerPrefs.HasKey("muteMaster"))
        {
            loadMute();
        }
        else
        {
            isMute = false;
        }
    }
    public void setMusic()
    {
        //Sets music volume
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void setMaster()
    {
        //Sets master volume
        if (!isMuting)
        {
            float volume = masterSlider.value;
            Debug.Log(volume);
            if (!isMute) myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            PlayerPrefs.SetFloat("masterVolume", volume);
        }
        else
        {
            isMuting = false;
        }
            
    }
    public void setSFX()
    {
        //Sets special effect volume
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void muteMaster()
    {
        //Mutes Master volume
        if (isMute)
        {
            PlayerPrefs.SetInt("muteMaster", 0);
            isMute = false;
            muteButton.image.color = Color.clear;
            float volume = PlayerPrefs.GetFloat("masterVolume");
            myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
            Debug.Log(volume);
            masterSlider.value = volume;
        }
        else
        {
            isMuting = true;
            float volume = masterSlider.value;
            Debug.Log(volume);
            PlayerPrefs.SetFloat("masterVolume", volume);
            PlayerPrefs.SetInt("muteMaster", 1);
            isMute = true;
            muteButton.image.color = new Color(245, 215, 0, 255);
            myMixer.SetFloat("Master", -80);
            masterSlider.value = -80;
        }

    }

    //Load premade settings
    public void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        setMusic();
        setMaster();
        setSFX();
    }
   
    //Loads mute setyings
    public void loadMute()
    {
        int muteBool = PlayerPrefs.GetInt("muteMaster");
        if(muteBool == 1)
        {
            isMute = false;
            muteMaster();
        }else isMute = false;
    }
}
