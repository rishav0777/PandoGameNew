using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdWaitTime : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField]
    private float _waitTime=10f;


    [SerializeField]
    private GameObject nextscene;
    void Start()
    {
        StartCoroutine(Wait());
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(_waitTime);
        SkipAd();
        
    }
    public void SkipAd()
    {
        transform.gameObject.SetActive(false);

        nextscene.gameObject.SetActive(true);

    }
}
