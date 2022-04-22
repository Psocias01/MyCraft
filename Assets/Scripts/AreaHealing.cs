using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class AreaHealing : MonoBehaviour
{
    public int HealthPoints = 1;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            GameManager.instance.Player.Rehab(HealthPoints);
            GameManager.instance.Player.municionMagiaElectrica = 10;
            GameManager.instance.Player.municionMagiaFuego = 3;
            GameManager.instance.Player.municionMagiaHielo = 3;
        }
    }
}
