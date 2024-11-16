using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;

public class login : MonoBehaviour
{
    [SerializeField] private GenerateRtId rtId;


    private string url = "https://Testnet.rtservices.pandoproject.org/apis/RT_userLogin";

    private void Start()
    {
        LoginF();
    }




    public void LoginF()
    {
        string _username = rtId.GetRtId() + "@pandoproject.org";
        string _password = rtId.GetRtId();

        StartCoroutine(Registrations(_username, _password));
    }

    IEnumerator Registrations(string _username, string _password)
    {
        WWWForm wWForm = new WWWForm();
        wWForm.AddField("username", _username);
        wWForm.AddField("password", _password);
        //wWForm.AddField("role", "four");
        using (UnityWebRequest request = UnityWebRequest.Post(url, wWForm))
        {

            yield return request.SendWebRequest();
            var response = request.result;

            if (request.result != UnityWebRequest.Result.Success) Debug.Log(request.error);
            else if (request.result == UnityWebRequest.Result.Success)
            {
                print("Successfully Login");
               
            }
        }
    }

}