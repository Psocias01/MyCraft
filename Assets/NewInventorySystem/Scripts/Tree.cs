using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using Random = UnityEngine.Random;

public class Tree : MonoBehaviour
{
    public int dropRatio1 = 50;
    public int dropRatio2 = 50;

    public int ChopTimes = 5;

    public int timeBetweenChops = 1;

    public GameObject itemToDrop1;
    public GameObject itemToDrop2;
    
    public Transform itemPlacer;
    public Transform itemPlacer2;

    private bool alreadyChoped = false;

    private void OnTriggerEnter(Collider other)
    {
        Axe axe = other.gameObject.GetComponent<Axe>();

        if (axe != null && !alreadyChoped)
        {
            alreadyChoped = true;
            ChopTimes -= 3;
            Debug.Log("Chop");
            Invoke(nameof(ResetChop), timeBetweenChops);
        }
        
        Mano mano = other.gameObject.GetComponent<Mano>();
        
        if (mano != null && !alreadyChoped)
        {
            alreadyChoped = true;
            ChopTimes -= 1;
            Debug.Log("Chop");
            Invoke(nameof(ResetChop), timeBetweenChops);
        }
    }

    public void ResetChop()
    {
        alreadyChoped = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (ChopTimes <= 0)
        {
            float dropRate1 = Random.Range(0, 100);
            if (dropRatio1 >= dropRate1)
            {
                Instantiate(itemToDrop1, itemPlacer.transform.position, itemPlacer.transform.rotation, itemPlacer); 
                Debug.Log("DropeandoMadera");
            }
            
            float dropRate2 = Random.Range(0, 100);
            if (dropRatio2 >= dropRate2)
            {
                Instantiate(itemToDrop2, itemPlacer2.transform.position, itemPlacer2.transform.rotation, itemPlacer2);
                Debug.Log("DropeandoManzana");
            }
            Destroy(this.gameObject);
        }
        
    }
}
