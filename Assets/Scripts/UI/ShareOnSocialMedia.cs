using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using NativeShareNamespace;
using System.IO;

public class ShareOnSocialMedia : MonoBehaviour
{
    [SerializeField]
    private GameObject _panelShare;
    [SerializeField]
    private string _referralCode;

    //for test purpose only
    [SerializeField]
    private string _demoReferaalCode;

    [SerializeField] private walletIdKey wallet;


    private void Start()
    {
        _panelShare.SetActive(false);

        _demoReferaalCode = "refpandoproject /$2b$10$w5xsX / b0ClfCoB / PAG1FAuiiOyGDeoPzmn7jSdHiEv7pAFWDVz.rq}";
    }

    public void ShareReferral()
    {
        _panelShare.SetActive(true);

        StartCoroutine(ShareReferralCode());
    }

    public void Share()
    {
        StartCoroutine(ShareReferralCode());
    }

	private IEnumerator ShareReferralCode()
	{
		yield return new WaitForEndOfFrame();

		

		new NativeShare()
			.SetSubject("Referral Code").
            SetText("Code: " + _demoReferaalCode)
			.SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
			.Share();
        
	}

}
