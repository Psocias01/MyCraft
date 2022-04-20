using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{

    private bool inventoryOpen = false;

    private bool optionsPanelOpen = false;

    public bool InventoryOpen => inventoryOpen;
    public GameObject inventoryParent;
    public GameObject inventoryTab;
    public GameObject craftingTab;
    public GameObject messagePanel;

    public GameObject optionsPanel;

    private List<ItemSlot> itemSlotList = new List<ItemSlot>();
    
    public GameObject inventorySlotPrefab;
    public GameObject craftingSlotPrefab;
    
    public Transform inventoryItemTransform;
    public Transform craftingItemTransform;

    private void Start()
    {
        Inventory.instance.onItemChange += UpdateInventoryUI;
        UpdateInventoryUI();
        SetUpCraftingRecipes();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (InventoryOpen)
            {
                // Close
                CloseInventory();
            }
            else
            {
                // Open
                OpenInventory();
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (optionsPanelOpen)
            {
                // Close
                CloseOptions();
            }
            else
            {
                // Open
                OpenOptions();
            }
        }
    }

    private void SetUpCraftingRecipes()
    {
        List<Item> craftingRecipes = GameManager.instance.craftingRecipes;

        foreach (Item recipie in craftingRecipes)
        {
            GameObject GO = Instantiate(craftingSlotPrefab, craftingItemTransform);
            ItemSlot slot = GO.GetComponent<ItemSlot>();
            slot.AddItem(recipie);
        }
    }

    private void UpdateInventoryUI()
    {
        int currentItemCount = Inventory.instance.inventoryItemList.Count;

        if(currentItemCount > itemSlotList.Count)
        {
            //Add more item slots
            AddItemSlots(currentItemCount);
        }

        for(int i = 0; i < itemSlotList.Count; ++i)
        {
            if(i < currentItemCount)
            {
                //update the current item in the slot
                itemSlotList[i].AddItem(Inventory.instance.inventoryItemList[i]);
            }
            else
            {
                itemSlotList[i].DestroySlot();
                itemSlotList.RemoveAt(i);
            }
        }
    }

    private void AddItemSlots(int currentItemCount)
    {
        int amount = currentItemCount - itemSlotList.Count;

        for (int i = 0; i < amount; ++i)
        {
            GameObject GO = Instantiate(inventorySlotPrefab, inventoryItemTransform);
            ItemSlot newSlot = GO.GetComponent<ItemSlot>();
            itemSlotList.Add(newSlot);
        }
    }

    private void OpenInventory()
    {
        ChangeCursorState(false);
        inventoryOpen = true;
        inventoryParent.SetActive(true);
        OnInventoryTabClicked();
    }
    
    private void OpenOptions()
    {
        ChangeCursorState(false);
        optionsPanelOpen = true;
        optionsPanel.SetActive(true);
    }
    
    private void CloseInventory()
    {
        ChangeCursorState(true);
        inventoryOpen = false;
        inventoryParent.SetActive(false);
    }
    
    private void CloseOptions()
    {
        ChangeCursorState(true);
        optionsPanelOpen = false;
        optionsPanel.SetActive(false);
    }

    public void OnCraftingTabClicked()
    {
        craftingTab.SetActive(true);
        inventoryTab.SetActive(false);
    }
    
    public void OnInventoryTabClicked()
    {
        craftingTab.SetActive(false);
        inventoryTab.SetActive(true);
    }

    private void ChangeCursorState(bool lockCursor)
    {
        if (lockCursor)
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    
    public void OpenMessagePanel(string text)
    {
        messagePanel.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        messagePanel.SetActive(false);
    }
}
