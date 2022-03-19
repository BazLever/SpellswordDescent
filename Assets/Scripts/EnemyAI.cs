using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{

    public NavMeshAgent navAgent;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            navAgent.SetDestination(other.transform.position);
        }
    }


}
