using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HotbarController : MonoBehaviour
{

    public int HotbarSlotSize => gameObject.transform.childCount;
    private List<ItemSlot> hotbarSlots = new List<ItemSlot>();

    private KeyCode[] hotbarKeys = new KeyCode[]
    {
        KeyCode.Alpha1, KeyCode.Alpha2, KeyCode.Alpha3, KeyCode.Alpha4, KeyCode.Alpha5, KeyCode.Alpha6, KeyCode.Alpha7,
        KeyCode.Alpha8, KeyCode.Alpha9
    };

    private void Start()
    {
        SetUpHotbarSlots();
        Inventory.instance.onItemChange += UpdateHotbarUI;
    }

    private void Update()
    {
        for (int i = 0; i < hotbarKeys.Length; i++)
        {
            if (Input.GetKeyDown(hotbarKeys[i]))
            {
                Debug.Log("Use item: " + i);
                // Use item
                hotbarSlots[i].UseItem();
                
                return;
            }
        }
    }

    private void UpdateHotbarUI()
    {
        int currentUsedSlotCount = Inventory.instance.hotbarItemList.Count;
        for (int i = 0; i < HotbarSlotSize; i++)
        {
            if (i < currentUsedSlotCount)
            {
                hotbarSlots[i].AddItem(Inventory.instance.hotbarItemList[i]);
            }
            else
            {
                hotbarSlots[i].ClearSlot();
            }
        }
    }

    private void SetUpHotbarSlots()
    {
        for (int i = 0; i < HotbarSlotSize; i++)
        {
            ItemSlot slot = gameObject.transform.GetChild(i).GetComponent<ItemSlot>();
            hotbarSlots.Add(slot);
        }
    }
}
