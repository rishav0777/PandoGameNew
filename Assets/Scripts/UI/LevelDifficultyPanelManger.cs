using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelDifficultyPanelManger : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> _panels = new List<GameObject>();

    
    private void Update()
    {
        
    }

    public void ExpandUI()
    {                

        foreach (var panel in _panels)
        {
          panel.gameObject.transform.position = new Vector3(panel.gameObject.transform.position.x, panel.gameObject.transform.position.y - 450, 0);
        }
    }

    public void ColapseUI()
    {

        foreach (var panel in _panels)
        {
            panel.gameObject.transform.position = new Vector3(panel.gameObject.transform.position.x, panel.gameObject.transform.position.y + 450, 0);
        }
    }

}
