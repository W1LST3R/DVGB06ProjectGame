using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class WorldScript : MonoBehaviour
{
    public static WorldScript world;
    [SerializeField] AudioMixer myMixer;

    private void Awake()
    {
        if (world == null)
        {
            world = this;
        }
    }
    private void Start()
    {
        //Calls function if player has premade settings
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            loadVolume();
        }
    }

    //Gets the time for the current level
    public float[] getTimes(string levelName)
    {
        // Gets the current world
        var worldStart = levelName.LastIndexOf(" ", StringComparison.Ordinal) + 1;
        var worldEnd = levelName.LastIndexOf("-", StringComparison.Ordinal);
        var worldNameLength = worldEnd - worldStart;
        string currentWorld = levelName.Substring(worldStart, worldNameLength);
        float[] times = new float[3];

        switch (currentWorld)
        {
            case "1":
                times = World_1.instance.getLevelTimes(levelName);
                break;

                //Pre made to 8 worlds easy to create more classes if i have time.
                /* case "2":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                 case "3":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                 case "4":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                 case "5":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                 case "6":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                 case "7":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                 case "8":
                     times = World_1.instance.getLevelTimes(levelName);
                     break;
                */
        }
        return times;
    }

    //Starts the choosen level
    public float worldSelect(string levelName)
    {
        //Gets the current world
        var worldStart = levelName.LastIndexOf(" ", StringComparison.Ordinal) + 1;
        var worldEnd = levelName.LastIndexOf("-", StringComparison.Ordinal);
        var worldNameLength = worldEnd - worldStart;
        string currentWorld = levelName.Substring(worldStart, worldNameLength);
        float stars = 0;

        switch (currentWorld)
        {
            case "1":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;

        //Pre made to 8 worlds easy to create more classes if i have time.
           /* case "2":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
            case "3":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
            case "4":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
            case "5":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
            case "6":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
            case "7":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
            case "8":
                stars = World_1.instance.getResultOfLevel(levelName);
                break;
           */
        }
        return stars;
    }

    //Loads music settings
    public void loadVolume()
    {
        if (PlayerPrefs.HasKey("muteMaster"))
        {
            int muteBool = PlayerPrefs.GetInt("muteMaster");
            if(muteBool == 1) 
            {
                myMixer.SetFloat("Master", -80);
            }
            else{
                float masterVolume = PlayerPrefs.GetFloat("masterVolume");
                myMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
            }
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            myMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
            myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        }
        else
        {
            float musicVolume = PlayerPrefs.GetFloat("musicVolume");
            float masterVolume = PlayerPrefs.GetFloat("masterVolume");
            float sfxVolume = PlayerPrefs.GetFloat("sfxVolume");
            myMixer.SetFloat("Master", Mathf.Log10(masterVolume) * 20);
            myMixer.SetFloat("Music", Mathf.Log10(musicVolume) * 20);
            myMixer.SetFloat("SFX", Mathf.Log10(sfxVolume) * 20);
        }
        
    }
}
