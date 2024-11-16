using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingLevel : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        string level = gameObject.GetComponent<TextMeshProUGUI>().text;
        walletIdKey.Instance.SetLevel(level[level.Length-1].ToString());
       
    }


}
