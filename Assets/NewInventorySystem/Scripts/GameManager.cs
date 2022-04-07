using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    #region singleton

    public static GameManager GM;

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
    }

    #endregion
    

    public List<Item> itemList = new List<Item>();
    public List<Item> craftingRecipes = new List<Item>();
    public PlayerMovement Player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
        }
    }

    public void OnStateItemUse(StatItemType itemType, int amount)
    {
        Debug.Log("Consuming " + itemType + " Add amount: " + amount);
    }
}
