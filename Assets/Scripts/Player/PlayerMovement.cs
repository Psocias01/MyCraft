using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;
using UnityEngine.EventSystems;

public class PlayerMovement : MonoBehaviour
{
    #region Public Members

    public CharacterController Controller;
    
    public Inventory Inventory;

    public HUD HUD;
    
    public GameObject Hand;

    public Animator _anim;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    #endregion
   

    #region Private Members
    
    private IInventoryItem mCurrentItem = null;
    
    private Vector3 velocity;
    private bool isGrounded;

    private HealthBar mHealthBar;
    
    #endregion
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        Inventory.ItemUsed += Inventory_ItemUsed;
        Inventory.ItemRemoved += Inventory_ItemRemoved;

        mHealthBar = HUD.transform.Find("HealthBar").GetComponent<HealthBar>();
        mHealthBar.Min = 0;
        mHealthBar.Max = Health;
        TakeDamage(55);

    }

    #region Health

    public int Health = 100;

    public bool isDead
    {
        get
        {
            return Health == 0;
        }
    }
    
    public void TakeDamage(int amount)
    {
        Health -= amount;
        if (Health <0)
        {
            Health = 0;
        }
        mHealthBar.SetHealth(Health);

        if (isDead)
        {
            // TO DO: _animator.SetTrigger("death") Activar animación de muerte.
            
        }
    }
    
    public void Rehab(int healthPoints)
    {
        Health += healthPoints;
        
        if (Health > 100)
        {
            Health = 100;
        }
        
        mHealthBar.SetHealth(Health);

    }

    #endregion
    
    private void Inventory_ItemRemoved(object sender, InventoryEventArgs e)
    {
        IInventoryItem item = e.Item;
        
        GameObject goItem = (item as MonoBehaviour).gameObject;
        goItem.SetActive(true);

        // Desparentamos el objeto del player al quitarlo del inventario.
        goItem.transform.parent = null;
    }

    private void SetItemActive(IInventoryItem item, bool active)
    {
        GameObject currentItem = (item as MonoBehaviour).gameObject;
        currentItem.SetActive(active);
        currentItem.transform.parent = active ? Hand.transform : null;
    }
    
    private void Inventory_ItemUsed(object sender, InventoryEventArgs e)
    {
        if (mCurrentItem != null)
        {
            SetItemActive(mCurrentItem, false);
        }
        IInventoryItem item = e.Item;
        
        // Hacer algo con el Objeto(ponerlo en la mano).
        SetItemActive(item, true);
        
        mCurrentItem = e.Item;
    }
    
    private void FixedUpdate()
    {
        if (!isDead)
        {
            // Metodo para dropear un item que tienes en la mano pulsando R Y que el currentItem NO sea null
            if (mCurrentItem != null && Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("DROPEAMOS CON LA R");
                DropCurrentItem();
            }
            else if (mCurrentItem == null && Input.GetKeyDown(KeyCode.R))
            {
                Debug.Log("NO PUEDES DROPEAR");
            }
        }
    }

    private bool mLockPickup = false;
    private void DropCurrentItem()
    {
        mLockPickup = true;
        
        GameObject goItem = (mCurrentItem as MonoBehaviour).gameObject;
                
        Inventory.RemoveItem(mCurrentItem);

        Rigidbody rbItem = goItem.AddComponent<Rigidbody>();
        rbItem.AddForce(transform.forward * 0.5f, ForceMode.Impulse);
        
        Invoke("DoDropItem", 0.25f);
    }

    public void DoDropItem()
    {
        mLockPickup = false;
        
        Destroy((mCurrentItem as MonoBehaviour).GetComponent<Rigidbody>());

        mCurrentItem = null;
    }
    
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // Metodo para recoger objetos si estamos en su radio de recogida.
            if (mItemToPickup != null && Input.GetKeyDown(KeyCode.F))
            {
                Inventory.AddItem(mItemToPickup);
                mItemToPickup.OnPickup();
                HUD.CloseMessagePanel();
            }
            
            // Ejecutar la accion deseada cuando el item deseado está activo.
            if (mCurrentItem != null && Input.GetMouseButtonDown(0))
            {
                // TO DO: Definir que acción invidual hará cada item.
                _anim.SetTrigger("Attack");
            }
            
            
            // Regulamos con uns CheckSphere cuando el player está Grounded
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            Vector3 move = transform.right * x + transform.forward * z;

            // Controlamos el movimiento horizontal mediante WASD
            Controller.Move(move * speed * Time.deltaTime);

            // Regulamos el Velocity.y para realizar un salto(si está grounded).
            if (Input.GetButtonDown("Jump") && isGrounded)
            {
                velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
            }
            
            velocity.y += gravity * Time.deltaTime;

            // Controlamos la caída del Player x gravedad
            Controller.Move(velocity * Time.deltaTime);
        }
    }

    private IInventoryItem mItemToPickup = null;
    private void OnTriggerEnter(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            if(mLockPickup)
                return;
            
            mItemToPickup = item;
            HUD.OpenMessagePanel("");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        IInventoryItem item = other.GetComponent<IInventoryItem>();
        if (item != null)
        {
            HUD.CloseMessagePanel();
            mItemToPickup = null;
        }
    }


}
