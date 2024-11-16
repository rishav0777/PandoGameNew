  using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VideoWait : MonoBehaviour
{
    [SerializeField]
    private float _waitTime;

    [SerializeField]
    private int _sceneIndex = 1;
 
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitTime);
        
        SceneManager.LoadScene(_sceneIndex);
    }
   

}
