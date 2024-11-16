using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class GetReferalUrl : MonoBehaviour
{

    private string Rurl = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral?rtId=";
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private TMP_Text referaltext;
    [SerializeField] private walletIdKey wallet;
    string Token;

    public class Data
    {
       public string url;
       public string message;
    }

    private void Start()
    {
        wallet = FindObjectOfType<walletIdKey>();
        rtId = FindObjectOfType<GenerateRtId>();
        request();
       
    }




    public void request()
    {
        Rurl = Rurl + rtId.GetRtId();
        print("/////////////////////////////////////getreferal"+Rurl);
        referaltext.text = walletIdKey.Instance.GetReferal();// "refpandoproject/$2b$10$w5xsX/b0ClfCoB/PAG1FAuiiOyGDeoPzmn7jSdHiEv7pAFWDVz.rq}";
        Debug.Log("wallet.GetReferalCode() " + walletIdKey.Instance.GetReferal());
        

        StartCoroutine(GetRequest());
    }
    //String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjFkMTM4MTg0YmZiZjEzMjJhMTkyZjA3NGYzODhjNTcxYmZjZjlhOTNAcGFuZG9wcm9qZWN0Lm9yZyIsInJ0SWQiOiIxZDEzODE4NGJmYmYxMzIyYTE5MmYwNzRmMzg4YzU3MWJmY2Y5YTkzIiwiaWF0IjoxNjg4NTA5MTQ4LCJleHAiOjE2OTU2ODAzNDh9.JKT8OnJ7KtMofLxotDfb47_rEqLpq4AXmjR5RBhIzSI";


    IEnumerator GetRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Rurl))
        {
            Debug.Log("wallet.GetToken() "+ walletIdKey.Instance.GetToken());
            webRequest.SetRequestHeader("Authorization", "Bearer " + walletIdKey.Instance.GetToken());
            yield return webRequest.SendWebRequest();

           
            print("////////getreferal");
            try
            {
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    
                    Debug.Log("/////Error" + webRequest.error);
                }
                else
                {
                    Debug.Log("data "+webRequest.downloadHandler.text);
                    Data data = JsonUtility.FromJson<Data>(webRequest.downloadHandler.text);
                    referaltext.text = data.url;
                    walletIdKey.Instance.SetReferal(data.url);
                    Debug.Log(data.url + "///////////////////////////////" + data.message); Debug.Log("referal"+data.url);
                }
            }
            catch(Exception e)
            {
                print("GetReferalUrl error: " + e);
            }
        }
    }



  
}
