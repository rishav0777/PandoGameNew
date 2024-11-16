using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System;
using TMPro;

public class DashBoardApi : MonoBehaviour
{
    // Start is called before the first frame update
    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtDashboard?rtId=";
    [SerializeField] private GenerateRtId rtId;
    

    public class MyData
    {
        public int status;
        public string message;
        public string totalPoe;
        public string levelBasedReward;
        public int streakBonusReward;
        public int referralReward;
        public string stakeAmount;
        public string walletID;
    }

    [SerializeField] private TMP_Text streakbonusreward;

    [SerializeField] private TMP_Text levelReward;
    [SerializeField] private TMP_Text referralReward;
    [SerializeField] private TMP_Text stakeAmount;
    [SerializeField] private TMP_Text totalEarning;

    private void Start()
    {
        request();
    }

    public void request()
    {
        Debug.Log("inside dashboard request");
        url = url + rtId.GetRtId();
        StartCoroutine(GetRequest());
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
                    streakbonusreward.text = (data.streakBonusReward.ToString()).ToString();
                    levelReward.text = (data.levelBasedReward.ToString()).ToString();
                    referralReward.text = (data.referralReward.ToString()).ToString();
                    totalEarning.text= (data.totalPoe.ToString()).ToString();
                    stakeAmount.text = (data.stakeAmount.ToString()).ToString();

                }
            }
            catch (Exception e)
            {
                print("GetReferalUrl error: " + e);
            }
        }
    }


    [SerializeField] private TMP_Text toastmessage;
    [SerializeField] private GameObject popupobj;
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
