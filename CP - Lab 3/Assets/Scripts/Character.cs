using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(CharacterController))]
public class Character : MonoBehaviour
{
    CharacterController cc;

    public float speed;
    public float jumpSpeed;
    public float rotationSpeed;
    public float gravity;

    // Setting type to 0 means use SimpleMove()
    // Setting type to 1 means use Move()
    [SerializeField] int type = 1;

    Vector3 moveDirection;

    // Make gun shoot
    public float projectileSpeed;
    public Rigidbody projectilePrefab;
    public Transform projectileSpawnPoint;

    public Transform thingToLookFrom;
    public float lookAtDistance;

    // Start is called before the first frame update
    void Start()
    {
        try
        {
            cc = GetComponent<CharacterController>();

            if (speed <= 0)
            {
                speed = 6.0f;
            }

            if (jumpSpeed <= 0)
            {
                jumpSpeed = 8.0f;
            }

            if (rotationSpeed <= 0)
            {
                rotationSpeed = 10.0f;
            }

            if (gravity <= 0)
            {
                gravity = 9.81f;
            }

            moveDirection = Vector3.zero;

            //throw new ArgumentNullException("Whoops");

            if (projectileSpeed <= 0)
            {
                projectileSpeed = 6.0f;
            }

            if (lookAtDistance <= 0)
            {
                lookAtDistance = 10.0f;
            }

            if (!projectilePrefab)
            {
                Debug.Log("Missing projectilePrefab on " + name);
            }

            if (!projectileSpawnPoint)
            {
                Debug.Log("Missing projectileSpawnPoint on " + name);
            }

        }
        catch (NullReferenceException e)
        {
            Debug.LogWarning(e.Message);
        }

    }

    // Update is called once per frame
    void Update()
    {
        // Using SimpleMove()
        if (type == 0)
        {
            // Use if not using MouseLook.CS
            //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

            Vector3 forward = transform.TransformDirection(Vector3.forward);

            float curSpeed = Input.GetAxis("Vertical") * speed;

            cc.SimpleMove(forward * curSpeed);

        }
        // Using Move()
        else if (type == 1)
        {
            if (cc.isGrounded)
            {
                moveDirection = new Vector3(0, 0, Input.GetAxis("Vertical"));

                // Use if not using MouseLook.CS
                //transform.Rotate(0, Input.GetAxis("Horizontal") * rotationSpeed, 0);

                moveDirection = transform.TransformDirection(moveDirection);

                moveDirection *= speed;

                if (Input.GetButtonDown("Jump"))
                    moveDirection.y = jumpSpeed;
            }

            moveDirection.y -= gravity * Time.deltaTime;

            cc.Move(moveDirection * Time.deltaTime);
        }

        //if(Input.GetKeyDown(KeyCode.LeftControl))
        if (Input.GetButtonDown("Fire1")) // Set in Edit | Project Settings | Input Manager
        {
            fire();
        }

        RaycastHit hit;

        if (!thingToLookFrom)
        {

            Debug.DrawRay(transform.position, transform.forward * lookAtDistance, Color.red);

            if (Physics.Raycast(transform.position, transform.forward, out hit, lookAtDistance))
            {
                if (hit.collider != null)
                {
                    // Find the hit reciver (if existant) and call the method
                    var hitReciver = hit.collider.gameObject.GetComponent<HitReciver>();
                    if (hitReciver != null)
                    {
                        hitReciver.OnRayHit();
                       // Debug.Log("Raycast hit:" + hit.transform.name);
                    }
                }

                 
            }

        }
        /*else
        {
            Debug.DrawRay(thingToLookFrom.transform.position, thingToLookFrom.transform.forward * lookAtDistance, Color.yellow);

            if (Physics.Raycast(thingToLookFrom.position, thingToLookFrom.forward, out hit, lookAtDistance))
            {
                Debug.Log("Raycast hit:" + hit.transform.name);
            }
        }*/
    }

    public void fire()
    {
        if (projectilePrefab && projectileSpawnPoint)
        {
                Rigidbody temp = Instantiate(projectilePrefab, projectileSpawnPoint.position, projectileSpawnPoint.rotation);

                temp.AddForce(projectileSpawnPoint.forward * projectileSpeed, ForceMode.Impulse);
        }
    }

    //Usage:
    //Both GameObjects Needs a Collider
    // - One or Both GameObjects need a Rigidbody
    private void OnCollisionEnter(Collision collision)
    {
       
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(gameObject);
        }
    }

    //Usage OnCollisionXXX:
    // - One GameObject Needs a Collider
    // - CharacterController mustt be added
    // - Behaves like OnCollisionStay

    /*private void OnControllerColliderHit(ControllerColliderHit collision)
    {
       
        Debug.Log("OnControllerColliderHit: " + collision.transform.name);
    }*/


    //Usage OnTriggerXXX
    // - Both GameObjects needs a Colliders
    // - One GameObject needs collider set to "IsTrigger"
    private void OnTriggerEnter(Collider other)
    {
      
    }

  
    // create a bool so that if the player is grounded it wont killl itt. 
}

