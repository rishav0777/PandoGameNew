using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using System.Text;
using Newtonsoft.Json;

public class UniqueDeviceId : MonoBehaviour
{
    [SerializeField]
    private string _MobileRegistrationUrl;
    [SerializeField]
    private string _tokenUrl;

    private string _username;
    private string _password;
    private string _role;

    [SerializeField]
    private string _testAdder = "A";

    private string _mobileRegistrationBodyJsonString;
    private string _tokenBodyJsonString;

    [SerializeField]
    private bool _userRegistered;

    void Start()
    {
        string uniqueID = SystemInfo.deviceUniqueIdentifier.ToString();

        _username = _testAdder + uniqueID + "@pandoproject.org";
        _password = uniqueID;

        _role = "four";


        Debug.Log("UserName: " + _username);
        Debug.Log("Password: " + _password);
        _userRegistered = false;

         UserData();
       

    }

    [System.Serializable]
    public class RegistartionData
    {
        public string username;
        public string password;
        public string role;

        public RegistartionData(string rtID_username, string rtID_password, string rtID_role)
        {
            username = rtID_username;
            password = rtID_password;
            role = rtID_role;
        }
    }

    void UserData()
    {
        RegistartionData loginData = new RegistartionData(_username, _password, _role);

        // Convert the dictionary to JSON format
        string jsonData = JsonUtility.ToJson(loginData);

        // Create a UnityWebRequest object to post the data to the server
        UnityWebRequest request = new UnityWebRequest(_MobileRegistrationUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(SendLoginRequest(request));
    }

    private IEnumerator SendLoginRequest(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        try
        {
            if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log("Login successful: " + request.downloadHandler.text);
                GetToken();
            }
        }
        finally { }

     
    }

   

    


    [System.Serializable]
    public class LoginReturnToken
    {
        public string message;
        public string token;
        public string role;

        public LoginReturnToken(string rtID_Message, string rtID_Token, string rtID_Role)
        {
            message = rtID_Message;
            token = rtID_Token;
            role = rtID_Role;
        }
    }

    void GetToken()
    {
        RegistartionData loginData = new RegistartionData(_username, _password, _role);

        // Convert the dictionary to JSON format
        string jsonData = JsonUtility.ToJson(loginData);

        // Create a UnityWebRequest object to post the data to the server
        UnityWebRequest request = new UnityWebRequest(_tokenUrl, "POST");
        byte[] bodyRaw = Encoding.UTF8.GetBytes(jsonData);
        request.uploadHandler = new UploadHandlerRaw(bodyRaw);
        request.downloadHandler = new DownloadHandlerBuffer();
        request.SetRequestHeader("Content-Type", "application/json");

        StartCoroutine(MobileLogin(request));

    }

    IEnumerator MobileLogin(UnityWebRequest request)
    {
        yield return request.SendWebRequest();

        if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
        {
            Debug.LogError(request.error);
        }
        else
        {
            Debug.Log("token successful: " + request.downloadHandler.text);
            
        }
    }

    /////////////////////////////////////////////
    ///


}
