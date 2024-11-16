using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Coin : MonoBehaviour
{
    public static event Action OnCoinCollect;

    private void Update()
    {
        if(this.enabled == false)
        {
            OnCoinCollect?.Invoke();
        }
    }


}
