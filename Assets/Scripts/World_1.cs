using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class World_1 : MonoBehaviour
{
    public static World_1 instance;
    private float[] times1_1 = { 36, 50 };
    private float[] times1_2 = { 29, 35 };
    private float[] times1_3 = { 33, 40 };
    private float[] times1_4 = { 37, 45 };
    private float[] times1_5 = { 45, 54 };
    private float[] times1_6 = { 26, 35 };
    private float[] times1_7 = { 16, 25 };
    private float[] times1_8 = { 65, 80 };

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    public float[] getLevelTimes(string levelName)
    {
        //calls the function for the current level and retuns the times for the level
        float[] times = new float[2];
        switch (levelName)
        {
            case "World 1-1":
                times = times1_1;
                break;
            case "World 1-2":
                times = times1_2;
                break;
            case "World 1-3":
                times = times1_3;
                break;
            case "World 1-4":
                times = times1_4;
                break;
            case "World 1-5":
                times = times1_5;
                break;
            case "World 1-6":
                times = times1_6;
                break;
            case "World 1-7":
                times = times1_7;
                break;
            case "World 1-8":
                times = times1_8;
                break;

        }
        return times;
    }
    public float getResultOfLevel(string levelName)
    {
        //calls the function for the current level and checks how many stars the player got
        float stars = 0;
        Timer.timer.stopTime();
        switch (levelName)
        {
            case "World 1-1":
                stars = stars1_1();
                break;
            case "World 1-2":
                stars = stars1_2();
                break;
            case "World 1-3":
                stars = stars1_3();
                break;
            case "World 1-4":
                stars = stars1_4();
                break;
            case "World 1-5":
                stars = stars1_5();
                break;
            case "World 1-6":
                stars = stars1_6();
                break;
            case "World 1-7":
                stars = stars1_7();
                break;
            case "World 1-8":
                stars = stars1_8();
                break;

        }
        return stars;
        
    }

    private float stars1_1()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_1[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_1[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_2()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_2[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_2[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_3()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_3[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_3[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_4()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_4[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_4[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_5()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_5[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_5[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_6()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_6[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_6[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_7()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_7[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_7[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_8()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < times1_8[0])
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < times1_8[1])
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
}
