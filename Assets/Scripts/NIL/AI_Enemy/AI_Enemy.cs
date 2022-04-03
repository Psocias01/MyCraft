using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class AI_Enemy : MonoBehaviour
{
   //Variables
   [SerializeField] private float speed;
   [SerializeField] private float runSpeed;
   [SerializeField] private LayerMask player_mask;
   public int enemy_Health;
   public int enemy_MaxHealth = 100;

   private bool isAlive = true;
   
   
   private NavMeshAgent _navMeshAgent;
   private Animator _animator;
   private Transform player;
   
   //Patrolling
   [SerializeField] private Transform[] waypoints;
   [SerializeField] private int currentWaypointIndex;
   
   //Attacking

   [SerializeField] public float timeBetweenAttacks;
   private bool alreadyAttacked;

   //States
   [SerializeField] public float sightRange, attackRange;
   private bool playerInSightRange, playerInAttackRange;

   private void Awake()
   {
      _navMeshAgent = GetComponent<NavMeshAgent>();
      _animator = GetComponent<Animator>();

      player = GameObject.FindGameObjectWithTag("Player").transform;
   }

   void Start()
   {
       _navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
       enemy_Health = enemy_MaxHealth;
       isAlive = true;
   }
   void Update()
   {
       playerInSightRange = Physics.CheckSphere(transform.position, sightRange, player_mask);
       playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, player_mask);

       if (!playerInSightRange && !playerInAttackRange)
       {
           Patroling();
       }

       if (playerInSightRange && !playerInAttackRange)
       {
           ChasePlayer();
       }

       if (playerInSightRange && playerInAttackRange)
       {
           AttackPlayer();
       }

       if (!isAlive)
       {
           gameObject.SetActive(false);
       }
   }

   public void TakeDamage(int damage)
   {
       // Activar animación de recibir daño (opcional).
       
       enemy_Health -= damage;
       
       if (enemy_Health <= 0)
       {
           // TO DO: Activar shader de muerte
           gameObject.SetActive(false);
       }
   }
   
   private void Patroling()
   {
       _navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
       
       //_animator.SetBool("isPatrolling", true);
       //_animator.SetBool("isChassing", false);

       _navMeshAgent.speed = this.speed;

       if (_navMeshAgent.remainingDistance < _navMeshAgent.stoppingDistance)
       {
           currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
           _navMeshAgent.SetDestination(waypoints[currentWaypointIndex].position);
       }
   }

   private void ChasePlayer()
   {
       //_animator.SetBool("isPatrolling", false);
       //_animator.SetBool("isChassing", true);

       _navMeshAgent.SetDestination(player.position);
       _navMeshAgent.acceleration = 8;

       _navMeshAgent.speed = this.runSpeed;
   }

   private void AttackPlayer()
   {
       _navMeshAgent.SetDestination(player.position);

       if (!alreadyAttacked)
       {
           alreadyAttacked = true;
           Debug.Log("Atack");
           
           _animator.SetTrigger("Attack");
           
           Invoke(nameof(ResetAttack), timeBetweenAttacks);
       }
   }
   
   

   private void ResetAttack()
   {
       alreadyAttacked = false;
   }
}
