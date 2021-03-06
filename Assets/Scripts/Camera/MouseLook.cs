using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseLook : MonoBehaviour
{
    public float mouseSensitivity;

    public Transform playerBody;

    private float xRotation = 0f;

    private bool cursorLocked = true;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {
        if (cursorLocked)
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -90f, 90f);
            transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
            playerBody.Rotate(Vector3.up * mouseX);
        }
        
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            cursorLocked = !cursorLocked;
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                
            }
        }
        
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            cursorLocked = !cursorLocked;
            if (cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Locked;
                Cursor.visible = false;
            }
            else if (!cursorLocked)
            {
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.visible = true;
                
            }
        }
    }

    public void MouseSensibilityOptions()
    {
        mouseSensitivity = GameManager.instance.mouseSensibilitySlder.value;
    }
}
