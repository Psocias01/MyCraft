using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSourcePlayer : MonoBehaviour
{
    public int DammagePerHit;
    private void OnTriggerEnter(Collider other)
    {
        AI_Enemy enemy = other.gameObject.GetComponent<AI_Enemy>();

        if (enemy != null)
        {
            Debug.Log("DAÑANDO AL ENEMY");
            enemy.TakeDamage(DammagePerHit);
        }
    }
}
