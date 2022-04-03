using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Axe : InventoryItemBase
{
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
}


