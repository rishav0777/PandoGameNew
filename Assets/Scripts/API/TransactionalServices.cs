using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class TransactionalServices : MonoBehaviour
{

    public string senderAddress;
    public string privateKey;
    public string rtId;

    public void SendRTMobileTransaction()
    {
        StartCoroutine(SendRTMobileTransactionCoroutine());
    }

    IEnumerator SendRTMobileTransactionCoroutine()
    {
        string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileTransaction";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        string jsonBody = "{\"senderAdres\": \"" + senderAddress + "\", \"privateKey\": \"" + privateKey + "\", \"rtId\": \"" + rtId + "\"}";

        UnityWebRequest request = UnityWebRequest.Post(url, jsonBody);
       
        request.downloadHandler = new DownloadHandlerBuffer();

        foreach (KeyValuePair<string, string> header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("RT Mobile Transaction sent successfully!");
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error sending RT Mobile Transaction: " + request.error);
        }
    }

    internal void ClickedNo()
    {
        throw new NotImplementedException();
    }
}
