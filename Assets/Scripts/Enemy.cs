using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : MonoBehaviour
{
    public float enemySpeed;

    public bool ranged;
    NavMeshAgent navAgent;

    GameManager gm;
    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();

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
        gm.kills++;
        Destroy(gameObject);
    }


}
