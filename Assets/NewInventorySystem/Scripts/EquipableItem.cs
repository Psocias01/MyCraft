using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(fileName = "Equipable_Item", menuName = "Item/Equip_Item")]
public class EquipableItem : Item
{
    public GameObject itemToEquip;

    public bool Hacha;
    public bool Espada;
    public bool Pico;
    public bool Baston;
    public bool Farolillo;
    
    public override void Use()
    {
        Debug.Log("Equiping " + name);
        if (!Farolillo)
        {
            Inventory.instance.PlaceItem(itemToEquip);
        }
        else
        {
            Inventory.instance.PlaceFarol(itemToEquip);
        }
        
        
        if (Hacha)
        {
            GameManager.instance.Player.Hacha = true;
            GameManager.instance.Player.Espada = false;
            GameManager.instance.Player.Pico = false;
            GameManager.instance.Player.Baston = false;
            GameManager.instance.Player.Farolillo = false;
            GameManager.instance.Player.Mano = false;
        }
        if (Espada)
        {
            GameManager.instance.Player.Espada = true;
            GameManager.instance.Player.Hacha = false;
            GameManager.instance.Player.Pico = false;
            GameManager.instance.Player.Baston = false;
            GameManager.instance.Player.Farolillo = false;
            GameManager.instance.Player.Mano = false;
        }
        if (Pico)
        {
            GameManager.instance.Player.Pico = true;
            GameManager.instance.Player.Hacha = false;
            GameManager.instance.Player.Espada = false;
            GameManager.instance.Player.Baston = false;
            GameManager.instance.Player.Farolillo = false;
            GameManager.instance.Player.Mano = false;
        }
        if (Baston)
        {
            GameManager.instance.Player.Baston = true;
            GameManager.instance.Player.Hacha = false;
            GameManager.instance.Player.Espada = false;
            GameManager.instance.Player.Pico = false;
            GameManager.instance.Player.Farolillo = false;
            GameManager.instance.Player.Mano = false;
        }
        if (Farolillo)
        {
            GameManager.instance.Player.Farolillo = true;
            GameManager.instance.Player.Hacha = false;
            GameManager.instance.Player.Espada = false;
            GameManager.instance.Player.Pico = false;
            GameManager.instance.Player.Baston = false;
            GameManager.instance.Player.Mano = false;
        }
    }
}
