using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;

    NavMeshAgent navAgent;


    void Start()
    {
        navAgent = gameObject.GetComponent<NavMeshAgent>();
        navAgent.speed = enemySpeed;


    }


    void Update()
    {
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
