using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;

    public bool ranged;
    NavMeshAgent navAgent;


    void Start()
    {
        if (!ranged)
        {
            navAgent = gameObject.GetComponent<NavMeshAgent>();
            navAgent.speed = enemySpeed;
        }



    }


    void Update()
    {
        
    }

    public void Death()
    {
        Destroy(gameObject);
    }


}
