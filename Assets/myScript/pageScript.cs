using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class pageScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] public GameObject transactioppage;
    

    public void onclickstart()
    {
        Debug.Log("////////TransactionalServices check wallet" + walletIdKey.Instance.GetWalletId());

        if (walletIdKey.Instance.GetWalletId() == "" || walletIdKey.Instance.GetWalletId() == null) 
        {
            transactioppage.SetActive(true);
            transform.gameObject.SetActive(false);
        }
        else
        {
            
            Debug.Log(walletIdKey.Instance.GetWalletId());
            //SwitchToReferal();
            Debug.Log("////////////////////hghjk//////////////////////////  "+walletIdKey.Instance.GetRtid() + "activeGame");
            if (PlayerPrefs.HasKey(walletIdKey.Instance.GetRtid() + "activeGame"))
            {
                Debug.Log("//                        //page if has key");
                int k = (PlayerPrefs.GetInt(walletIdKey.Instance.GetRtid() + "activeGame"));
                Debug.Log("////page if has key"+k);
                LoadGame(k);
            }
            else
            {
                Debug.Log("///                               /page else has key");
                LoadGame(2);
            }
        }
    }
    public void LoadGame(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }



    private bool sound = false;
    private bool vibration = false;
  
    public GameObject soundImagspriteOn;
    public GameObject vibrationImagspriteOn;
    public GameObject soundImagspriteOff;
    public GameObject vibrationImagspriteOff;

     void Start()
    {
        PlayerPrefs.SetInt("sound", 1);
        PlayerPrefs.SetInt("vibration", 1);
    }
    public void settingSound()
    {
        soundImagspriteOn.SetActive(false);
        soundImagspriteOff.SetActive(false);
        if (sound) soundImagspriteOn.SetActive(true);
        if (!sound) soundImagspriteOff.SetActive(true);

        if (sound) PlayerPrefs.SetInt("sound", 1);
        else PlayerPrefs.SetInt("sound", 0);

        sound = !sound;

    }
    public void settingVibration()
    {
        vibrationImagspriteOn.SetActive(false);
        vibrationImagspriteOff.SetActive(false);
        if (vibration)vibrationImagspriteOn.SetActive(true);
        if (!vibration)vibrationImagspriteOff.SetActive(true);

        if (vibration) PlayerPrefs.SetInt("vibration", 1);
        else PlayerPrefs.SetInt("vibration", 0);

        vibration = !vibration;
    }
}
