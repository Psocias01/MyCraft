using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : InventoryItemBase
{

    public int DammagePerHit;
    public override string Name
    {
        get
        {
            return "Axe";
        }
    }

    public override void OnUse()
    {
        // TO DO: Que haga algo el objeto.
        base.OnUse();
    }
    
    private void OnTriggerEnter(Collider other)
    {
        AI_Enemy enemy = other.gameObject.GetComponent<AI_Enemy>();

        if (enemy != null)
        {
            enemy.TakeDamage(DammagePerHit);
        }
    }
}


