using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

public class LeveBasedReward : MonoBehaviour
{
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private walletIdKey wallet;
    [SerializeField] private string gameLevel="8";
    [SerializeField] private string gameId = "game1";

    [SerializeField] private TMP_Text toastmessage;
    [SerializeField] private GameObject popupobj;

    private string url = "https://Testnet.rtservices.pandoproject.org/apis/levelReward";


    private void Start()
    {
        wallet = FindObjectOfType<walletIdKey>();
        rtId = FindObjectOfType<GenerateRtId>();
    }
    public class MyData
    {
       
        public string message;
        public string data;
        public int status;
        
    }

    public void Reward()
    {
        Debug.Log("Inside levelReward");
        string _rtid = rtId.GetRtId();
        string _token = walletIdKey.Instance.GetToken();
        gameLevel = walletIdKey.Instance.GetLevel();
        StartCoroutine(Rewarding(_rtid, gameLevel, gameId,_token));
    }

    IEnumerator Rewarding(string _rtid,string _gamelevel,string _gameid,string _token)
    {
        
        string jsonData = $"{{\"rtId\": \"{_rtid}\",\"level\": \"{_gamelevel}\",\"gameId\": \"{_gameid}\"}}";
        Debug.Log("insise levelbonusing");
        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            Debug.Log("insise if levelbonusing");
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + _token);

                yield return request.SendWebRequest();
                try
                {
                    Debug.Log("insise try levelbonusing");

                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        Debug.Log(request.downloadHandler.text);
                        MyData data = JsonUtility.FromJson<MyData>(request.downloadHandler.text);
                        print("Successfully LevelBased Reward ");
                        //showToast(data.message);
                    }
                }
                catch(Exception e)
                {
                    Debug.Log("Exception " + e);
                }
            }
        }
    }



    public void showToast(string message)
    {
        StartCoroutine(ShowingToast(message));
    }

    IEnumerator ShowingToast(string message)
    {
        popupobj.SetActive(true);
        toastmessage.text = message;
        yield return new WaitForSeconds(3);
        toastmessage.text = "";
        popupobj.SetActive(false);

    }

}
