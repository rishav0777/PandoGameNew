using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "ShopMenu", menuName ="Scriptable Object/New Shop Item", order = 1)]
public class ShopItemSO : ScriptableObject
{   
    public Sprite unlockedSkinImg;
    public Material _playerMaterial;
    public int cost;
}
