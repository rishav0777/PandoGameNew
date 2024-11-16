using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopManager : MonoBehaviour
{
    public static ShopManager instance;

    private void Awake()
    {
        instance = this;
    }


    [SerializeField]
    private int _coins;
    [SerializeField]
    private TextMeshProUGUI _CoinUI;
    [SerializeField]
    private List<ShopItemSO> _shopItemSO;
    [SerializeField]
    private List<ShopTemplate> _shopPanels;

    [SerializeField]
    private UnityEngine.UI.Button _shopPurchasableButtons;
    [SerializeField]
    private bool _canPurcahse;

    [SerializeField]
    private ShopTemplate _currentShop;

    [SerializeField]
    private Material _currentMaterial;

    [SerializeField]
    private GameObject _player;


    private void Start()
    {
        LoadPanel();
        AddCoins();
        CheckPurchasable(_shopPanels[0]);
    }

    public void AddCoins()
    {
        
        _CoinUI.text = _coins.ToString();
    }

   

    public void LoadPanel()
    {
        for (int i =0; i < _shopItemSO.Count; i++)
        {
            _shopPanels[i]._unlockedSkinImg.sprite = _shopItemSO[i].unlockedSkinImg;           
            _shopPanels[i]._cost = _shopItemSO[i].cost;
            _shopPanels[i]._playerMaterial = _shopItemSO[i]._playerMaterial;
        }
    }

    public void OnSkinItemSelected(ShopTemplate obj)
    {
        obj._selected.gameObject.SetActive(true);

        if(obj == null)
        {
            _shopPanels[0]._selected.gameObject.SetActive(false);
        }

        for (int i = 0; i < _shopPanels.Count; i++)
        {
            if (_shopPanels[i] != obj)
            {
                _shopPanels[i]._selected.gameObject.SetActive(false);
            }
        }

        CheckPurchasable(obj);
    }

    public void CheckPurchasable(ShopTemplate item)
    {
        if(_coins >= item._cost)
        {
            _shopPurchasableButtons.gameObject.GetComponent<Image>().color = Color.green;
            _canPurcahse = true;
        }
        else
        {
            _shopPurchasableButtons.gameObject.GetComponent<Image>().color = Color.grey;
            _canPurcahse = false;
        }

        _currentShop = item;
    }

    public void PurchaseItem()
    {
        var item = _currentShop;

        if(_coins >= item._cost)
        {
            item._lockedSkinImg.SetActive(false);
        }
    }

    public void SetMaetrial()
    {
        _currentMaterial = _currentShop._playerMaterial;

        _player.gameObject.GetComponent<SkinnedMeshRenderer>().material = _currentMaterial;
    }
}
