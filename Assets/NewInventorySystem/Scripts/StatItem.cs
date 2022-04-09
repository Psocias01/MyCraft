using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(fileName = "Stat_Item", menuName = "Item/Stat_Item")]
public class StatItem : Item
{
    public StatItemType itemType;
    public int amount;
    public override void Use()
    {
        base.Use();
        // Añadimos algo
        GameManager.instance.OnStateItemUse(itemType, amount);
        Inventory.instance.RemoveItem(this);
    }
}

public enum StatItemType
{
    HealthItem,
    FoodItem,
}
