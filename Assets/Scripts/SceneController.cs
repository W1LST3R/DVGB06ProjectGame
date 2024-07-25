using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public static SceneController instance;
    [SerializeField]Animator animator;
    [SerializeField] GameObject scoreBanner;
    [SerializeField] GameObject transition;

    //
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Retrives the name of the nextlevel and start the transition to the next level
    public void nextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string scenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
        var sceneNameStart = scenePath.LastIndexOf("/", StringComparison.Ordinal) + 1;
        var sceneNameEnd = scenePath.LastIndexOf(".", StringComparison.Ordinal);
        var sceneNameLength = sceneNameEnd - sceneNameStart;
        string nextSceneName = scenePath.Substring(sceneNameStart, sceneNameLength);
        StaticData.nextScene = nextSceneName;
        StartCoroutine(startTransition());
    }

    //starts the transition to the specified scene
    public void loadScene(String sceneName)
    {
        StaticData.nextScene = sceneName;
        StartCoroutine(startTransition());
    }

    //Starts transiton between scenes
    IEnumerator startTransition()
    {
        transition.SetActive(false);
        if (!SceneManager.GetActiveScene().name.Equals("MainMenu") && !SceneManager.GetActiveScene().name.Equals("Tutorial"))
        {
            //Get the stars for the level and plays the correct animation
            scoreBanner.SetActive(true);
            string levelName = SceneManager.GetActiveScene().name;
            float nbrStars = WorldScript.world.worldSelect(levelName);
            checkIfHighestScore(nbrStars);
            animator.SetFloat("NbrOfStars", nbrStars);
            yield return new WaitForSeconds(3);
            scoreBanner.SetActive(false);
        }
        //Makes transiton
        transition.SetActive(true);
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        animator.SetFloat("NbrOfStars", 0);
        SceneManager.LoadSceneAsync("Transition");
    }

    //Starts the scene
    public void startScene(String sceneName)
    {
        animator.SetTrigger("Start");
        SceneManager.LoadSceneAsync(sceneName);
        
    }

    //Loads the scene to main menu
    public void loadHomeScene()
    {
        StaticData.nextScene = "MainMenu";
        StartCoroutine(homeTransition());
    }

    //Makes the transition to home menus
    IEnumerator homeTransition()
    {
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Transition");
    }

    //restarts scene
    public void restart()
    {
        StartCoroutine(restartTransition());
    }

    //Restarts the level
    IEnumerator restartTransition()
    {
        string sceneName = SceneManager.GetActiveScene().name;
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        startScene(sceneName);
    }

    //Checks if the stars for the current was a new highscore
    public void checkIfHighestScore(float stars)
    {
        if (stars > PlayerPrefs.GetFloat(SceneManager.GetActiveScene().name))
        {
            PlayerPrefs.SetFloat(SceneManager.GetActiveScene().name, stars);
        }
    }
}
