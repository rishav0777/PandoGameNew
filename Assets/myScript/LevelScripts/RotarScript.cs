using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotarScript : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject rotar;
    public float rotatevalue=100f;
    

    // Update is called once per frame
    void Update()
    {
        rotar.transform.Rotate(0f, rotatevalue * Time.deltaTime, 0f);
    }
}
