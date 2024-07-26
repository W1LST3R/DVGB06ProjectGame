using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class LevelMenu : MonoBehaviour
{
    public Button[] buttons;
    public GameObject levelButtons;
    public Sprite starImg;
    public Sprite emptyStarImg;

    private void Awake()
    {
        buttonsToArray();
        for (int i = 0; i < buttons.Length; i++)
        {
            //Disable button
            buttons[i].enabled = false;
            Image[] imageArr = buttons[i].GetComponentsInChildren<Image>();
            for(int j = 1; j < imageArr.Length; j++)
            {
                //Makes the lock sprite visible
                if (j == 4)
                {
                    imageArr[j].color = Color.white;
                }
                //Makes the stars empty
                else if (j > 0 && j < 4)
                {
                    changeToEmpty(imageArr[j]);
                }
            }
        }
        int unclockedLevels = PlayerPrefs.GetInt("UnlockedLevel", 1);
        Debug.Log(unclockedLevels);
        //Loops threw all unlocked levels
        for (int i = 0; i < unclockedLevels; i++)
        {
            //Enable button
            buttons[i].enabled = true;
            Image[] im = buttons[i].GetComponentsInChildren<Image>();
            for (int j = 1; j < im.Length; j++)
            {
                //Makes the lock invisible
                if (j == 4)
                {
                    im[j].color = Color.clear;
                }
                //Makes the stars appear for the number of stars, the player have unlocked
                else if (j > 0 && j <= PlayerPrefs.GetFloat("World 1-"+ (i+1)))
                {
                    changeToStar(im[j]);
                }
            }
        }
    }

    //Loads the selected level
    public void loadLevel(int levelId)
    {
        string levelName = "World 1-" + levelId;
         SceneController.instance.loadScene(levelName);
    }
    public void loadLevel(string levelName)
    {
        SceneController.instance.loadScene(levelName);
    }

    //Adds all buttons to the array
    void buttonsToArray()
    {
        int childCount = levelButtons.transform.childCount;
        buttons = new Button[childCount];
        for (int i = 0; i < childCount; i++)
        {
            buttons[i] = levelButtons.transform.GetChild(i).GetComponent<Button>();
        }
    }
    //updates the sprite
    private void changeToStar(Image img)
    {
        img.sprite = starImg;
    }
    //updates the sprite
    private void changeToEmpty(Image img)
    {
        img.sprite = emptyStarImg;
    }
}
