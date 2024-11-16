using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public class ResetGame : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _ui;
    [SerializeField]
    private int _sceneIndex;




    private void Start()
    {
        foreach (GameObject go in _ui)
        {
            if (go != null)
            {
                go.SetActive(false);
            }
        }

    }

    private void OnEnable()
    {
        bossManager.OnLevelEnd += OnLevelEnd;
        PlayerManager.OnNoPlayerLeft += OnLevelEnd;
    }

    void NoPlayerLeft()
    {
        Debug.Log("No Player Left");
    }

    private void OnLevelEnd(bool a)
    {
        foreach (GameObject go in _ui)
        {
            if (go != null)
            {
                go.SetActive(true);
            }

        }
    }

    private void OnDisable()
    {
        bossManager.OnLevelEnd -= OnLevelEnd;
        PlayerManager.OnNoPlayerLeft -= OnLevelEnd;
    }

    public void ResetTheGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Debug.Log("Reload Scene");
    }

    [SerializeField] private LeveBasedReward leveBasedReward;

    public void LoadScene()
    {
        Debug.Log("inside loadscene");
        try
        {
           // leveBasedReward.Reward();
        }
        catch(Exception e)
        {
            Debug.Log("Exceptions " + e);
        }
        Debug.Log("inside2 loadscene");
        SceneManager.LoadScene(_sceneIndex);
    }

}
