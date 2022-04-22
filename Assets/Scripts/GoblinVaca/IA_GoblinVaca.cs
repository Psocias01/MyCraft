using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class IA_GoblinVaca : MonoBehaviour
{
    [SerializeField] private float movementSpeed;

    public int vacaGoblinHealth;
    public int vacaGoblinMaxHealth = 150;

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;
    private Transform player;

    private bool isAlive;
    
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
    }

    public void TakeDamage(int damage)
    {
        vacaGoblinHealth -= damage;

        if (vacaGoblinHealth <= 0)
        {
            movementSpeed = 0;
            _animator.SetBool("IsDead", true);
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
}
