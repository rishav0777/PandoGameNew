using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canon : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject goalPrefab;
    [SerializeField] private GameObject parent;
    [SerializeField] private GameObject InitialPos;
    public float forcevaluex=100f;
    public float forcevaluey = 100f;
    public float forcevaluez = 0f;
    public float posx = 100f;
    public float posy = 100f;
    public float posz = 0f;
    public float firingtime = 2f;


    private void Start()
    {
        Debug.Log("instart");
;       StartCoroutine(StariFiring());
    }
    IEnumerator StariFiring()
    {
        Debug.Log("instart ienumerator");
        Firing();
        yield return new WaitForSeconds(firingtime);
        Debug.Log("instart after wait");
        StartCoroutine(StariFiring());
    }

    public void Firing()
    {
        
        Vector3 pos = new Vector3(posx, posy, posz);
        Debug.Log("instart firing" + pos);
        GameObject gola=Instantiate(goalPrefab);
        gola.transform.SetParent(parent.transform);
        gola.transform.position = InitialPos.transform.position;
       // gola.gameObject.transform.position = pos;// Camera.main.ScreenToWorldPoint(pos);
        gola.GetComponent<Rigidbody>().AddForce(forcevaluex, forcevaluey, forcevaluez, ForceMode.Impulse);
    }
}
