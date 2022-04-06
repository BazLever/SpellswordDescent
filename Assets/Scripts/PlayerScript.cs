using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerScript : MonoBehaviour

{

    GameManager gm;
    Vector3 moveVec;
    Vector3 turnVec;

    CharacterController charController;


    //PlayerStats
    [Header ("Stats")]
    public float playerMoveSpeed;
    public float playerTurnSpeed;
    public float turnSpeedMultiplier = 10f;
    public float gravity;
    public float jumpHeight = 5f;
    public float swordAttackLength = 0.3f;
    public float mass = 3f;
    public float lungeForce = 10f;
    public float lungeAttackTime;

    [Header("Objects")]
    //PlayerObjects
    public GameObject magicMissile;
    public GameObject playerCam;
    public Transform groundCheck;
    public GameObject swordHitbox;
    public GameObject deathCam;


    [Header("Other")]
    public float groundDistance;
    public LayerMask groundMask;
    public LayerMask swordMask;

    bool jumped;
    bool lunging;

    bool swordAttack;
    float swordDelta;


    public bool grounded = true;
    float gravityY;

    float groundedTimer;
    float groundedTimerDelta;
    float xRotation;

    float lungeATDelta;

    Vector3 impact = Vector3.zero;


    void Start()
    {
        gm = GameObject.Find("GameManager").GetComponent<GameManager>();
        charController = gameObject.GetComponent<CharacterController>();
        swordHitbox.SetActive(false);

    }

    void Update()
    {

        gravityY -= gravity * Time.deltaTime;

        grounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);
        if (grounded && gravityY < 0)
        {
            gravityY = -0.5f;
        }


        Vector3 transformVec = transform.TransformVector(moveVec);
        charController.Move(new Vector3(transformVec.x, gravityY, transformVec.z)* playerMoveSpeed * Time.deltaTime);


        
        transform.Rotate(new Vector3(0, turnVec.x, 0) * playerTurnSpeed * Time.deltaTime);

        xRotation -= turnVec.y/4;
        xRotation = Mathf.Clamp(xRotation, -95f, 95f);
        playerCam.transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);

        //playerCam.transform.Rotate(new Vector3(-turnVec.y, 0, 0) * playerTurnSpeed * Time.deltaTime);


        if (swordAttack)
        {
            swordDelta += Time.deltaTime;
            if (swordDelta >= swordAttackLength)
            {
                swordHitbox.SetActive(false);
                swordDelta = 0f;
                swordAttack = false;
            }
        }


        if(impact.magnitude > 0.2f)
        {
            charController.Move(impact * Time.deltaTime);
            impact = Vector3.Lerp(impact, Vector3.zero, 5 * Time.deltaTime);
        }

        if (lunging)
        {
            swordHitbox.SetActive(true);
            lungeATDelta += Time.deltaTime;
            if (lungeATDelta >= lungeAttackTime)
            {
                lunging = false;
                lungeATDelta = 0f;
                swordHitbox.SetActive(false);
            }
        }

    }


    public void PlayerDeath()
    {
        Instantiate(deathCam, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "DeathBox")
        {
            PlayerDeath();
        }
    }

    public void AddImpact(Vector3 dir, float force)
    {
        dir.Normalize();
        if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        impact += dir.normalized * force / mass;
    }


    //Character Controller Inputs
    //================================

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        moveVec = new Vector3(inputVec.x, 0, inputVec.y);
    }

    public void OnLook(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        turnVec = new Vector3(inputVec.x, inputVec.y, 0);
    }

    public void OnJump()
    {
        if (grounded)
        {
            grounded = false;
            gravityY = jumpHeight;
        }

    }

    public void OnFireStaff()
    {
        if (gm.ammo > 0)
        {
            Instantiate(magicMissile, playerCam.transform.position, playerCam.transform.rotation);
            gm.ammo -= 1;
        }

        
    }

    public void OnFireSword()
    {
        swordHitbox.SetActive(true);
        swordAttack = true;
    }

    public void OnFireSwordLunge()
    {
        lunging = true;
        gravityY = 0f;
        AddImpact(transform.forward, lungeForce);
    }

    public void OnRestart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
