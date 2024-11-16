using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class referalURL : MonoBehaviour
{
    // Start is called before the first frame update

    private string url = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral";
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private TMP_InputField userreferal;
    [SerializeField] private walletIdKey wallet;
    [SerializeField] private TMP_Text toastmessage;
    [SerializeField] private GameObject popupobj;
    private string _referalId;

    

    public class Data
    {
        public String message;
        public List<object> data;
        public int status;
    }
    private int statuss;
    private string Rmessage;

    void Start()
    {
        wallet = FindObjectOfType<walletIdKey>();
        rtId = FindObjectOfType<GenerateRtId>();
        // request();
        // Referal();
    }
  



    public void Referal()
    {
        string _referalUrl = userreferal.text;
        string _rtid = rtId.GetRtId();
        //if(_referalUrl== "refpandoproject/$2b$10$w5xsX/b0ClfCoB/PAG1FAuiiOyGDeoPzmn7jSdHiEv7pAFWDVz.rq}") LoadGame(2);

        StartCoroutine(PostingReferalUrl(_referalUrl, _rtid));
    }

    IEnumerator PostingReferalUrl(string _referalUrl, string _rtid)
    {

        string jsonData = $"{{\"referralURL\": \"{_referalUrl}\", \"rtId\": \"{_rtid}\"}}";

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
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("/////////////url  referaled "+ request.downloadHandler.text);
                        var response = request.downloadHandler.text;
                        print(request.downloadHandler.text);
                        Data dataa = JsonUtility.FromJson<Data>(response);
                        statuss = dataa.status;
                        Rmessage = dataa.message;
                        showToast(dataa.message);
                        
                    }
                }
                catch(Exception e)
                {
                    print(e);
                }
            }
        
        }
    }


    public void LoadGame(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }









    public void showToast(string message)
    {
        StartCoroutine(ShowingToast(message));
    }

    IEnumerator ShowingToast(string message)
    {
        if (message == "This rt is trying to use its own referral")
            message = "You are trying to use Your own referral";
        if (message == "Incorrect Referral url")
            message = "You are Entering Incorrect Referral url";
        if (message == "Successfully use refrral url through RTID")
            message = "Thank you ! you have successfully used the reefrral URL";
        if (message == "Please! fill referral url field")
            message = "Please! enter referral in respective field";
        popupobj.SetActive(true);
        toastmessage.text = message;
        yield return new WaitForSeconds(3);
        toastmessage.text = "";
        popupobj.SetActive(false);
        if (statuss == 200 && Rmessage.Equals("Successfully use refrral url through RTID"))
        {
            if (PlayerPrefs.HasKey(walletIdKey.Instance.GetToken() + "activeGame"))
            {
                Debug.Log("////referal if has key");
                int k = (PlayerPrefs.GetInt(walletIdKey.Instance.GetToken()+"activeGame"));
                LoadGame(k);
            }
            else
            {
                Debug.Log("////referal else has key");
                LoadGame(2);
            }
        }

    }


   
}
