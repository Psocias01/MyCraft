using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.XR;

public class Inventory : MonoBehaviour
{
    #region singleton

    public static Inventory instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion

    public delegate void OnItemChange();
    public OnItemChange onItemChange = delegate {};

    public List<Item> inventoryItemList = new List<Item>();
    
    public List<Item> hotbarItemList = new List<Item>();
    public HotbarController hotbarController;
    
    public Transform itemPlacerTransform;
    
    public BoxCollider HandCollider;

    public void UnequipItem()
    {
        GameManager.instance.Player._anim.SetBool("Item", false);
        if (itemPlacerTransform.childCount > 0)
        {
            Debug.Log("Desequipando Objeto");
            Destroy(itemPlacerTransform.GetChild(0).gameObject);
            HandCollider.enabled = true;
        }
        else
        {
            Debug.Log("No hay objeto equipado");
            return;
        }
    }
    
    public void PlaceItem(GameObject itemToEquip)
    {
        if (itemPlacerTransform.childCount > 0)
        {
            Debug.Log("Hay un objeto equipado");
            Destroy(itemPlacerTransform.GetChild(0).gameObject);
        }
        else if (itemPlacerTransform.childCount == 0)
        {
            Debug.Log("NO Hay un objeto equipado");
        }
        GameManager.instance.Player._anim.SetBool("Item", true);
        GameManager.instance.Player._anim.SetBool("Farol", false);
        Instantiate(itemToEquip, itemPlacerTransform.transform.position, itemPlacerTransform.transform.rotation, itemPlacerTransform);
        HandCollider.enabled = false;
    }
    
    public void PlaceFarol(GameObject itemToEquip)
    {
        if (itemPlacerTransform.childCount > 0)
        {
            Debug.Log("Hay un objeto equipado");
            Destroy(itemPlacerTransform.GetChild(0).gameObject);
        }
        else if (itemPlacerTransform.childCount == 0)
        {
            Debug.Log("NO Hay un objeto equipado");
        }
        GameManager.instance.Player._anim.SetBool("Item", false);
        GameManager.instance.Player._anim.SetBool("Farol", true);
        Instantiate(itemToEquip, itemPlacerTransform.transform.position, itemPlacerTransform.transform.rotation, itemPlacerTransform);
        HandCollider.enabled = false;
    }
    
    public void SwitchHotbarInventory(Item item)
    {
        foreach (Item i in inventoryItemList)
        {
            if (i == item)
            {
                if (hotbarItemList.Count >= hotbarController.HotbarSlotSize)
                {
                    Debug.Log("No more slots available in hotbar");
                }
                else
                {
                    hotbarItemList.Add(item);
                    inventoryItemList.Remove(item);
                    onItemChange.Invoke();
                }
                
                return;
            }
        }
        
        // Devolver los items al inventory
        foreach (Item i in hotbarItemList)
        {
            if (i == item)
            {
                hotbarItemList.Remove(item);
                inventoryItemList.Add(item);
                onItemChange.Invoke();
                
                return;
            }
        }
    }
    public void AddItem(Item item)
    {
        inventoryItemList.Add(item);
        onItemChange.Invoke();
        //Debug.Log("Item Added");

    }

    public void RemoveItem(Item item)
    {
        if (inventoryItemList.Contains(item))
        {
            inventoryItemList.Remove(item);
        }
        else if (hotbarItemList.Contains(item))
        {
            hotbarItemList.Remove(item);
        }
        onItemChange.Invoke();
        //Debug.Log("Item Removed");
    }

    public bool ContainsItem(string itemName, int amount)
    {
        int itemCounter = 0;

        foreach (Item i in inventoryItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }
        
        foreach (Item i in hotbarItemList)
        {
            if (i.name == itemName)
            {
                itemCounter++;
            }
        }

        if (itemCounter >= amount)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public void RemoveItems(String itemName, int amount)
    {
        for (int i = 0; i < amount; i++)
        {
            RemoveItemType(itemName);
        }
    }

    public void RemoveItemType(string itemName)
    {
        foreach (Item i in inventoryItemList)
        {
            if (i.name == itemName)
            {
                inventoryItemList.Remove(i);
                return;
            }
        }
        
        foreach (Item i in hotbarItemList)
        {
            if (i.name == itemName)
            {
                hotbarItemList.Remove(i);
                return;
            }
        }
    }
}
