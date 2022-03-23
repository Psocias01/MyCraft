using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FPSShooter : MonoBehaviour
{
    public Camera Camera;
    public GameObject projectile;
    public Transform LHFirePoint, RHFirePoint;
    
    private Vector3 destination;
    private bool leftHand;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            ShootProjectile();
        }
    }

    void ShootProjectile()
    {
        Ray ray = Camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            destination = hit.point;
        }
        else
        {
            destination = ray.GetPoint(1000);
        }

        if (leftHand)
        {
            leftHand = false;
            InstantiateProjectile(LHFirePoint);
        }
        else
        {
            leftHand = true;
            InstantiateProjectile(RHFirePoint);
        }
        
    }
    
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
    }

}
