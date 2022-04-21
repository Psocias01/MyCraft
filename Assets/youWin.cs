using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class youWin : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        PlayerMovement playerMovement = other.GetComponent<PlayerMovement>();

        if (playerMovement != null)
        {
            SceneManager.LoadScene("MainMenu");
        }
    }
}