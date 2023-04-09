using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneUtils : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    
    public void LoadSceneAsync(string sceneName, Action OnComplete, Action<float> percent = null)
    {
        StartCoroutine(LoadSceneAsyncRoutine(sceneName, OnComplete, percent));
    }

    IEnumerator LoadSceneAsyncRoutine(string sceneName, Action OnComplete, Action<float> OnPercent = null)
    {
        yield return null;

        //Begin to load the Scene you specify
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        //Don't let the Scene activate until you allow it to
        asyncOperation.allowSceneActivation = false;
        //When the load is still in progress, output the Text and progress bar
        while (!asyncOperation.isDone)
        {
            //Output the current progress

            OnPercent?.Invoke(asyncOperation.progress * 100);

            // Check if the load has finished
            if (asyncOperation.progress >= 0.9f)
            {
                //Change the Text to show the Scene is ready
                OnPercent?.Invoke(99);
                //Wait to you press the space key to activate the Scene
                asyncOperation.allowSceneActivation = true;
            }

            yield return null;
            OnPercent?.Invoke(100);
            OnComplete?.Invoke();
        }
    }
}