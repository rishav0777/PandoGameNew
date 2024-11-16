using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class backGame : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject back;
    public TextMeshProUGUI level;
    
    void Update()
    {
        string te = level.text;
        int t = te[te.Length - 2]-47;
        Debug.Log("   njn  "+walletIdKey.Instance.GetRtid() + "activeGame");
        if (PlayerPrefs.HasKey(walletIdKey.Instance.GetRtid() + "activeGame"))
        {
            Debug.Log("////back if has key"+t);
            int k = (PlayerPrefs.GetInt(walletIdKey.Instance.GetRtid() + "activeGame"));
            int nw = t;
            Debug.Log("////back if has key11" + t+"  "+nw);
            if (k <= nw)
            {
                Debug.Log("////backif greater has key");

                PlayerPrefs.SetInt(walletIdKey.Instance.GetRtid() + "activeGame", t);
            }
        }
        else {
            Debug.Log("////backelse has key");
            PlayerPrefs.SetInt(walletIdKey.Instance.GetRtid() + "activeGame", t);
            }
    }
    public void OnClick()
    {
        Debug.Log("onclickback button");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
