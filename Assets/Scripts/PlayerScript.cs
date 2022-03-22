using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerScript : MonoBehaviour

{

    GameManager gm;
    Vector3 moveVec;
    Vector3 turnVec;

    CharacterController charController;


    //PlayerStats
    [Header("Stats")]
    public float playerMoveSpeed;
    public float playerTurnSpeed;
    public float turnSpeedMultiplier = 10f;
    public float gravity;
    public float jumpHeight = 5f;
    public float swordAttackLength = 0.3f;
    public float playerLungeSpeed;
    public float playerDecellerateSpeed;

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


    bool swordAttack;
    float swordDelta;

    bool lungeAttack;
    float lungeDelta;
    float lungeSpeed;
    bool lunged;


    public bool grounded = true;
    float gravityY;

    float groundedTimer;
    float groundedTimerDelta;
    float xRotation;



    Vector2 movement;


    [Header("Debug")]
    public Vector2 velocity;
    public float velocityMax;
    public bool moving;


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





        //Movement Code
        //==========================

        if (moving)
        {
            velocity += movement * playerMoveSpeed * Time.deltaTime;

            if (velocity.x > velocityMax)
            {
                velocity.x = velocityMax;
            }
            if (velocity.x < -velocityMax)
            {
                velocity.x = -velocityMax;
            }


            if (velocity.y > velocityMax)
            {
                velocity.y = velocityMax;
            }
            if (velocity.y < -velocityMax)
            {
                velocity.y = -velocityMax;
            }

        }


        if (!lungeAttack)
        {
            moveVec.x = velocity.x; //* playerMoveSpeed;
            moveVec.z = velocity.y; //* playerMoveSpeed;
            Vector3 transformVec = transform.TransformVector(moveVec);
            charController.Move(new Vector3(transformVec.x, gravityY, transformVec.z) * playerMoveSpeed * Time.deltaTime);
 
        }

        



        transform.Rotate(new Vector3(0, turnVec.x, 0) * playerTurnSpeed * Time.deltaTime);

        xRotation -= turnVec.y / 4;
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

        if (lungeAttack)
        {
            charController.Move(transform.up * gravityY);
            lungeDelta += Time.deltaTime;
            if (lungeDelta >= 0.5f)
            {

                swordHitbox.SetActive(true);
                if (!lunged)
                {

                    velocity.x = playerLungeSpeed;
                    lunged = true;
                }
                charController.Move(transform.forward * velocity.x);
            }
            if (lungeDelta >= 1)
            {
                lungeAttack = false;
                swordHitbox.SetActive(false);
                lungeDelta = 0f;
                lunged = false;
            }
        }




        //Velocity calcs

        if (velocity.x > 0.2)
        {
            velocity.x -= playerDecellerateSpeed * Time.deltaTime;
        }
        else if (velocity.x < -0.2)
        {
            velocity.x += playerDecellerateSpeed * Time.deltaTime;
        }
        else
        {
            velocity.x = 0f;
        }

        if (velocity.y > 0.2)
        {
            velocity.y -= playerDecellerateSpeed * Time.deltaTime;
        }
        else if (velocity.y < -0.2)
        {
            velocity.y += playerDecellerateSpeed * Time.deltaTime;
        }
        else
        {
            velocity.y = 0f;
        }

        Debug.Log(gravityY);
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


    //Character Controller Inputs
    //================================

    public void OnMove(InputValue input)
    {
        Vector2 inputVec = input.Get<Vector2>();
        if (inputVec.x != 0 || inputVec.y != 0)
        {
            moving = true;
        }
        else
        {
            moving = false;
        }

        movement = inputVec;


        /*


        Vector2 inputVec = input.Get<Vector2>();
        moveVec = new Vector3(inputVec.x, 0, inputVec.y);
        velocity.x += inputVec.x;
        velocity.y += inputVec.y;
        //Debug.Log(inputVec);

        if (velocity.x > velocityMax)
        {
            velocity.x = velocityMax;
        }        
        if (velocity.y > velocityMax)
        {
            velocity.y = velocityMax;
        }
        */
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
        if (!lungeAttack)
        {
            swordHitbox.SetActive(true);
            swordAttack = true;
        }
    }

    public void OnFireLunge()
    {

        gravityY = 0;
        lungeAttack = true;
    }

}
