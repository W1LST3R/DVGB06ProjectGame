using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FinishLine : MonoBehaviour
{
    [SerializeField] bool nextLevel;
    [SerializeField] string levelName;
    public Animator animator;

    //If the player collides with the end, then the player will go to the next level, if not it will go to the specified level
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            animator.SetTrigger("EndHit");
            unlockNextLevel();
            if (nextLevel)
            {
                SceneController.instance.nextLevel();
            }
            else
            {
                SceneController.instance.loadScene(levelName);
            }

        }
    }
     void unlockNextLevel()
    {
        //unlocks levels if it the highest level made, necessary for the main menu
        //so levels will be locked until the level before it has been made
        Debug.Log(SceneManager.GetActiveScene().buildIndex-2);
        Debug.Log(PlayerPrefs.GetInt("ReachedIndex"));
        if (SceneManager.GetActiveScene().buildIndex-2 >= PlayerPrefs.GetInt("ReachedIndex"))
        {
            PlayerPrefs.SetInt("ReachedIndex", PlayerPrefs.GetInt("ReachedIndex")+1);
            PlayerPrefs.SetInt("UnlockedLevel", PlayerPrefs.GetInt("UnclockedLevel",1)+1);
            PlayerPrefs.Save();
            Debug.Log(PlayerPrefs.GetInt("UnclockedLevel"));

        }
    }
}
