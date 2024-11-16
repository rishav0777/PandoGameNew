using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LoadingScene : MonoBehaviour
{
    [SerializeField]
    private bool _loadOnStart = false;
    [SerializeField]
    private int _sceneToLoad;
    
    public GameObject LoadingScreen;
    public Image LoadingBarFill;


    private void Start()
    {
        if (_loadOnStart)
        {
            LoadScene(_sceneToLoad);
        }

    }
  

    public void LoadScene(int sceneID)
    {
        //StartCoroutine(LoadSceneAsync(sceneID));
    }

    IEnumerator LoadSceneAsync(int sceneID)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneID);

        if (LoadingScreen != null)
        {

            LoadingScreen.SetActive(true);
        }

        while(!operation.isDone)
        {
            float progressValue = Mathf.Clamp01(operation.progress / 0.9f);

            if (LoadingBarFill != null)
            {

                LoadingBarFill.fillAmount = progressValue;
            }

            yield return null;
        }
    }


    public void LoadSceneWithoutBar(int SceneID)
    {
        SceneManager.LoadScene(SceneID);
    }
}
