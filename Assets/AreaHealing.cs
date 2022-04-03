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
            GameManager.Instance.Player.Rehab(HealthPoints);
        }
    }
}
