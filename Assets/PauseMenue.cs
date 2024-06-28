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
    public void quit()
    {
        Application.Quit();
    }

    //Resumes the game
    public void resume()
    {
        Time.timeScale = 1f;
        pauseMenu.SetActive(false);
        Cursor.visible = false;
    }
}
