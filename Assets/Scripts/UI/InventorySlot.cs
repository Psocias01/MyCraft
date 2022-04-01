using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class InventorySlot : MonoBehaviour
{
   public Stack<IInventoryItem> mItemStack = new Stack<IInventoryItem>();

   private int mId = 0;

   public InventorySlot(int id)
   {
      mId = id;
   }

   public int Id
   {
      get { return mId; }
   }

   public void AddItem(IInventoryItem item)
   {
      item.Slot = this;
      mItemStack.Push(item);
      Debug.Log("PUSH item stack");
      
   }

   public IInventoryItem FirstItem
   {
      get
      {
         if (isEmpty)
            return null;

         return mItemStack.Peek();
      }
   }

   public bool isStackable (IInventoryItem item)
   {
      if (isEmpty)
      {
         return false;
      }
      
      
      IInventoryItem first = mItemStack.Peek();

      Debug.Log("FIRTS_name" + first.Name);
      Debug.Log("Item_name" + item.Name);
      if (first.Name == item.Name)
      {
         Debug.Log("HE ENTRADO TRUE");
         return true;
      }
      return false;
   }

   public bool isEmpty
   {
      get { return Count == 0; }
   }

   public int Count
   {
      get { return mItemStack.Count; }
   }

   public bool Remove(IInventoryItem item)
   {
      if (isEmpty)
      {
         return false;
      }

      IInventoryItem first = mItemStack.Peek();
      if (first.Name == item.Name)
      {
         mItemStack.Pop();
         return true;
      }
      return false;
   }
}
