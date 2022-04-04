using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagicMissle : MonoBehaviour
{

    public float missileSpeed;
    public GameObject particleEffectOnHit;

    Vector3 velocity;

    public LayerMask collisionLayers;
    public LayerMask enemyLayer;

    void Start()
    {

        velocity = transform.forward * missileSpeed * Time.fixedDeltaTime;
    }


    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        Vector3 nextPos = transform.position + velocity;

        RaycastHit hit;
        if (Physics.SphereCast(transform.position, 0.5f, velocity.normalized, out hit, velocity.magnitude, collisionLayers))
        {
            Instantiate(particleEffectOnHit, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        }
        else if (Physics.SphereCast(transform.position, 0.5f, velocity.normalized, out hit, velocity.magnitude, enemyLayer))
        {
            Debug.Log("Enemy Hit!");
            if (hit.transform.tag == "Enemy")
            {
                hit.transform.gameObject.GetComponent<Enemy>().Death();
            }
            if (hit.transform.tag == "Crystal")
            {
                hit.transform.gameObject.GetComponent<DemonCrystal>().CrystalSmashed();
            }

            Instantiate(particleEffectOnHit, gameObject.transform.position, gameObject.transform.rotation);
            Destroy(gameObject);
        } else
        {
            transform.position = nextPos;
        }
    }



}
