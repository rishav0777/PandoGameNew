using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _timer;

    private void Start()
    {
        _timer.SetActive(false);
    }


    private void OnEnable()
    {
        PlayerManager.OnFightingBoss += PlayerManager_OnFightingBoss;
    }

    private void PlayerManager_OnFightingBoss(bool isFightingBoss)
    {
        _timer.SetActive(isFightingBoss);
    }

    private void OnDisable()
    {
        PlayerManager.OnFightingBoss -= PlayerManager_OnFightingBoss;
    }
}
