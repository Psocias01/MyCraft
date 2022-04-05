using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ActivadorDeNota : MonoBehaviour
{
    public GameObject notaVisual;
    public bool activa;
    
    public GameObject textLetra;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && activa == true)
        {
            notaVisual.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.Escape) && activa == true)
        {
            notaVisual.SetActive(false);
            
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            activa = true;
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            activa = false;
            notaVisual.SetActive(false);
        }
    }
    
    public void OpenMessagePanel(string text)
    {
        textLetra.SetActive(true);
    }

    public void CloseMessagePanel()
    {
        textLetra.SetActive(false);
    }
}
