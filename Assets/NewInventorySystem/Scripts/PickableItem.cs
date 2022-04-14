using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class PickableItem : MonoBehaviour
{
    public Item item;
    private bool isPicked = false;
    
    private void OnTriggerEnter(Collider other)
    {
        var player = other.GetComponent<PlayerMovement>();
        
        if (player != null && !isPicked)
        {
            isPicked = true;
            Inventory.instance.AddItem(item);
        }
        Destroy(gameObject);
    }
}
