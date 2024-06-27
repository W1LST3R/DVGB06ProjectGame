using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    [SerializeField] bool nextLevel;
    [SerializeField] string levelName;
    private void OnTriggerEnter2D(Collider2D collision)
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
