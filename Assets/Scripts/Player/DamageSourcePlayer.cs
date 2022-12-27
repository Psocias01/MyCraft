using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSourcePlayer : MonoBehaviour
{
    public int DammagePerHit;
    private void OnTriggerEnter(Collider other)
    {
        AI_Enemy enemy = other.gameObject.GetComponent<AI_Enemy>();

        IA_GoblinVaca goblinVaca = other.gameObject.GetComponent<IA_GoblinVaca>();
        
        AI_Golem Golem = other.gameObject.GetComponent<AI_Golem>();
        
        AI_BossGoblin goblinboss = other.gameObject.GetComponent<AI_BossGoblin>();

        if (enemy != null)
        {
            Debug.Log("DAÑANDO AL ENEMY");
            enemy.TakeDamage(DammagePerHit);
        }

        if (goblinVaca != null)
        {
            Debug.Log("Dañando al animal");
            goblinVaca.TakeDamage(DammagePerHit);
        }
        
        if (Golem != null)
        {
            Debug.Log("Dañando al animal");
            Golem.TakeDamage(DammagePerHit);
        }
        
        if (goblinboss != null)
        {
            Debug.Log("Dañando al animal");
            goblinboss.TakeDamage(DammagePerHit);
        }
        
        
    }
}
