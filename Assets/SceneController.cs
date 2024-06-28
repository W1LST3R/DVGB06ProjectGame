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
    public void nextLevel()
    {
        int nextSceneIndex = SceneManager.GetActiveScene().buildIndex + 1;
        string scenePath = SceneUtility.GetScenePathByBuildIndex(nextSceneIndex);
        var sceneNameStart = scenePath.LastIndexOf("/", StringComparison.Ordinal) + 1;
        var sceneNameEnd = scenePath.LastIndexOf(".", StringComparison.Ordinal);
        var sceneNameLength = sceneNameEnd - sceneNameStart;
        string nextSceneName = scenePath.Substring(sceneNameStart, sceneNameLength);
        Debug.Log("next Scene name " + nextSceneName);
        StaticData.nextScene = nextSceneName;
        StartCoroutine(startTransition());
    }

    public void loadScene(String sceneName)
    {
        StaticData.nextScene = sceneName;
        StartCoroutine(startTransition());
    }

    IEnumerator startTransition()
    {
        Debug.Log("i transition");
        animator.SetTrigger("End");
        yield return new WaitForSeconds(1);
        SceneManager.LoadSceneAsync("Transition");
    }
    public void startScene(String sceneName)
    {
        animator.SetTrigger("Start");
        SceneManager.LoadSceneAsync(sceneName);
    }
}
