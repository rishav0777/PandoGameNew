using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using System;

public class RandomAdvertisement : MonoBehaviour
{
    // Start is called before the first frame update
    private string url = "https://Testnet.rtservices.pandoproject.org/apis/randomCampaign";
    [SerializeField] private GenerateRtId rtId;
    [SerializeField] private walletIdKey wallet;

    [SerializeField] private GameObject cross;
    [SerializeField] private GameObject nextscene;

   
    public class AdDetail
    {
        public string redirectURL;
        public string videoURL;
    }

    
    public class AdData
    {
        public AdDetail[] adDetail;
        public string message;
    }
    private void Start()
    {
        Debug.Log("starting advertisement"+walletIdKey.Instance.GetAUrl());
        video.gameObject.SetActive(true);
        //request();

        if (walletIdKey.Instance.GetAUrl() == null || walletIdKey.Instance.GetAUrl() == "")
        {
            SkipAd();
            ShowingAd("https://testing1.pandoprojectdata.org/filedownload?fileId=648ffe94660b03ada4c42516&sessionKey=5f7a148d-d303-42ad-9e77-be104c2f9eac");
        }
        else
            ShowingAd(walletIdKey.Instance.GetAUrl());

    }

    public void request()
    {
        Debug.Log("inside request ");
        StartCoroutine( GetRequest());
    }
    IEnumerator GetRequest()
    {
        Debug.Log("inside getrequest");
        using (UnityWebRequest webRequest = UnityWebRequest.Get(url))
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
                   // ShowingAd(videoURL);

                }
            }
            catch(Exception e)
            {
                print(e);
            }
           
        }
    }



    public VideoPlayer video;

    public void ShowingAd(string videoUrl)
    {
        Debug.Log("showing ad");
        video.url = videoUrl;
        video.audioOutputMode = VideoAudioOutputMode.AudioSource;
        video.EnableAudioTrack(0, true);
        video.prepareCompleted += CallVideoPlayer;
        
        video.Prepare();
    }
    public void CallVideoPlayer(VideoPlayer s)
    {
        StartCoroutine(Toskip());
        video.Play();

    }

    IEnumerator Toskip()
    {
        yield return new WaitForSeconds(3);
       // cross.gameObject.SetActive(true);
        
    }

    public void SkipAd()
    {
        transform.gameObject.SetActive(false);
        cross.gameObject.SetActive(false);
        nextscene.gameObject.SetActive(true);
        video.gameObject.SetActive(false);
    }


    
}
