using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Transition : MonoBehaviour
{
    [SerializeField] Text sceneName;
    public float countDownTime = 2;
    private float remainingTime;
    private bool timeIsCounting;
    private string nextSceneName;
    // Start is called before the first frame update
    void Start()
    {
        //Gives the data to variabels
        nextSceneName = StaticData.nextScene;
        if (!nextSceneName.Equals("MainMenu")) sceneName.text = nextSceneName;
        timeIsCounting = true;
        remainingTime = countDownTime;
    }

    void Update()
    {
        //Counts down before going to the next scene
        if (timeIsCounting)
        {
            remainingTime -= Time.deltaTime;
            int minutes = Mathf.FloorToInt(remainingTime / 60);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            if (string.Format("{0:00}:{1:00}", minutes, seconds).Equals("00:00"))
            {
                timeIsCounting = false;
                remainingTime = countDownTime;
                SceneController.instance.startScene(nextSceneName); 
            }
        }

    }
}
