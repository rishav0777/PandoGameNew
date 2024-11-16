using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopButton : MonoBehaviour
{
    [SerializeField]
    private ShopTemplate _btnparent;

    private void Start()
    {
        _btnparent = this.GetComponentInParent<ShopTemplate>();

        OnSkinSlected();
    }

    public void OnSkinSlected()
    {
        
        ShopManager.instance.OnSkinItemSelected(_btnparent);
    }
   
}
