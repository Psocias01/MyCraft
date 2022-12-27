using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class IA_GoblinVaca : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    public int vacaGoblinHealth;
    public int vacaGoblinMaxHealth = 150;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Transform player;
    
    public int dropRatio1 = 100;
    public int dropRatio2 = 50;
    
    public Item itemToDrop1;
    public Item itemToDrop2;

    private bool isAlive = true;
    private bool TotallyDead = false;
    
    //Movimiento
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private int currentWaypointIndex;

    private void Awake()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();

        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Start()
    {
        _navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        vacaGoblinHealth = vacaGoblinMaxHealth;
        isAlive = true;
    }

    // Update is called once per frame
    void Update()
    {
        Patroling();
        
        if (!isAlive && !TotallyDead)
        {
            StartCoroutine(Morir());
        }
        if (vacaGoblinHealth <= 0)
        {
            isAlive = false;
            _navMeshAgent.velocity = Vector3.zero;
        }
    }

    public void TakeDamage(int damage)
    {
        vacaGoblinHealth -= damage;

        if (vacaGoblinHealth <= 0)
        {
            isAlive = false;
            movementSpeed = 0;
            
        }
    }

    private void Patroling()
    {
        _navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);

        _animator.SetBool("IsPatroling", true);
        
        _navMeshAgent.speed = this.movementSpeed;

        if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
            _navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
        }
    }
    
    private IEnumerator Morir()
    {
        TotallyDead = true;
        _animator.SetBool("IsDead", true);
        Debug.Log("EnemyMuriendo");
        yield return new WaitForSeconds(3);
        Inventory.instance.AddItem(itemToDrop1);
        float dropRate1 = Random.Range(0, 100);
        if (dropRatio1 >= dropRate1)
        {
            Inventory.instance.AddItem(itemToDrop2);
        }
        gameObject.SetActive(false);
        // Activar shader de dissolve
       
    }
}
