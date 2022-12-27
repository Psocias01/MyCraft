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

    public GameObject materialOn;
    public GameObject materialOff;
    
    
    
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
        CheckBullets();
    }
    
    void Update()
    {
        if (canFire)
        {
            if (Input.GetButton("Fire1") && Time.time >= timeToFire)
            {
                timeToFire = Time.time + 1 / fireRate;
                StartCoroutine(DispararMagia());
                AudioManager.audioManager.Play("Magia1");
            }
        }
    }

    public void CheckBullets()
    {
        
        if (this.tag == "BastonElectrico")
        {
            if (GameManager.instance.Player.municionMagiaElectrica <=0)
            {
                GameManager.instance.Player.municionMagiaElectrica = 0;
                materialOff.SetActive(true);
                materialOn.SetActive(false);
            }
            else
            {
                materialOff.SetActive(false);
                materialOn.SetActive(true);
            }
        }
        if (this.tag == "BastonFuego")
        {
            if (GameManager.instance.Player.municionMagiaFuego <=0)
            {
                GameManager.instance.Player.municionMagiaFuego = 0;
                materialOff.SetActive(true);
                materialOn.SetActive(false);
            }
            else
            {
                materialOff.SetActive(false);
                materialOn.SetActive(true);
            }
        }
        if (this.tag == "BastonHielo")
        {
            if (GameManager.instance.Player.municionMagiaHielo <=0)
            {
                GameManager.instance.Player.municionMagiaHielo = 0;
                materialOff.SetActive(true);
                materialOn.SetActive(false);
            }
            else
            {
                materialOff.SetActive(false);
                materialOn.SetActive(true);
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
            if (this.tag == "BastonElectrico")
            {
                if (GameManager.instance.Player.municionMagiaElectrica <=0)
                {
                    GameManager.instance.Player.municionMagiaElectrica = 0;
                }
                else
                {
                    GameManager.instance.Player.municionMagiaElectrica -= 1;
                    InstantiateProjectile(LHFirePoint);
                }
            }
            if (this.tag == "BastonFuego")
            {
                if (GameManager.instance.Player.municionMagiaFuego <=0)
                {
                    GameManager.instance.Player.municionMagiaFuego = 0;
                }
                else
                {
                    GameManager.instance.Player.municionMagiaFuego -= 1;
                    InstantiateProjectile(LHFirePoint);
                }
            }
            if (this.tag == "BastonHielo")
            {
                if (GameManager.instance.Player.municionMagiaHielo <=0)
                {
                    GameManager.instance.Player.municionMagiaHielo = 0;
                }
                else
                {
                    GameManager.instance.Player.municionMagiaHielo -= 1;
                    InstantiateProjectile(LHFirePoint);
                }
            }

        }

        CheckBullets();
    }

    void ShootProjectile()
    {
        
    }
    
    void InstantiateProjectile(Transform firePoint)
    {
        var projectileObj = Instantiate(projectile, firePoint.position, Quaternion.identity) as GameObject;
        
        projectileObj.GetComponent<Rigidbody>().velocity = (destination - firePoint.position).normalized * projectileSpeed;
        projectileObj.GetComponent<Transform>().rotation = GameManager.instance.Player.transform.rotation;
        leftHand = true;
    }

}
