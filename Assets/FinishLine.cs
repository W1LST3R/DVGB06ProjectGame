using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] bool nextLevel;
    [SerializeField] string levelName;

    //If the player collides with the end, then the player will go to the next level, if not it will go to the specified level
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
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
}
