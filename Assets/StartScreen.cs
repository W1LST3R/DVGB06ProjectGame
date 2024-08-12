using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public float countDownTime = 2;
    private float remainingTime;
    private bool timeIsCounting;
    void Start()
    {
        timeIsCounting = true;
        remainingTime = countDownTime;
    }

    void Update()
    {
        if (timeIsCounting)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            if (string.Format("{0:00}:{1:00}", minutes, seconds).Equals("00:00"))
            {
                timeIsCounting = false;
                remainingTime = countDownTime;
                SceneManager.LoadSceneAsync("MainMenu");
            }
        }

    }
}
