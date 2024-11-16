using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SkipPage : MonoBehaviour
{
    // Start is called before the first frame update
   public void Onclick()
    {
        if (PlayerPrefs.HasKey(walletIdKey.Instance.GetRtid() + "activeGame"))
        {
            Debug.Log("////skip if has key");
            int k = (PlayerPrefs.GetInt(walletIdKey.Instance.GetRtid() + "activeGame"));
            Debug.Log("////skip if has key"+k);
            SceneManager.LoadScene(k);
        }
        else
        {
            Debug.Log("////skip else has key");
            SceneManager.LoadScene(2);
        }
        
    }
}
