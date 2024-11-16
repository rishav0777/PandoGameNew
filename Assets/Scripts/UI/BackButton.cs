using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackButton : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextGameobject;
    [SerializeField]
    private bool _diablethisObject = true;

    [SerializeField, Header("Main Menu Buttons")]
    private GameObject _buttons;


    [SerializeField, Header("Disable UI")]
    private GameObject _disableObject;

    [SerializeField]
    private bool _isGameRunning = false;

   

    private void OnEnable()
    {
        MenuManager.OnGameStart += MenuManager_OnGameStart;
    }

    private void MenuManager_OnGameStart()
    {
        _isGameRunning = true;
    }

    private void OnDisable()
    {
        MenuManager.OnGameStart -= MenuManager_OnGameStart;
    }

    public void Onclick()
    {
        if (_nextGameobject != this.gameObject && _nextGameobject != null)
        {
            if (!_isGameRunning)
            {
                _nextGameobject.SetActive(true);
            }
            else if(_isGameRunning)
            {
                _nextGameobject.SetActive(false);
            }
        }

        if (_buttons != null)
        {
            _buttons.SetActive(true);
        }

        if (_diablethisObject)
        {
            this.gameObject.SetActive(false);
        }
    }

    public void DisableUI()
    {
        _disableObject.SetActive(false);
    }
}
