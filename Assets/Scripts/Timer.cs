using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] Text timerText;
    private float elapsedTime;
    public static Timer timer;
    private bool timeStop = false;
    private void Awake()
    {
        if (timer == null)
        {
            timer = this;
        }
    }
    
    void Update()
    {
        if (!timeStop)
        {
            //Counts the time up each frame and formats it to 00:00
            elapsedTime += Time.deltaTime;
            int minutes = Mathf.FloorToInt(elapsedTime / 60);
            int seconds = Mathf.FloorToInt(elapsedTime % 60);
            timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
    }

    //Returns the current time in total seconds
    public float getFinalTime()
    {
        return elapsedTime;
    }
    public void stopTime()
    {
        timeStop = true;
    }
}
