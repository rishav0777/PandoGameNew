using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using System;

public class Updatertid : MonoBehaviour
{
    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileUpdateRt";
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private walletIdKey wallet;

    [SerializeField] private GameObject _nextgameobject;
   
   

    public void UpdateRtId(String _walletId)
    {
        string _walletid = _walletId;
        String _token=wallet.GetToken();
        string _rtid = rtId.GetRtId();

        StartCoroutine(Updating(_walletid, _rtid,_token));
    }

    IEnumerator Updating(string _walletid, string _rtid,String _token)
    {

        string jsonData = $"{{\"rtId\": \"{_rtid}\", \"walletId\": \"{_walletid}\"}}";

        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Put(url, ""))
            {
                
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + _token);
                request.method = "PATCH";

                yield return request.SendWebRequest();
                var response = request.result;
                try
                {
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        var json = request.downloadHandler.text;
                        print("updatertid output " + request.downloadHandler.text);
                        print("successfully updated rtid ");

                        _nextgameobject.SetActive(true);
                        transform.gameObject.SetActive(false);
                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }
    }



}
