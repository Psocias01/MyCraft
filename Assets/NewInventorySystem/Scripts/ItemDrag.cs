using System;
using System.Collections;
using System.Collections.Generic;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private ItemSlot _itemSlot;
    private Transform baseParent;
    private RectTransform hotbarRect;
    private int siblingIndex;
    private void Start()
    {
        _itemSlot = GetComponent<ItemSlot>();
        baseParent = transform.parent;
        hotbarRect = GameManager.instance.HotbarTransform as RectTransform;
        siblingIndex = transform.GetSiblingIndex();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        transform.SetParent(GameManager.instance.mainCanvas);
        _itemSlot.OnCursorExit();
        _itemSlot.isBeingDraged = true;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(baseParent);
        transform.SetSiblingIndex(siblingIndex);
        _itemSlot.isBeingDraged = false;

        if (RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, Input.mousePosition))
        {
            Inventory.instance.SwitchHotbarInventory(_itemSlot.Item);
        }
    }
}
