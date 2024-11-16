using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UpdateRTID : MonoBehaviour
{
    public string rtId;
    public string walletId;

    public void UpdateRTMobile()
    {
        StartCoroutine(UpdateRTMobileCoroutine());
    }

    IEnumerator UpdateRTMobileCoroutine()
    {
        string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileUpdateRt";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        string jsonBody = "{\"rtId\": \"" + rtId + "\", \"waletId\": \"" + walletId + "\"}";

        UnityWebRequest request = UnityWebRequest.Put(url, jsonBody);
       
        request.downloadHandler = new DownloadHandlerBuffer();

        foreach (KeyValuePair<string, string> header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("RT Mobile updated successfully!");
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error updating RT Mobile: " + request.error);
        }
    }
}
