using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;
using System;

public class TransactionService : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject referal;
    [SerializeField] private GameObject register;
    [SerializeField] private TMP_InputField senderAddress;
    [SerializeField] private TMP_InputField privatekey;
    [SerializeField] private GenerateRtId rtId;

    [SerializeField] private walletIdKey wallet;
    private string _senderaddress;
    private string _privatekey;

    [SerializeField] private TMP_Text toastmessage;

    [SerializeField] private GameObject popup;

    [SerializeField] private Updatertid updatertid;

    [SerializeField] private GameObject popupobj;

    private String Rmessage;

    public class Data
    {
       public String message;
       public String data;
       public int status;
    }
    private int Rstatus = 0;


    private string url = "https://Testnet.rtservices.pandoproject.org/apis/rtMobileTransaction";
    

    public void SwitchToReferal()
    {
        register.SetActive(false);
        referal.SetActive(true);
    }
    public void LoadGame(int _sceneIndex)
    {
        SceneManager.LoadScene(_sceneIndex);
    }





    private void Start()
    {
        
        wallet = FindObjectOfType<walletIdKey>();
        rtId = FindObjectOfType<GenerateRtId>();
        Debug.Log("////////TransactionalServices check wallet" + walletIdKey.Instance.GetWalletId());

        if(walletIdKey.Instance.GetWalletId() == "" || walletIdKey.Instance.GetWalletId() == null) { }
        else
        {
            Debug.Log(walletIdKey.Instance.GetWalletId());
             //SwitchToReferal();
            LoadGame(2);
        }
        //Register();
    }




    public void Register()
    {

     
        _senderaddress = senderAddress.text;
        _privatekey = privatekey.text;
        string rtid = rtId.GetRtId();

        wallet.SetWalletId(_senderaddress);
        CheckForPrivateKey(_privatekey);
        CheckForWallet(_senderaddress);
        StartCoroutine(Registrations(_senderaddress,_privatekey,rtid));
    }

    IEnumerator Registrations(string _username, string _password,string rtid)
    {
        string jsonData = $"{{\"senderAddress\": \"{_username}\", \"privateKey\": \"{_password}\",\"rtId\": \"{rtid}\"}}";

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
                print("registration");

                yield return request.SendWebRequest();
                try
                {
                    print("in try");
                    if (request.result != UnityWebRequest.Result.Success) Debug.Log("error "+request.error);
                    else if (request.result == UnityWebRequest.Result.Success)
                    {
                        var json = request.downloadHandler.text;
                        print("transaction output "+request.downloadHandler.text);
                        Data data = JsonUtility.FromJson<Data>(json);
                        Rmessage = data.message;
                        Rstatus = data.status;
                        Onclick();
                         showToast(data.message);

                    }
                }
                catch(Exception e)
                {
                    print("TransactionalServices error: " + e);
                }
            }
        }
    }






    public bool CheckForWallet(string w)
    {
        int walletlength = w.Length;print("walletlength"+walletlength);
        if (walletlength >= 40 && walletlength <= 42) return true;

       // showToast("Invalid WalletID or Private key");
        return false;
    }
    public bool CheckForPrivateKey(string p)
    {
        int privatelength = p.Length;print("privatelength"+privatelength);
        if (privatelength >= 64 && privatelength <= 66) return true;

        //showToast("Invalid WalletID or Private key");
        return false;
    }







    [SerializeField]
    private GameObject _nextGameobject;

    public void Onclick()
    {
        Rstatus = 0;
        if (Rmessage.Equals("RTId wallet verification has been completed")) Rstatus = 200;
       // Register();
        Debug.Log("message " + Rmessage);
        if (Rmessage.Equals("Other rtId is Already exist for this wallet"))
        {
            print("inside popup");
            popup.SetActive(true);
        }
        else if (_nextGameobject != this.gameObject && _nextGameobject != null && Rstatus == 200 &&
            CheckForPrivateKey(_privatekey) && CheckForWallet(_senderaddress))
        {
            print("on Activate");
            _nextGameobject.SetActive(true);
            transform.gameObject.SetActive(false);
        }

    }

    public void ClickedNo()
    {
        popup.SetActive(false);
        senderAddress.text = "";
        privatekey.text = "";
        popupobj.SetActive(false);
        //transform.gameObject.SetActive(false);
        /* if (_nextGameobject != this.gameObject && _nextGameobject != null &&
             CheckForPrivateKey(_privatekey) && CheckForWallet(_senderaddress))
         {
             print("on Activate");
             _nextGameobject.SetActive(true);
             transform.gameObject.SetActive(false);
         }*/
    }
    public void ClickedYes()
    {
        updatertid.UpdateRtId(_senderaddress);
    }






    public void showToast(string message)
    {
        StartCoroutine(ShowingToast(message));
    }

    IEnumerator ShowingToast(string message)
    {
        
        if (message == "This wallet id has been used for activation key") 
            message = "This Wallet Address already registered for Other Rametron Type ! ";
        popupobj.SetActive(true);
        toastmessage.text = message;
        yield return new WaitForSeconds(3);
        toastmessage.text = "";
        popupobj.SetActive(false);

    }

}
//_nextGameobject != this.gameObject && _nextGameobject != null && 