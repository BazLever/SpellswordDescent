using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRanged : MonoBehaviour
{

    Transform player;
    public float attackRange;
    public LayerMask mask;
    public float attackSpeed = 2f;
    public GameObject fireBall;
    float attackDelta;

    bool attacking;

    void Start()
    {
        player = GameObject.Find("PlayerHead").transform;
    }
    void Update()
    {

        transform.LookAt(player);

        
        RaycastHit hit;
        Debug.DrawLine(transform.position, transform.forward * attackRange, Color.red);
        Debug.DrawRay(transform.position, transform.forward * attackRange, Color.red);
        if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange) && hit.transform.tag == "Player")
        {
            attacking = true;
            Debug.Log("I can see you!");
        } else
        {
            attacking = false;
        }
        

        if (attacking == true)
        {
            attackDelta += Time.deltaTime;
            if (attackDelta >= attackSpeed)
            {
                Instantiate(fireBall, transform.position, transform.rotation);
                attackDelta = 0f;
            }
        }


    }

}
