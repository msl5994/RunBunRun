using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

    public Vector3 teleportPoint;
    private Rigidbody rb;

    public float speed = 1.0f;
    public float maxSpeed = 10.0f;
    public float angleIncrement = 45.0f;
    public float turnTimer = 0.0f;
    public float leftTurnTimer = 0.0f;
    public float rightTurnTimer = 0.0f;

    //public Vector3 jumpVector = new Vector3(0.0f, 5.0f, 0.0f);
    private float jumpForce = 5f;
    private bool isGrounded = true;

    private Vector3 touchVectorStart;
    private Vector3 touchVectorEnd;
    private bool turnTimerActive = false;
    private bool turnRight, turnLeft = false;

    // Use this for initialization
    void Start ()
    {
        // get a reference to the rigidbody
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // start the timer
        if(turnTimerActive)
        {
            turnTimer += Time.deltaTime;
        }
        if (turnLeft)
        {
            leftTurnTimer += Time.deltaTime;
            if(leftTurnTimer > 0.2f)
            {
                transform.Rotate(new Vector3(0.0f, -30.0f * Time.deltaTime, 0.0f));
            }
        }
        if (turnRight)
        {
            rightTurnTimer += Time.deltaTime;
            if (rightTurnTimer > 0.2f)
            {
                transform.Rotate(new Vector3(0.0f, 30.0f * Time.deltaTime, 0.0f));
            }
        }

        /*
        // if there has been a touch
        if(Input.touchCount > 0)
        {
            // find out which phase it is in
            Touch touch = Input.GetTouch(0);
            switch(touch.phase)
            {
                case TouchPhase.Began:
                    // determine which side of the screen it was on
                    touchVectorStart = touch.position;
                    if (touch.position.x > (Display.main.systemWidth / 2.0f) + (Display.main.systemWidth / 4.0f))
                    {
                        turnRight = true;
                        turnLeft = false;
                    }
                    else if (touch.position.x < (Display.main.systemWidth / 2.0f) - (Display.main.systemWidth/4.0f))
                    {
                        turnLeft = true;
                        turnRight = false;
                    }
                    else
                    {
                        if(isGrounded)
                        {
                            Jump();
                            isGrounded = false;
                        }
                    }
                    // start the timer
                    turnTimerActive = true;
                    break;
                case TouchPhase.Moved:
                    // if the finger has moved, find out if it moved to a different side of the screen
                    touchVectorStart = touch.position;
                    if (touch.position.x > Display.main.systemWidth / 2.0f)
                    {
                        turnRight = true;
                        turnLeft = false;
                    }
                    else
                    {
                        turnLeft = true;
                        turnRight = false;
                    }
                    // start fine tuning the turn instead of a sharp turn if the timer has been going
                    turnTimerActive = true;
                    if (turnTimer > .2f)
                    {
                        if (turnRight)
                        {
                            transform.Rotate(new Vector3(0.0f, 30.0f * Time.deltaTime, 0.0f));
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0.0f, -30.0f * Time.deltaTime, 0.0f));
                        }
                    }
                    break;
                case TouchPhase.Stationary:
                    // if the finger has not moved we don't need to recalculate the side of the screen, but do need to fine tune turn
                    if (turnTimer > .2f)
                    {
                        if(turnRight)
                        {
                            transform.Rotate(new Vector3(0.0f, 30.0f * Time.deltaTime, 0.0f));
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0.0f, -30.0f * Time.deltaTime, 0.0f));
                        }
                    }
                    break;
                case TouchPhase.Ended:
                    // sharp turning on the key-up of the touch if it wasn't held
                    touchVectorEnd = touch.position;
                    if (turnTimer < .2f)
                    {
                        if(turnRight)
                        {
                            transform.Rotate(new Vector3(0.0f, angleIncrement, 0.0f));
                        }
                        else
                        {
                            transform.Rotate(new Vector3(0.0f, -angleIncrement, 0.0f));
                        }    
                    }
                    turnTimer = 0.0f;
                    break;
            }
        }*/

        CheckTurning();
    }

    // method to jump
    public void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    // for rigidbody physics and movement
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        //rb.velocity = new Vector3(transform.forward.x * speed, 0.0f, transform.forward.z * speed);
        // jump moved here because it uses physics
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Jump();
        }
    }

    // methods to turn left using a button for mobile use
    // start turn
    public void StartLeftTurnTimer()
    {
        Debug.Log("Started");
        turnLeft = true;
        leftTurnTimer = 0.0f;
    }

    // stop turn
    public void StopLeftTurnTimer()
    {
        Debug.Log("Stopped");
        turnLeft = false;
        if (leftTurnTimer < .2f)
        {
            transform.Rotate(new Vector3(0.0f, -angleIncrement, 0.0f));
        }
        leftTurnTimer = 0.0f;
    }

    // methods to turn right using a button for mobile use
    // start turn
    public void StartRightTurnTimer()
    {
        Debug.Log("Started");
        turnRight = true;
        rightTurnTimer = 0.0f;
    }

    // stop turn
    public void StopRightTurnTimer()
    {
        Debug.Log("Stopped");
        turnRight = false;
        if (rightTurnTimer < .2f)
        {
            transform.Rotate(new Vector3(0.0f, angleIncrement, 0.0f));
        }
        rightTurnTimer = 0.0f;
    }


    // turning for keyboard
    private void CheckTurning()
    {
        if (Input.GetKeyUp(KeyCode.A))
        {
            if (turnTimer < .2f)
            {
                transform.Rotate(new Vector3(0.0f, -angleIncrement, 0.0f));
            }
            turnTimer = 0.0f;
        }
        if (Input.GetKeyUp(KeyCode.D))
        {
            if (turnTimer < .2f)
            {
                transform.Rotate(new Vector3(0.0f, angleIncrement, 0.0f));
            }
            turnTimer = 0.0f;
        }
        if (Input.GetKey(KeyCode.A))
        {
            turnTimer += Time.deltaTime;
            if (turnTimer > .2f)
            {
                transform.Rotate(new Vector3(0.0f, -30.0f * Time.deltaTime, 0.0f));
            }
        }
        if (Input.GetKey(KeyCode.D))
        {
            turnTimer += Time.deltaTime;
            if (turnTimer > .2f)
            {
                transform.Rotate(new Vector3(0.0f, 30.0f * Time.deltaTime, 0.0f));
            }
        }
    }

    // collision detection for resetting the ability to jump if the player collides with the ground or an obstacle
    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstacle")
        {
            Debug.Log("Hit obstacle");
            isGrounded = true;
        }
        if(collision.gameObject.tag == "Ground")
        {
            Debug.Log("Landed");
            isGrounded = true;
        }
    }


}
