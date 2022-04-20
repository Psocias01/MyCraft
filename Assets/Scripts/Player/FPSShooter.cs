using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class FPSShooter : MonoBehaviour
{
    public Camera Camera;
    private GameObject cameraObject;
    public GameObject projectile;
    public Transform LHFirePoint;
    
    
    
    private Vector3 destination;
    [SerializeField] private bool leftHand;
    public float projectileSpeed = 30;
    public float fireRate = 4;

    private float timeToFire;

    public bool canFire;
    
    void Start()
    {
        cameraObject = GameObject.FindGameObjectWithTag("MainCamera");
        Camera = cameraObject.GetComponent<Camera>();
    }
    
    void Update()
    {
        if (canFire)
        {
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                StartCoroutine(DispararMagia());
            }
        }
    }

    IEnumerator DispararMagia()
    {
        yield return new WaitForSeconds(0.67f);
        
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
    }

    void ShootProjectile()
    {
        
    }
    
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        leftHand = true;
    }

}
