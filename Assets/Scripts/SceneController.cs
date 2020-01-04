using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneController : SingleTon<SceneController> {

    private void Awake()
    {
        Application.targetFrameRate = 60;

        Screen.SetResolution(1440, 2560,true);

        DontDestroyOnLoad(this);
    }

    public void LoadScene(string scene)
    {

        StartCoroutine(LoadYourAsyncScene(scene));

        Time.timeScale = 1;

    }


    IEnumerator LoadYourAsyncScene(string scene)
    {

        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(scene);

        asyncLoad.allowSceneActivation = false;

        while (!asyncLoad.isDone)
        {

            if (asyncLoad.progress >= 0.9f)
            {
                asyncLoad.allowSceneActivation = true;
            }

            yield return null;

        }


    }
}
