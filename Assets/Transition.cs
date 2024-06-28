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
        nextSceneName = StaticData.nextScene;
        sceneName.text = nextSceneName;
        timeIsCounting = true;
        remainingTime = countDownTime;
        Debug.Log(nextSceneName);
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
                SceneController.instance.startScene(nextSceneName); 
            }
        }

    }
}
