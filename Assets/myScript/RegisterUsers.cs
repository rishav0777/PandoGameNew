using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System.Text.RegularExpressions;
using System;
using UnityEngine.SceneManagement;


public class RegisterUsers : MonoBehaviour
{

    // [SerializeField] private GameObject loginPage;
    //[SerializeField] private TMP_InputField username;
    //[SerializeField] private TMP_InputField password;

    [SerializeField] private GameObject popupobj;
    [SerializeField] private TMP_Text toastmessage;
    private String registrarionMeassage;
    private String loginMessage;


    [SerializeField] private GenerateRtId rtId;
    private bool loginFlag = false;
    private bool verificationFlag = false;

    private string Token;


    private string url = "https://Testnet.rtservices.pandoproject.org/apis/RT_userRegistartion";
    private string Rurl = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileCreateRt";
    private string lurl = "https://Testnet.rtservices.pandoproject.org/apis/RT_userLogin";


    public class MyData
    {
        public string message;
        public string token;
    }

    public class verificationResponse
    {
        public string message;
        public string walletId;
        public int status;
    }
    public class registerData
    {
        public string success;
        public string message;
        public int status;
    }

    string _walletId;
    [SerializeField] private walletIdKey wallet;

    public void LoadGame(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }

    private void Start()
    {
        wallet = FindObjectOfType<walletIdKey>();
        rtId = FindObjectOfType<GenerateRtId>();
        Register();
    }


    public void Register()
    {
        string _username = rtId.GetRtId() + "@pandoproject.org";
        string _password = rtId.GetRtId();

        StartCoroutine(Registrations(_username, _password));
    }

