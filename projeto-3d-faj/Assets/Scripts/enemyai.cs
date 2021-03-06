using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

    public GameObject projectile;

    public Transform arm;

    public static int count = 0;

    static int morteIA = 0;


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
    public float speed = 1f;
    private void Awake()
    {
        myAnim = GetComponent<Animator>();
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        myAnim.SetFloat("speed", 1);
        count++;
    }

    private void Update()
    {
        playerInsightRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);
        playerIntAttackRange = Physics.CheckSphere(transform.position, sightRange, whatIsPlayer);

        if (!playerInsightRange && !playerIntAttackRange) Patrulhar();
        if (playerInsightRange && !playerIntAttackRange) PerseguirPlayer();
        if (playerIntAttackRange && playerInsightRange) AttackPlayer();
        verificaMorte();

    }

    private void Patrulhar()
    {


        if (!walkPointSet) SearchwalkPoint();

        if (walkPointSet)
            agent.SetDestination(walkPoint);

        Vector3 distanteToWalkPoint = transform.position - walkPoint;

        if (distanteToWalkPoint.magnitude < 1f)
        {
            walkPointSet = false;

        }

        if (speed < 1f)
        {
            speed += 0.1f;
            myAnim.SetFloat("speed", speed);
        }




    }

    private void SearchwalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomx = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomx, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up))
        {
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
            speed = 0;
            myAnim.SetFloat("speed", speed);
            Rigidbody rb = Instantiate(projectile, arm.position, Quaternion.identity).GetComponent<Rigidbody>();
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


    void verificaMorte()
    {

        if (gameObject.GetComponent<PlayerHealth>().getHealth() < 0.1)
        {
            Destroy(gameObject, 0.8f);
            FindObjectOfType<GameManager>().EndGame();

            

            morteIA = morteIA + 1;

            if (morteIA > 13)
            {
                SceneManager.LoadScene("YouWin");
            }
        }
    }
}