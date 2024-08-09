using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class HighScore : MonoBehaviour
{
    public Text highScoreText;
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            gameObject.SetActive(false);
        }
        else
        {
            //Gets the best time for the current level and displays it
            float highScore = PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name+ "HighScore");
            int minutes = Mathf.FloorToInt(highScore / 60);
            int seconds = Mathf.FloorToInt(highScore % 60);
            string time = string.Format("{0:00}:{1:00}", minutes, seconds);
            highScoreText.text = "HighSore: " + time;
        }
    }


}
