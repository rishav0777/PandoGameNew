using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalButton : MonoBehaviour
{
    public void Onclick()
    {
        this.gameObject.SetActive(false);
        Debug.Log("BTn Pressed");
    }
}