    IEnumerator Registrations(string _username, string _password)
    {


        string jsonData = $"{{\"username\": \"{_username}\", \"password\": \"{_password}\",\"role\": \"four\"}}";
        Debug.Log(jsonData);
        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(url, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();
                var response = request.result;
                try
                {
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("Successfully registered ");
                        var json = request.downloadHandler.text;
                        Debug.Log(json);
                        registerData data = JsonUtility.FromJson<registerData>(json);
                        if (data.message == "Registration is not done let's try again later") { showToast(""); }
                        else showToast(data.message);
                        LoginF();
                    }
                }
                catch (Exception e)
                {
                    print(e);
                }
            }
        }
    }










    public void LoginF()
    {
        string _username = rtId.GetRtId() + "@pandoproject.org";
        string _password = rtId.GetRtId();


        StartCoroutine(Logging(_username, _password));
    }

    IEnumerator Logging(string _username, string _password)
    {
        Debug.Log("username " + _username); Debug.Log("password " + _password); Debug.Log("lurl " + lurl);

        string jsonData = $"{{\"username\": \"{_username}\", \"password\": \"{_password}\",\"role\": \"four\"}}";

        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(lurl, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");

                yield return request.SendWebRequest();
                try
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log("error " + request.error);
                        loginMessage = request.result.ToString();
                    }
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        loginFlag = true;
                        var json = request.downloadHandler.text;
                        MyData data = JsonUtility.FromJson<MyData>(json);
                        Token = data.token;
                        // Debug.Log(json); Debug.Log(Token); Debug.Log(data.message);
                        walletIdKey.Instance.SetToken(Token);
                        loginMessage = data.message;
                        if (loginMessage == "Token is genrate") loginFlag = false;
                        //showToast(data.message);
                        Arequest();
                        rrequest();
                        CreateVerify(Token);
                    }
                }
                catch (Exception e)
                {
                    print("Inner try Block: " + e);
                }
            }
        }


    }











    public void CreateVerify(string token)
    {
        //token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6ImE5Y2Y2ZjczLWU2NWUtNDg3Yy04ZmVkLWRhMWJmNGVjMTk4OUBwYW5kb3Byb2plY3Qub3JnIiwicnRJZCI6ImE5Y2Y2ZjczLWU2NWUtNDg3Yy04ZmVkLWRhMWJmNGVjMTk4OSIsImlhdCI6MTY4ODA1OTgwNywiZXhwIjoxNjk1MjMxMDA3fQ.wWsboFfH6u2lcRI1B0vB4ihiO8mllNvtDwsGYguwJUM";
        print("Enter Create verifying");
        string _rtId = rtId.GetRtId();
        StartCoroutine(Verifications(_rtId, token));
    }

    IEnumerator Verifications(string _rtId, string token)
    {
        string jsonData = $"{{\"rtId\": \"{_rtId}\"}}";//," +
        /*   $" \"walletId\": \"{null}\"," +
           $"\"latitude\": \"8.888888\", " +
           $"\"longitude\": \"88.99\", " +
           $"\"rtType\": \"rtMobile\", " +
           $"\"created\": \"{true}\"," +
           $"\"allFreeMemory\": \"{854.6640625}\", " +
           $"\"availableRam\": \"{7926.78125}\", " +
           $"\"core\": \"{4}\", " +
           $"\"cpu\": \"11th Gen Intel(R) Core(TM)i3-1115G4 @ 3.00GHz\", " +
           $"\"osPlateform\": \"Android 12,MIUI 14\", " +
           $"\"osType\": \"Android\", " +
           $"\"osVersion\": \"Android 12\", " +
           $"\"speed\": \"56\", " +
           $"\"stake\": \"0\", " +
           $"\"country\": \"india\"}}";*/

        Debug.Log(jsonData);
        // Validate the data fields before sending the request
        if (!string.IsNullOrEmpty(jsonData))
        {
            using (UnityWebRequest request = UnityWebRequest.Post(Rurl, ""))
            {
                byte[] bodyRaw = System.Text.Encoding.UTF8.GetBytes(jsonData);

                request.uploadHandler = new UploadHandlerRaw(bodyRaw);
                request.downloadHandler = new DownloadHandlerBuffer();
                request.SetRequestHeader("Content-Type", "application/json");
                request.SetRequestHeader("Authorization", "Bearer " + token);
                yield return request.SendWebRequest();

                try
                {
                    if (request.result != UnityWebRequest.Result.Success)
                    {
                        Debug.Log(request.error + request.downloadHandler.text);
                        registrarionMeassage = request.result.ToString();
                    }
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        print("Successfully created and verified");
                        //verificationFlag = true;
                        var response = request.downloadHandler.text;
                        print(request.downloadHandler.text);
                        verificationResponse data = JsonUtility.FromJson<verificationResponse>(response);
                        _walletId = data.walletId;
                        registrarionMeassage = data.message;
                        Debug.Log("register user" + _walletId);
                        walletIdKey.Instance.SetWalletId(_walletId);
                        //if (_walletId != null) LoadGame(2);
                    }
                }
                catch (Exception e)
                {
                    print("verification error: " + e);
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
        if(message!="")popupobj.SetActive(true);
        if (message == "RT-user Registration is successfully Done !!")
            message = "Succcefully get registered the Device";
        toastmessage.text = message;
        yield return new WaitForSeconds(2);
        toastmessage.text = "";
        popupobj.SetActive(false);
        if (loginFlag) { loginFlag = false; showToast(loginMessage); }
        else if (verificationFlag) { verificationFlag = false; showToast(registrarionMeassage); }


    }












    private string Rrurl = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral?rtId=";
    public class Daata
    {
        public string url;
        public string message;
    }


    public void rrequest()
    {
        Rrurl = Rrurl + rtId.GetRtId();
        print("/////////////////////////////////////getreferal" + Rrurl);

        StartCoroutine(GettRequest());
    }
    //String token = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJ1c2VybmFtZSI6IjFkMTM4MTg0YmZiZjEzMjJhMTkyZjA3NGYzODhjNTcxYmZjZjlhOTNAcGFuZG9wcm9qZWN0Lm9yZyIsInJ0SWQiOiIxZDEzODE4NGJmYmYxMzIyYTE5MmYwNzRmMzg4YzU3MWJmY2Y5YTkzIiwiaWF0IjoxNjg4NTA5MTQ4LCJleHAiOjE2OTU2ODAzNDh9.JKT8OnJ7KtMofLxotDfb47_rEqLpq4AXmjR5RBhIzSI";


    IEnumerator GettRequest()
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Rrurl))
        {
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
                    Debug.Log("data " + webRequest.downloadHandler.text);
                    Daata data = JsonUtility.FromJson<Daata>(webRequest.downloadHandler.text);
                    Debug.Log("data " + data.url);
                    walletIdKey.Instance.SetReferal(data.url);
                }
            }
            catch (Exception e)
            {
                print("GetReferalUrl error: " + e);
            }
        }
    }







    private string Aurl = "https://Testnet.rtservices.pandoproject.org/apis/randomCampaign";

    public class AdDetail
    {
        public string redirectURL;
        public string videoURL;
    }


    public class AdData
    {
        public List<AdDetail> adDetail;
        public string message;
    }

    public void Arequest()
    {
        Debug.Log("inside request ");
        StartCoroutine(GetARequest());
    }
    IEnumerator GetARequest()
    {
        Debug.Log("inside getrequest");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(Aurl))
        {
            Debug.Log("inside getrequest using");
            webRequest.SetRequestHeader("Authorization", "Bearer " + walletIdKey.Instance.GetToken());
            yield return webRequest.SendWebRequest();
            try
            {
                if (webRequest.result != UnityWebRequest.Result.Success)
                {
                    Debug.Log(webRequest.error);
                    Debug.Log("getrequest failure");
                }
                else
                {
                    Debug.Log(webRequest.downloadHandler.text);
                    AdData adData = JsonUtility.FromJson<AdData>(webRequest.downloadHandler.text);
                    string videoURL = adData.adDetail[0].videoURL;
                    Debug.Log("getrequest success");
                    print(videoURL);
                    walletIdKey.Instance.SetAUrl(videoURL);
                    // ShowingAd(videoURL);

                }
            }
            catch (Exception e)
            {
                print(e);
            }

        }

    }
}

