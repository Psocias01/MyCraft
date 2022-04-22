using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class AI_Enemy : MonoBehaviour
{
   //Variables
   [SerializeField] private float speed;
   [SerializeField] private float runSpeed;
   [SerializeField] private LayerMask player_mask;
   public int enemy_Health;
   public int enemy_MaxHealth = 100;
   
   public int dropRatio1 = 50;
   public int dropRatio2 = 50;
   public int dropRatio3 = 10;
   public int dropRatio4 = 5;

   public Item itemToDrop1;
   public Item itemToDrop2;
   public Item itemToDrop3;
   public Item itemToDrop4;

   private bool isAlive = true;
   private bool TotallyDead = false;


   private Vector3 AttackPos;
   
   private NavMeshAgent _navMeshAgent;
   private Animator _animator;
   private Transform player;
   
   //Patrolling
   [SerializeField] private Transform[] waypoints;
   [SerializeField] private int currentWaypointIndex;
   [SerializeField] private int tiempoEntreWaypoints;
   
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

       if (!isAlive && !TotallyDead)
       {
           StartCoroutine(Morir());
       }
       if (enemy_Health <= 0)
       {
           isAlive = false;
           _navMeshAgent.velocity = Vector3.zero;
       }
   }

   public void TakeDamage(int damage)
   {
       // Activar animación de recibir daño (opcional).
       
       enemy_Health -= damage;
       
       if (enemy_Health <= 0)
       {
           isAlive = false;
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
           //StartCoroutine(WaitForWaypoint());
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
       _navMeshAgent.velocity = Vector3.zero;

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

   private IEnumerator WaitForWaypoint()
   {
       yield return new WaitForSeconds(tiempoEntreWaypoints);
       
       
   }
   
   private IEnumerator Morir()
   {
       TotallyDead = true;
       _animator.SetTrigger("isDead");
       Debug.Log("EnemyMuriendo");
       yield return new WaitForSeconds(3);
       
       float dropRate1 = Random.Range(0, 100);
       if (dropRatio1 >= dropRate1)
       {
           Inventory.instance.AddItem(itemToDrop1);
       }
            
       float dropRate2 = Random.Range(0, 100);
       if (dropRatio2 >= dropRate2)
       {
           Inventory.instance.AddItem(itemToDrop2);
       }
       
       float dropRate3 = Random.Range(0, 100);
       if (dropRatio3 >= dropRate3)
       {
           Inventory.instance.AddItem(itemToDrop3);
       }
       
       float dropRate4 = Random.Range(0, 100);
       if (dropRatio4 >= dropRate4)
       {
           Inventory.instance.AddItem(itemToDrop4);
       }
       gameObject.SetActive(false);
   }
   
   
}
