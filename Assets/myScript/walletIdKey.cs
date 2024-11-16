using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;
using TMPro;
using System;

public class walletIdKey : MonoBehaviour
{
    private static walletIdKey instance;

    public static walletIdKey Instance
    {
        get { return instance; }
    }

    private void Awake()
    {
        if(instance!=null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
    }


    private String walletid ;
    private String _token;
    private String _rtid;
    private String _Rcode;//= "refpandoproject/$2b$10$w5xsX/b0ClfCoB/PAG1FAuiiOyGDeoPzmn7jSdHiEv7pAFWDVz.rq}";
    private String _Level;
    private String _AUrl;
    public string GetWalletId()
    {
        Debug.Log("Getwalletid called " + walletid);
        return walletid;
    }
    public void SetWalletId(String walletId)
    {
        walletid = walletId;
        Debug.Log("setwalletid called " + walletid);
    }



    public string GetToken()
    {
       // Debug.Log("GetToken called " + _token);
        return _token;
    }
    public void SetToken(String token)
    {
        _token = token;
        Debug.Log("setToken called " + _token);
    }
    public string GetRtid()
    {
        Debug.Log("Getrtid called " + _rtid);
        return _rtid;
    }
    public void SetRtid(String rtid)
    {
        _rtid = rtid;
        Debug.Log("setrtid called " + _rtid);
    }

    


    
    public  string GetReferal()
    {
        Debug.Log("GetReferal called " + _Rcode);
        return _Rcode;
    }
    public void  SetReferal(String code)
    {
        _Rcode = code;
        Debug.Log("setReferal called " + _Rcode);
    }



    public string GetLevel()
    {
        Debug.Log("GetLevel called " + _Level);
        return _Level;
    }
    public void SetLevel(String level)
    {
        _Level = level;
        Debug.Log("SetLevel called " + _Level);
    }



    public string GetAUrl()
    {
        Debug.Log("GetAUrl called " + _AUrl);
        return _AUrl;
    }
    public void SetAUrl(String code)
    {
        _AUrl = code;
        Debug.Log("setAUrl called " + _AUrl);
    }
}
