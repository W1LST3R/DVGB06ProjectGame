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
    private bool isMute;
    private float tempMaster;
    private void Start()
    {
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
        float volume = musicSlider.value;
        myMixer.SetFloat("Music", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("musicVolume", volume);
    }
    public void setMaster()
    {
        float volume = masterSlider.value;
        if(!isMute) myMixer.SetFloat("Master", Mathf.Log10(volume) * 20);
        else tempMaster = volume;
        PlayerPrefs.SetFloat("masterVolume", volume);
    }
    public void setSFX()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", Mathf.Log10(volume) * 20);
        PlayerPrefs.SetFloat("sfxVolume", volume);
    }
    public void muteMaster()
    {
        if (isMute)
        {
            PlayerPrefs.SetInt("muteMaster", 0);
            isMute = false;
            muteButton.image.color = Color.clear;
            myMixer.SetFloat("Master", PlayerPrefs.GetFloat("masterVolume"));
            masterSlider.value = tempMaster;
        }
        else
        {
            float temp = masterSlider.value;
            PlayerPrefs.SetInt("muteMaster", 1);
            isMute = true;
            muteButton.image.color = new Color(245, 215, 0, 255);
            myMixer.SetFloat("Master", -80);
            masterSlider.value = -80;
            tempMaster = temp;
        }

    }

    public void loadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        masterSlider.value = PlayerPrefs.GetFloat("masterVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("sfxVolume");
        setMusic();
        setMaster();
        setSFX();
    }
   
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
