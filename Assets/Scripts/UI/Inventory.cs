using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Inventory : MonoBehaviour
{
   private const int SLOTS = 9;

   public  List<IInventoryItem> MItems = new List<IInventoryItem>();

   public event EventHandler<InventoryEventArgs> ItemAdded;
   public event EventHandler<InventoryEventArgs> ItemRemoved;
   public event EventHandler<InventoryEventArgs> ItemUsed;
   
   public Inventory()
   {
      for (int i = 0; i < SLOTS; i++)
      {
         //MItems.Add(new InventorySlot(i));
         //mSlots.Add(gameObject.AddComponent<InventorySlot>());
         
      }
   }
   
   // Logica aplicada al quitar un objeto del inventario(de momento colisionanado).
   public void AddItem(IInventoryItem item)
   {
      if (MItems.Count < SLOTS)
      {
         Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
         if (collider.enabled)
         {
            collider.enabled = false;
            
            MItems.Add(item);
            
            item.OnPickup();

            if (ItemAdded != null)
            {
               ItemAdded(this, new InventoryEventArgs(item));
            }
         }
      }
   }
   
   
   public void UseItem(IInventoryItem item)
   {
      if (ItemUsed != null)
      {
         ItemUsed(this, new InventoryEventArgs(item));
      }
   }
   
   // Logica aplicada al quitar un objeto del inventario(de momento arrastrando).
   public void RemoveItem(IInventoryItem item)
   {
      if (MItems.Contains(item))
      {
         MItems.Remove(item);
         
         item.OnDrop();
         
         Collider collider = (item as MonoBehaviour).GetComponent<Collider>();
         if (collider != null)
         {
            collider.enabled = true;
         }
         
         if (ItemRemoved != null)
         {
            ItemRemoved(this, new InventoryEventArgs(item));
         }
      }
   }
}
