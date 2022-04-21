using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    private bool collided;
    public GameObject poderPrefab;
    private void OnCollisionEnter(Collision co)
    {
        if (co.gameObject.tag != "Magia" && co.gameObject.tag != "Player" && !collided)
        {
            collided = true;

            var impact = Instantiate(poderPrefab, co.contacts[0].point, Quaternion.identity) as GameObject;
            impact.GetComponent<Transform>().rotation = GameManager.instance.Player.transform.rotation;
            
            Destroy(impact, 6);
            
            Destroy(gameObject);
        }
    }
}
