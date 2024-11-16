using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
public class ReferralID : MonoBehaviour
{
    [SerializeField]
    private string _url = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral?";
    [SerializeField]
    private string _sendUrl;

    [SerializeField]
    private string rtID;

    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;

    // Start is called before the first frame update
    private void OnEnable()
    {
        rtID = SystemInfo.deviceUniqueIdentifier.ToString();

        _sendUrl = _url + "rtID=" + rtID;

        StartCoroutine(GetReferralData());
    }
    // Update is called once per frame
    void Update()
    {        

    }



    IEnumerator GetReferralData()
    {
        string url = "https://Testnet.rtservices.pandoproject.org/apis/mobileReferral?rtId=sakshi";

        using (UnityWebRequest webRequest = UnityWebRequest.Get(_sendUrl))
        {
            yield return webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                Debug.Log(webRequest.error);
               // _textMeshProUGUI.text = webRequest.result.ToString();
            }
            else
            {
                Debug.Log(webRequest.downloadHandler.text);
              //  _textMeshProUGUI.text = webRequest.downloadHandler.text;
                print(" hh "+_textMeshProUGUI.text);

            }
        }
    }
}
