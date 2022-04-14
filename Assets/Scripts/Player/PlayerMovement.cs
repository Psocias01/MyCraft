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

    public bool puedeTalar;
    
    // INVENTORY REFERENCIAS
    [SerializeField] private InventoryUI InventoryUI;

    public float speed = 12f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    #endregion
   

    #region Private Members
    
    private Vector3 velocity;
    private bool isGrounded;

    [SerializeField] private HealthBar mHealthBar;
    
    #endregion
    

    
    
    // Start is called before the first frame update
    void Start()
    {
        Controller = GetComponent<CharacterController>();
        mHealthBar = InventoryUI.transform.Find("HealthBar").GetComponent<HealthBar>();
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
    
    
    // Update is called once per frame
    void Update()
    {
        if (!isDead)
        {
            // Ejecutar la accion deseada cuando el item deseado está activo.
            if (Input.GetMouseButtonDown(0))
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
}
