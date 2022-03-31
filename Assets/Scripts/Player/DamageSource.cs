using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageSource : MonoBehaviour
{
    #region Private members

    private bool _isCausingDamage = false;

    #endregion
    
    #region Public members

    public float DamageRepeatRate = 0.1f;

    public int DamageAmount = 1;

    public bool Repeating = true;

    #endregion

    private void OnTriggerEnter(Collider other)
    {
        _isCausingDamage = true;

        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();

        if (player != null)
        {
            if (Repeating)
            {
                // Logica de daño repetitivo (fuego, etc).
                StartCoroutine(TakeDamage(player, DamageRepeatRate));
            }
            else
            {
                // Logica de daño una sola vez
                player.TakeDamage(DamageAmount);
            }
        }
    }

    IEnumerator TakeDamage(PlayerMovement player, float RepeatRate)
    {
        while (_isCausingDamage)
        {
            player.TakeDamage(DamageAmount);
            TakeDamage(player, RepeatRate);

            if (player.isDead)
            {
                _isCausingDamage = false;
            }
            yield return new WaitForSeconds(RepeatRate);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerMovement player = other.gameObject.GetComponent<PlayerMovement>();
        if (player != null)
        {
            _isCausingDamage = false;
        }
    }
}
