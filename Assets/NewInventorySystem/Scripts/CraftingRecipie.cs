using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
[CreateAssetMenuAttribute(fileName = "Item", menuName = "CraftingRecipie/baseRecipie")]
public class CraftingRecipie : Item
{
    public Item result;
    public Ingredient[] ingredients;

    private bool CanCraft()
    {
        // Le preguntamos al script de Inventory si tenemos los recursos necesarios
        foreach (Ingredient ingredient in ingredients)
        {
            bool containsCurrentIngredient = Inventory.instance.ContainsItem(ingredient.item, ingredient.amount);

            if (!containsCurrentIngredient)
            {
                return false;
            }
        }

        return true;
    }

    private void RemoveIngredientsFromInventory()
    {
        foreach (Ingredient ingredient in ingredients)
        {
            Inventory.instance.RemoveItems(ingredient.item, ingredient.amount);
        }
    }

    public override void Use()
    {
        if (CanCraft())
        {
            //Removemos los objetos
            RemoveIngredientsFromInventory();

            //Añadimos el objeto crafteado al inventario
            Inventory.instance.AddItem(result);
            Debug.Log(result.name + " creado");
        }
        else
        {
            Debug.Log("No tienes los objetos necesarios para crear: " + result.name);
        }
    }

    public override string GetItemDescription()
    {
        string itemIngredients = "";

        foreach (Ingredient ingredient in ingredients)
        {
            itemIngredients += "- " + ingredient.amount + " " + ingredient.item.name + "\n";
        }

        return itemIngredients;

    }
    
    [System.Serializable]
    public class Ingredient
    {
        public Item item;
        public int amount;
    }
}
