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

    public GameObject Hand;

    public Animator _anim;

    public bool Hacha;
    public bool Espada;
    public bool Pico;
    public bool Baston;
    public bool Farolillo;
    public bool Mano;

    public int municionMagiaElectrica;
    
    public int municionMagiaFuego;
    
    public int municionMagiaHielo;
    
    // INVENTORY REFERENCIAS
    [SerializeField] private InventoryUI InventoryUI;

    public float speed = 10f;
    public float normalSpeed = 10f;
    public float sprintSpeed = 18f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;
    public float playerMovement;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    public int MagiaDeFuego;
    public int MagiaDeRayo;
    public int MagiaDeHielo;

    #endregion
   

    #region Private Members
    
    private Vector3 velocity;
    private bool isGrounded;
    private float playerIsMoving;
    private bool isRunning;

    [SerializeField] private HealthBar mHealthBar;
    [SerializeField] private HungerBar mHungerBar;
    [SerializeField] private InventoryUI inventoryUI;
    
    #endregion
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        mHealthBar = InventoryUI.transform.Find("HealthBar").GetComponent<HealthBar>();
        mHealthBar.Min = 0;
        mHealthBar.Max = Health;
        Mano = true;
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

    #region Food

    public int foodAmount = 100;
    public bool hasSpent;
    public float timeToSpendFood = 15f;
    
    
    IEnumerator ConsumeFood()
    {
        hasSpent = true;
        yield return new WaitForSeconds(timeToSpendFood);
        foodAmount -= 10;
        hasSpent = false;
        
        mHungerBar.SetFood(foodAmount);
    }

    #endregion
    
    
    // Update is called once per frame
    void Update()
    {
        if (!hasSpent)
        {
            StartCoroutine(ConsumeFood());
        }
        
        
        if (!isDead)
        {
            // Ejecutar la accion deseada cuando el item deseado está activo.
            if (Input.GetMouseButtonDown(0) && !inventoryUI.InventoryOpen && !inventoryUI.OptionsPanelOpen)
            {
                if (Pico)
                {
                    _anim.SetTrigger("Picar");
                }
                else if (Hacha)
                {
                    _anim.SetTrigger("Talar");
                }
                else if (Espada || Mano)
                {
                    _anim.SetTrigger("Attack");
                }
                else if (Baston)
                {
                    _anim.SetTrigger("Magic");
                }
                else if (Farolillo)
                {
                    _anim.SetBool("Farol",Farolillo);
                }
                // TO DO: Definir que acción invidual hará cada item.
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                Inventory.instance.UnequipItem();
                Hacha = false;
                Espada = false;
                Pico = false;
                Baston = false;
                Farolillo = false;
                Mano = true;
                _anim.SetBool("Farol",Farolillo);
            }

            if (Input.GetKey(KeyCode.LeftShift))
            {
                speed = sprintSpeed;
                
                isRunning = true;
                _anim.SetBool("isRunning", isRunning);
            }
            else
            {
                speed = normalSpeed;
                
                isRunning = false;
                _anim.SetBool("isRunning", isRunning);
            }
            
            
            // Regulamos con uns CheckSphere cuando el player está Grounded
            isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

            if (isGrounded && velocity.y < 0)
            {
                velocity.y = -2f;
            }
            
            float x = Input.GetAxis("Horizontal");
            float z = Input.GetAxis("Vertical");

            playerIsMoving = (x + z);
            if (playerIsMoving != 0)
            {
                playerMovement = 1;
                _anim.SetFloat("PlayerMovement", playerMovement);
            }
            else if (playerIsMoving == 0)
            {
                playerMovement = 0;
                _anim.SetFloat("PlayerMovement", playerMovement);
            }

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
}
