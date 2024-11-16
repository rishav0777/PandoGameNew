using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    [SerializeField]
    private GameObject _nextGameobject;
   
    [SerializeField]
    private bool _diablethisObject = true;

    [SerializeField, Header("Main Menu Buttons")]
    private GameObject _buttons;


    [SerializeField, Header("Disable UI")]
    private GameObject _disableObject;

    private void Start()
    {
        if (_nextGameobject != null)
        {
            _nextGameobject.SetActive(false);
        }
    }

    public void Onclick()
    {
        if (_nextGameobject != this.gameObject && _nextGameobject != null)
        {
            _nextGameobject.SetActive(true);
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
