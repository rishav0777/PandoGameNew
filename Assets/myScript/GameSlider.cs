using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameSlider : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI handlevalue;
    // Start is called before the first frame update
   public void Onchange(float val)
    {
        handlevalue.text = val.ToString();
    }
}
