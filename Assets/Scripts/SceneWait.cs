using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneWait : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextScene;

    [SerializeField]
    private int _waitTime;

    private void Start()
    {
        _nextScene.SetActive(false);
        StartCoroutine(SceneLoadWait());

    }

    IEnumerator SceneLoadWait()
    {
        yield return new WaitForSeconds(_waitTime);
        _nextScene.SetActive(true);
        transform.gameObject.SetActive(false);
    }

}
