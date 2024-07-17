using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Windows;
public class World_1 : MonoBehaviour
{
    public static World_1 instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }
    public float getResultOfLevel(string levelName)
    {
        //calls the function for the current level and checks how many stars the player got
        float stars = 0;
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
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
        }
        return numberOfStars;
    }
    private float stars1_2()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
    private float stars1_3()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
    private float stars1_4()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
    private float stars1_5()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
    private float stars1_6()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
    private float stars1_7()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
    private float stars1_8()
    {
        float numberOfStars = 1;
        if (Timer.timer.getFinalTime() < 16)
        {
            numberOfStars = 3;
            Debug.Log("I if sats");
        }
        else if (Timer.timer.getFinalTime() < 22)
        {
            numberOfStars = 2;
            Debug.Log("I if sats 2 stars");
        }
        return numberOfStars;
    }
}
