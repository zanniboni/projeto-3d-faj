using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemyai : MonoBehaviour
{

    public NavMeshAgent agent;

    public Transform player;

    public LayerMask whatIsGround, whatIsPlayer;

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

    private void Awake()
    {
        player = GameObject.Find()
    }




}
