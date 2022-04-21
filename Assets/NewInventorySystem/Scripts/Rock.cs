using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Rock : MonoBehaviour
{
    public int dropRatio1 = 100;
    public int dropRatio2 = 50;

    public int hitTimes = 10;

    public int timeBetweenhits = 1;
    
    public Item rock;

    private bool alreadyHitted = false;

    private void OnTriggerEnter(Collider other)
    {
        Pickaxe axe = other.gameObject.GetComponent<Pickaxe>();

        if (axe != null && !alreadyHitted)
        {
            alreadyHitted = true;
            hitTimes -= 3;
            Debug.Log("RockHit");
            Invoke(nameof(ResetChop), timeBetweenhits);
        }
        
        Mano mano = other.gameObject.GetComponent<Mano>();
        
        if (mano != null && !alreadyHitted)
        {
            alreadyHitted = true;
            hitTimes -= 1;
            Debug.Log("RockHit");
            Invoke(nameof(ResetChop), timeBetweenhits);
        }
    }

    public void ResetChop()
    {
        alreadyHitted = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTimes <= 0)
        {
            float dropRate1 = Random.Range(0, 100);
            if (dropRatio1 >= dropRate1)
            {
                Inventory.instance.AddItem(rock);
                Debug.Log("DropeandoPiedra");
            }
            
            float dropRate2 = Random.Range(0, 100);
            if (dropRatio2 >= dropRate2)
            {
                Inventory.instance.AddItem(rock);
                Debug.Log("DropeandoPiedra");
            }
            Destroy(this.gameObject);
        }
        
    }
}
