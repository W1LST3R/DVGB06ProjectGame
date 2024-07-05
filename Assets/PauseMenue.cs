using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenue : MonoBehaviour
{
    public GameObject pauseMenu;
    // Makes the cursor invisible
    void Start()
    {
        Cursor.visible = false;
    }

    //If the esc key is enterd the time stops and the pause menu is shown
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {

            if (!pauseMenu.activeSelf)
            {
                Time.timeScale = 0f;
                pauseMenu.SetActive(true);
                Cursor.visible = true;
            }
            else
            {
                Time.timeScale = 1f;
                pauseMenu.SetActive(false);
                Cursor.visible = false;
            }
        }
    }

    //Quits the game
    public void home()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        SceneController.instance.loadHomeScene();
    }
    //Restars the game
    public void restart()
    {
        pauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.visible = false;
        SceneController.instance.restart();
    }
    //Resumes the game
    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
}
