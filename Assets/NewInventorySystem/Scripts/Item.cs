using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(fileName = "Item", menuName = "Item/BaseItem")]
public class Item : ScriptableObject
{
    new public string name = "Default Item";
    public Sprite icon = null;
    public String itemDescription = "Used for crafting";

    public virtual void Use()
    {
        Debug.Log("Using " + name);
    }

    public virtual String GetItemDescription()
    {
        return itemDescription;
    }
}
