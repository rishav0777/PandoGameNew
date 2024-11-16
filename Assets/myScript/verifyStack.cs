using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;

public class verifyStack : MonoBehaviour
{
    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileStake?walletId=";
    [SerializeField] private GenerateRtId rtId;
    


    public class MyData
    {

        public Boolean isBlocked;
        public Boolean isVerified;
        public Boolean walletIdfound;
        public int stakeAmount;

    }
    private void Start()
    {
       
        rtId = FindObjectOfType<GenerateRtId>();
        request();
    }


    public void request()
    {
        url = url + walletIdKey.Instance.GetWalletId();
        GetRequest();
    }
    IEnumerator GetRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
        {
            webRequest.SetRequestHeader("Authorization", "Bearer " + walletIdKey.Instance.GetToken());
            yield return webRequest.SendWebRequest();

            try
            {
                if (webRequest.result != UnityWebRequest.Result.Success)
                {

                    Debug.Log("/////Error" + webRequest.error);
                }
                else
                {
                    Debug.Log("data " + webRequest.downloadHandler.text);
                    MyData data = JsonUtility.FromJson<MyData>(webRequest.downloadHandler.text);

                    StartCoroutine(wait());

                }
            }
            catch (Exception e)
            {
                print("GetReferalUrl error: " + e);
            }
        }
    }


    IEnumerator wait()
    {
        yield return new WaitForSeconds(5.0f);
        request();
    }
}
