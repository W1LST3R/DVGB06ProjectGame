using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldScript : MonoBehaviour
{
    public static WorldScript world;

    private void Awake()
    {
        if (world == null)
        {
            world = this;
        }
    }
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
}
