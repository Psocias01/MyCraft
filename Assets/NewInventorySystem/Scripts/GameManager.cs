using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager GM;

    private void Awake()
    {
        if (GM == null)
        {
            GM = this;
        }
    }

    public List<Item> itemList = new List<Item>();
    public PlayerMovement Player;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            Inventory.instance.AddItem(itemList[Random.Range(0, itemList.Count)]);
        }
    }
}
