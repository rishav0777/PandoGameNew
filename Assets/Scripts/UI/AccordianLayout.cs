using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AccordianLayout : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _accordianTabs;

    [SerializeField]
    private List<GameObject> _panel, _body;


    [SerializeField]
    private List<float> _originalPos;

    [SerializeField]
    private float _shiftamount;

    private void Start()
    {        

        foreach (var tab in _accordianTabs)
        {
            _originalPos.Add(tab.transform.position.y);
        }
    }

    public void Expand(int _index)
    {
        for(int i = _index + 1; i < _accordianTabs.Count; i++)
        {
            _accordianTabs[i].transform.position = new Vector3(_accordianTabs[i].transform.position.x, _accordianTabs[i].transform.position.y - _shiftamount, _accordianTabs[i].transform.position.z);
        }

        CollapseOtherTabs(_index);
    }

    private void CollapseOtherTabs(int index)
    {
        for(int i = 0; i < _accordianTabs.Count; i++)
        {
            if(i <= index)
            {
                _accordianTabs[i].transform.position = new Vector3(_accordianTabs[i].transform.position.x, _originalPos[i], _accordianTabs[i].transform.position.z);
            }            

            if(i != index)
            {
                _panel[i].SetActive(true);
                _body[i].SetActive(false);
            }                       

            if(index != 0)
            {

            }
        }
        
    }
    public void Collapse()
    {
        for(int i = 0; i < _accordianTabs.Count; i++)
        {
            _accordianTabs[i].transform.position = new Vector3(_accordianTabs[i].transform.position.x,_originalPos[i], _accordianTabs[i].transform.position.z);
        }
    }

}
