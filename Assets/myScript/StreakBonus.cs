using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

public class StreakBonus : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private walletIdKey wallet;

    [SerializeField] private TMP_Text toastmessage;
    [SerializeField] private GameObject popupobj;
    private string message;

    public class MyData
    {
        public String message;
        public int status;
    }

    private string url = "https://Testnet.rtservices.pandoproject.org/apis/streakBonus";
    private void Start()
    {
        
        //Bonus();
    }
    public void Bonus()
    {
        Debug.Log("insise bonus");
        string _rtid = rtId.GetRtId();
        string _token = walletIdKey.Instance.GetToken();
        StartCoroutine(Bonusing(_rtid,_token));
    }

    IEnumerator Bonusing(string _rtid,string _token)
    {
        string jsonData = $"{{\"rtId\": \"{_rtid}\"}}";
        Debug.Log("insise bonusing"+_rtid+"  ");
        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + walletIdKey.Instance.GetToken());

                yield return request.SendWebRequest();

                try
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(request.error);
                        showToast("You have already marked for today");
                    }
                    else
                    {
                        print("Successfully Streak Bonus " + request.downloadHandler.text);
                        var json = request.downloadHandler.text;
                        MyData data = JsonUtility.FromJson<MyData>(json);
                        showToast(data.message);
                    }
                }
                catch (Exception e)
                {
                    Debug.Log("Exceptions "+e);
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
