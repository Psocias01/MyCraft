using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(fileName = "Equipable_Item", menuName = "Item/Equip_Item")]
public class EquipableItem : Item
{
    public GameObject itemToEquip;
    
    public override void Use()
    {
        Debug.Log("Equiping " + name);
        Inventory.instance.PlaceItem(itemToEquip);
    }
}
