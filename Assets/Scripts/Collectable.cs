using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Collectable : MonoBehaviour
{
    public static event Action OnCoinCollect;


    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Coin"))
        {
            OnCoinCollect?.Invoke();

            other.gameObject.SetActive(false);
                        

        }
    }
}
