using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BarcoMoviment : MonoBehaviour
{
    public float speed;
    public float minSpeedValue = 5f;
    public float maxSpeedValue = 20f;
    
    
    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(minSpeedValue, maxSpeedValue);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }
}
