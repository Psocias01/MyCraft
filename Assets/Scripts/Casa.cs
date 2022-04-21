using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casa : MonoBehaviour
{
    public int dropRatio1 = 100;
    public int dropRatio2 = 50;

    public int hitTimes = 10;

    public int timeBetweenhits = 1;
    
    public Item rock;
    public Item Wood;

    private bool alreadyHitted = false;
    public GameObject casa;

    private void OnTriggerEnter(Collider other)
    {
        Pickaxe pickaxe = other.gameObject.GetComponent<Pickaxe>();

        if (pickaxe != null && !alreadyHitted)
        {
            alreadyHitted = true;
            hitTimes -= 3;
            Debug.Log("CasaHit");
            Invoke(nameof(ResetChop), timeBetweenhits);
        }
        
        Axe axe = other.gameObject.GetComponent<Axe>();

        if (axe != null && !alreadyHitted)
        {
            alreadyHitted = true;
            hitTimes -= 3;
            Debug.Log("CasaHit");
            Invoke(nameof(ResetChop), timeBetweenhits);
        }
        
        Mano mano = other.gameObject.GetComponent<Mano>();
        
        if (mano != null && !alreadyHitted)
        {
            alreadyHitted = true;
            hitTimes -= 1;
            Debug.Log("CasaHit");
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
                Inventory.instance.AddItem(Wood);
                Inventory.instance.AddItem(Wood);
                Debug.Log("DropeandoMadera");
            }
            
            float dropRate2 = Random.Range(0, 100);
            if (dropRatio2 >= dropRate2)
            {
                Inventory.instance.AddItem(rock);
                Inventory.instance.AddItem(rock);
                Debug.Log("DropeandoPiedra");
            }
            Destroy(casa.gameObject);
        }
        
    }
}
