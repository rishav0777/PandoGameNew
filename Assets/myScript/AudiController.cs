using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudiController : MonoBehaviour
{
    public AudioSource coinCollection;
    public AudioSource running;
    public AudioSource fighting;
    public AudioSource blooding;
    public AudioSource winning;
    public AudioSource Loosing;

    public AndroidJavaObject vibrate;
    
    IEnumerator _OnWinning()
    {
        Debug.Log("////winning");
        CloseAudio();
        winning.Play();
        yield return null;
    }
    IEnumerator _Onrunning()
    {
        Debug.Log("////running");
        CloseAudio();
        running.Play();
        yield return null;
    }
    IEnumerator _OnCollecting()
    {
        Debug.Log("////collecting");
        //CloseAudio();
        coinCollection.Play();
        yield return null;
    }
    IEnumerator _OnBlooding()
    {
        Debug.Log("////blooding");
        // CloseAudio();
        blooding.Play();
        yield return null;
    }
    IEnumerator _OnFighting()
    {
        Debug.Log("////fighting");
        CloseAudio();
        fighting.Play();
        yield return null;
    }
    IEnumerator _OnLoosing()
    {
        Debug.Log("////loosing");
        CloseAudio();
        Loosing.Play();
        yield return null;
    }

    IEnumerator _CloseAudio()
    {
        Debug.Log("////closing all audio");
        //winning.Stop();
        running.Stop();
        //coinCollection.Stop();
        //blooding.Stop();
        fighting.Stop();
        //Loosing.Stop();
        yield return null;
    }

    IEnumerator _OnVibration()
    {
        if (Application.platform == RuntimePlatform.Android)
        {
            vibrate.Call("vibrate", 400);
        }
        yield return null;
    }



    public void OnWinning()
    {
        Debug.Log("////winning");
        StartCoroutine(_OnWinning());
    }
    public void Onrunning()
    {
        Debug.Log("////running");
        StartCoroutine(_Onrunning());
    }
    public void OnCollecting()
    {
        Debug.Log("////collecting");
        StartCoroutine(_OnCollecting());
    }
    public void OnBlooding()
    {
        Debug.Log("////blooding");
        StartCoroutine(_OnBlooding());
    }
    public void OnFighting()
    {
        Debug.Log("////fighting");
        StartCoroutine(_OnFighting());
    }
    public void OnLoosing()
    {
        Debug.Log("////loosing");
        StartCoroutine(_OnLoosing());
    }

    public void CloseAudio()
    {
        Debug.Log("////closing all audio");
        StartCoroutine(_CloseAudio());
    }

    public void OnVibration()
    {
        StartCoroutine(_OnVibration());
    }
}
