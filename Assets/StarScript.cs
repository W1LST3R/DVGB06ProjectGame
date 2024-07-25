using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class StarScript : MonoBehaviour
{
    public Sprite emptyStar;
    private float threeStars;
    private float twoStars;
    private bool twoChange = false;
    private bool oneChange = false;
    private Image[] imageArr;
    public Text timeText;


    // Start is called before the first frame update
    void Start()
    {
        getTimesForStage();
        imageArr = GameObject.FindGameObjectWithTag("Stars").GetComponentsInChildren<Image>();
        changeTimeText(threeStars);
    }

    // Update is called once per frame
    void Update()
    {
        if(threeStars < Timer.timer.getFinalTime() && !twoChange)
        {
            twoChange = true;
            imageArr[2].sprite = emptyStar;
            changeTimeText(twoStars);
        }
        else if (twoStars < Timer.timer.getFinalTime() && !oneChange)
        {
            oneChange = true;
            imageArr[1].sprite = emptyStar;
            timeText.text = "";
        }
    }

    private void getTimesForStage()
    {
       float[] times = WorldScript.world.getTimes(SceneManager.GetActiveScene().name);
       threeStars = times[0];
       twoStars = times[1];
    }
    public void changeTimeText(float time)
    {
        int minutes = Mathf.FloorToInt(time / 60);
        int seconds = Mathf.FloorToInt(time % 60);
        timeText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}
