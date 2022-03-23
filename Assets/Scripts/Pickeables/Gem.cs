using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gem : InventoryItemBase
{
    public override string Name
    {
        get
        {
            return "Gem";
        }
    }
    public override void OnUse()
    {
        // TO DO: Que haga algo el objeto.
        base.OnUse();
    }
}
