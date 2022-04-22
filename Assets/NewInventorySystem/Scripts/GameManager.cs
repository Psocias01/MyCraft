using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region singleton

    public static GameManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    #endregion
    
    public Slider mouseSensibilitySlder;
    
    public List<Item> itemList = new List<Item>();
    public List<Item> craftingRecipes = new List<Item>();
    public PlayerMovement Player;

    public Transform canvas;
    public GameObject itemInfoPrefab;
    private GameObject currentItemInfo = null;

    public Transform mainCanvas;
    public Transform HotbarTransform;
    public Transform inventoryTransform;
    
    public float moveX = 0f;
    public float moveY = 0f;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Item newItem = itemList[Random.Range(0, itemList.Count)];
            Inventory.instance.AddItem(Instantiate(newItem));
        }
    }

    public void OnStateItemUse(StatItemType itemType, int amount)
    {
        Debug.Log("Consuming " + itemType + " Add amount: " + amount);
        if (itemType == StatItemType.FoodItem)
        {
            
        }
        else if (itemType == StatItemType.HealthItem)
        {
            Player.Rehab(amount);
        }
        else if (itemType == StatItemType.Mana)
        {
            GameManager.instance.Player.municionMagiaElectrica = 10;
            GameManager.instance.Player.municionMagiaFuego = 3;
            GameManager.instance.Player.municionMagiaHielo = 3;
        }
    }

    public void DisplayItemInfo(string itemName, String itemDescription, Vector2 buttonPos)
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }

        buttonPos.x += moveX;
        buttonPos.y += moveY;

        currentItemInfo = Instantiate(itemInfoPrefab, buttonPos, Quaternion.identity, canvas);
        currentItemInfo.GetComponent<ItemInfo>().SetUp(itemName, itemDescription);
    }

    public void DestroyItemInfo()
    {
        if (currentItemInfo != null)
        {
            Destroy(currentItemInfo.gameObject);
        }
    }
}
