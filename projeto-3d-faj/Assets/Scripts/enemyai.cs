using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyai : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject projectile;

    // Patrulha
    public Vector3 walkPoint;
    bool walkPointSet;
    public float walkPointRange;

    //Ataque
    public float timeBetweenAttacks;
    bool alreadyAttacked;

    //Estados
    public float sightRange, attackRange;
    public bool playerInsightRange, playerIntAttackRange;

    Animator myAnim;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerIntAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInsightRange && !playerIntAttackRange) Patrulhar();
        if (playerInsightRange && !playerIntAttackRange) PerseguirPlayer();
        if (playerIntAttackRange && playerInsightRange) AttackPlayer();

    }

    private void Patrulhar()
    {
        if (!walkPointSet) SearchwalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanteToWalkPoint = transform.position - walkPoint;

        if (distanteToWalkPoint.magnitude < 1f){
            walkPointSet = false;
            
        }
        myAnim.SetFloat("speed", 1);
            

    }

    private void SearchwalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up)){
            walkPointSet = true;
        }
            
    }
    private void PerseguirPlayer()
    {
        agent.SetDestination(player.position);
    }

    private void AttackPlayer()
    {
        agent.SetDestination(transform.position);
        transform.LookAt(player);

        if (!alreadyAttacked)
        {

            Rigidbody rb = Instantiate(projectile, transform.position, Quaternion.identity).GetComponent<Rigidbody>();
            rb.AddForce(transform.forward * 32f, ForceMode.Impulse);
            rb.AddForce(transform.up * 8f, ForceMode.Impulse);

            alreadyAttacked = true;
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }
    }

    private void ResetAttack()
    {
        alreadyAttacked = false;
    }

}
