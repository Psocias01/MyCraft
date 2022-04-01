using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryItemBase : MonoBehaviour, IInventoryItem
{
    public virtual string Name
    {
        get
        {
            return "_base item_";
        }
        
    }

    public Sprite _Image;
    public Sprite Image
    {
        get
        {
            return _Image;
        }
    }

    public virtual void OnUse()
    {
        // Movemos el item al hacer click al Public Vector3 asginado (mano).
        transform.localPosition = PickPosition;
        transform.localEulerAngles = PickRotation;
    }
    
    public virtual void OnPickup()
    {
        gameObject.SetActive(false);
    }

    public virtual void OnDrop()
    {
        // TO DO: Añadir lógica para que la hacha vaya a la mano
        RaycastHit hit = new RaycastHit();
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit, 1000))
        {
            gameObject.SetActive(true);
            gameObject.transform.position = hit.point;
            gameObject.transform.eulerAngles = DropRotation;
        }
    }

    public Vector3 PickPosition;
    
    public Vector3 PickRotation;

    public Vector3 DropRotation;
}
