using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class UseReferralUrl : MonoBehaviour
{
    public string referralURL;
    public string rtId;

    public void SubmitMobileReferral()
    {
        StartCoroutine(SubmitMobileReferralCoroutine());
    }

    IEnumerator SubmitMobileReferralCoroutine()
    {
        string url = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral";

        Dictionary<string, string> headers = new Dictionary<string, string>();
        headers.Add("Content-Type", "application/json");

        string jsonBody = "{\"referalURL\": \"" + referralURL + "\", \"rId\": \"" + rtId + "\"}";

        UnityWebRequest request = UnityWebRequest.Put(url, jsonBody);
      
        request.downloadHandler = new DownloadHandlerBuffer();

        foreach (KeyValuePair<string, string> header in headers)
        {
            request.SetRequestHeader(header.Key, header.Value);
        }

        yield return null;

        if (request.result == UnityWebRequest.Result.Success)
        {
            Debug.Log("Mobile referral submitted successfully!");
            Debug.Log(request.downloadHandler.text);
        }
        else
        {
            Debug.LogError("Error submitting mobile referral: " + request.error);
        }
    }
}
