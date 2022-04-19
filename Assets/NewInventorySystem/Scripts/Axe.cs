using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Axe : MonoBehaviour
{
    private BoxCollider collider;

    private void Start()
    {
        collider = GetComponent<BoxCollider>();
    }

    public void ActivarColliderHacha()
    {
        collider.enabled = true;
    }
    
    public void DesctivarColliderHacha()
    {
        collider.enabled = false;
    }
}
