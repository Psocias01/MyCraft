using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ciclo_Dia_Noche : MonoBehaviour
{
    public float rotationScale = 10f;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(rotationScale * Time.deltaTime, 0, 0);
    }
}
