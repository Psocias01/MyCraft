using System;
using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using TreeEditor;
using UnityEngine;
using UnityEngine.EventSystems;
using Image = UnityEngine.UI.Image;

public class ItemDrag : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    private ItemSlot _itemSlot;
    private RectTransform hotbarRect;
    private RectTransform inventoryRect;
    
    public GameObject previewPrefab;
    private GameObject currentPreview;
    private Image image;
    private Color baseColor;
    private bool isHotbarSlot;
    
    private void Start()
    {
        _itemSlot = GetComponent<ItemSlot>();
        hotbarRect = GameManager.instance.HotbarTransform as RectTransform;
        inventoryRect = GameManager.instance.inventoryTransform as RectTransform;

        image = GetComponent<Image>();
        baseColor = image.color;

        isHotbarSlot = RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, transform.position);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _itemSlot.OnCursorExit();
        _itemSlot.isBeingDraged = true;
        
        // Cambiamos el valor del Alpha
        var tmpColor = baseColor;
        tmpColor.a = 0.6f;
        image.color = tmpColor;

        currentPreview = Instantiate(previewPrefab, GameManager.instance.mainCanvas);
        currentPreview.GetComponent<Image>().sprite = _itemSlot.Item.icon;
        currentPreview.transform.position = transform.position;
    }
    
    public void OnDrag(PointerEventData eventData)
    {
        currentPreview.transform.position = Input.mousePosition;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        _itemSlot.isBeingDraged = false;
        image.color = baseColor;

        if ((RectTransformUtility.RectangleContainsScreenPoint(hotbarRect, Input.mousePosition) && !isHotbarSlot) 
            || (RectTransformUtility.RectangleContainsScreenPoint(inventoryRect, Input.mousePosition) && isHotbarSlot))
        {
            Inventory.instance.SwitchHotbarInventory(_itemSlot.Item);
        }
        
        Destroy(currentPreview);
    }
}
