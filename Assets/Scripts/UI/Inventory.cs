using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEditor;

public class Inventory : MonoBehaviour
{
   private const int SLOTS = 9;

   public  IList<InventorySlot> mSlots = new List<InventorySlot>();

   public event EventHandler<InventoryEventArgs> ItemAdded;
   public event EventHandler<InventoryEventArgs> ItemRemoved;
   public event EventHandler<InventoryEventArgs> ItemUsed;
   
   public Inventory()
   {
      for (int i = 0; i < SLOTS; i++)
      {
         mSlots.Add(new InventorySlot(i));
         //mSlots.Add(gameObject.AddComponent<InventorySlot>());
         
      }
   }

   private InventorySlot FindStackableSlot(IInventoryItem item)
   {
      foreach (InventorySlot slot in mSlots)
      {
         if (slot.isStackable(item))
         {
            return slot;
         }
      }
      return null;
   }

   private InventorySlot FindNextEmptySlot()
   {
      foreach (InventorySlot slot in mSlots)
      {
         if (slot.isEmpty)
         {
            Debug.Log("Devolvemos slot");
            return slot;
         }
      }
      return null;
   }


   // Logica aplicada al quitar un objeto del inventario(de momento colisionanado).
   public void AddItem(IInventoryItem item)
   {
      Debug.Log("Inicio ADDITEM");
      InventorySlot freeSlot = FindStackableSlot(item);
      
      
      if (freeSlot == null)
      {
         freeSlot = FindNextEmptySlot();
      }
      if (freeSlot.isEmpty)
      {
         
         freeSlot.isStackable(item);
         freeSlot.AddItem(item);

         if (ItemAdded != null)
         {
            ItemAdded(this, new InventoryEventArgs(item));
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
      foreach (InventorySlot slot in mSlots)
      {
         if (slot.Remove(item))
         {
            if (ItemRemoved != null)
            {
               ItemRemoved(this, new InventoryEventArgs(item));
            }
            break;
         }
      }
   }
}
