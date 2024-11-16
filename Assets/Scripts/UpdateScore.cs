using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UpdateScore : MonoBehaviour
{
    [SerializeField]
    private TMP_Text m_Text;
    [SerializeField]
    private TMP_Text m_Text1;
    [SerializeField]
    private int _score = 000;

    public AudiController audio;

    private void Start()
    {
        audio = GameObject.FindGameObjectWithTag("reward").GetComponent<AudiController>();
        _score = 000;
        m_Text.text = _score.ToString();
        m_Text1.text = _score.ToString();
    }

    private void OnEnable()
    {
        Collectable.OnCoinCollect += Coin_OnCoinCollect;
    }

    private void Coin_OnCoinCollect()
    {
        if (PlayerPrefs.GetInt("sound") == 1) audio.OnCollecting();
        if (PlayerPrefs.GetInt("vibration") == 1) audio.OnVibration();
        _score += 100;
        m_Text.text = _score.ToString();
        m_Text1.text = _score.ToString();
    }

    private void OnDisable()
    {
        Collectable.OnCoinCollect -= Coin_OnCoinCollect;
    }
}
