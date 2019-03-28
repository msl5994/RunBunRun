using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {

    public Vector3 teleportPoint;
    private Rigidbody rb;

    public float speed = 25.0f;
    public float maxSpeed = 100.0f;
    public float angleIncrement = 45.0f;
    public float turnTimer = 0.0f;
    public float leftTurnTimer = 0.0f;
    public float rightTurnTimer = 0.0f;

    //public Vector3 jumpVector = new Vector3(0.0f, 5.0f, 0.0f);
    private float jumpForce = 5f;
    private bool isGrounded = true;
    private bool isJumping = false;

    private Vector3 touchVectorStart;
    private Vector3 touchVectorEnd;
    private bool turnTimerActive = false;
    private bool turnRight, turnLeft = false;
    public bool outOfStamina = false;

    public GameObject gameManagerObject;
    private GameManager gameManager;
    public GameObject wolfIndicatorPanel;
    public bool changeAlpha = false;
    public bool upAlpha = true;
    private Image indicatorImage;

    // audio variables
    int currentJumpSoundNum = 0;
    AudioSource audioSource;
    private AudioClip currentJumpSound;
    public AudioClip jump01;
    public AudioClip jump02;
    public AudioClip jump03;
    public AudioClip jump04;
    bool firstframe;
    bool chasing;
    private WolfSpawner wolfSpawner;

    // animation
    Animator anim;

    // Use this for initialization
    void Start ()
    {
        // get a reference to the rigidbody
        rb = GetComponent<Rigidbody>();
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationY | RigidbodyConstraints.FreezeRotationZ;
        rb.rotation = Quaternion.identity;

        // ignore collisions with certain obstacles
        Physics.IgnoreLayerCollision(9,10);
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        wolfSpawner = gameManagerObject.GetComponent<WolfSpawner>();
        indicatorImage = wolfIndicatorPanel.GetComponent<Image>();

        anim = GetComponent<Animator>();

        chasing = false;
        firstframe = false;
        upAlpha = true;
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
        CheckTurning();
        anim.SetTrigger("Run");

        // check if at max jump height
        if(this.transform.position.y >= 7f)
        {
            isJumping = false;
        }

        // flashing border
        if (changeAlpha)
        {
            if(upAlpha)
            {
                Debug.Log("In Up Alpha");
                Color temp = indicatorImage.color;
                temp.a += 0.5f * Time.deltaTime;
                indicatorImage.color = temp;
                if (indicatorImage.color.a >= 0.8f)
                {
                    Debug.Log("Alpha Down");
                    upAlpha = false;
                }
            }
            else
            {
                Debug.Log("In Down Alpha");
                Color temp = indicatorImage.color;
                temp.a -= 0.5f * Time.deltaTime;
                indicatorImage.color = temp;
                if (indicatorImage.color.a <= 0.02f)
                {
                    upAlpha = true;
                    Debug.Log("Alpha Up");
                }
            }
        }
    }

    // method to jump
    public void Jump()
    {
        if (isGrounded)
        {
            isGrounded = false;
            isJumping = true;
            // randomly select the audio clip
            currentJumpSoundNum = UnityEngine.Random.Range(1,5);
            switch(currentJumpSoundNum)
            {
                case 1: currentJumpSound = jump01;
                    break;
                case 2: currentJumpSound = jump02;
                    break;
                case 3: currentJumpSound = jump03;
                    break;
                case 4: currentJumpSound = jump04;
                    break;
                default: currentJumpSound = jump02;
                    break;
            }
            audioSource.PlayOneShot(currentJumpSound, gameManager.sfxSlider.value);

            //rb.velocity += gameObject.transform.up.normalized * 1000.0f;
            //rb.AddForce(transform.forward * jumpForce, ForceMode.Impulse);
        }
    }

    // for rigidbody physics and movement
    private void FixedUpdate()
    {
        // reset velocity
        rb.velocity = new Vector3(0.0f,0.0f,0.0f);

        if(outOfStamina)
        {
            //rb.velocity += gameObject.transform.forward.normalized * speed / 4.0f;
            //rb.velocity += (gameObject.transform.up.normalized * -1.0f); // account for gravity
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * (speed/4.0f));
        }
        else
        {
           // rb.velocity += gameObject.transform.forward.normalized * speed;
            //rb.velocity += (gameObject.transform.up.normalized * -1.0f); // account for gravity
            rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
        }
        if(isJumping)
        {
            rb.AddForce(Vector3.up * 40f, ForceMode.Impulse);
        }
        rb.AddForce(Vector3.down * 20f, ForceMode.Impulse);

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
        //Debug.Log("Started");
        turnLeft = true;
        leftTurnTimer = 0.0f;
    }

    // stop turn
    public void StopLeftTurnTimer()
    {
        //Debug.Log("Stopped");
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
        //Debug.Log("Started");
        turnRight = true;
        rightTurnTimer = 0.0f;
    }

    // stop turn
    public void StopRightTurnTimer()
    {
        //Debug.Log("Stopped");
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
            isGrounded = true;
            //Debug.Log("Hit obstacle");
        }
        if(collision.gameObject.tag == "Ground")
        {
            //Debug.Log("Landed");
            isGrounded = true;
        }
        if(collision.gameObject.tag == "Wolf")
        {
            wolfIndicatorPanel.SetActive(false);
            changeAlpha = false;
            gameManagerObject.GetComponent<AudioSource>().pitch = 1.0f;
            gameManager.firstFrame = true;
            gameManager.gameState = GameManager.GameState.GameOver;
            gameManager.prevGameState = GameManager.GameState.Game;
            gameManager.GameOver();
        }
    }

    // trigger zone for wolves and changing sound pitch
    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Wolf")
        {
            Debug.Log("Pitched Up");
            gameManagerObject.GetComponent<AudioSource>().pitch = 1.5f;
            wolfIndicatorPanel.SetActive(true);
            changeAlpha = true;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Wolf")
        {
            Debug.Log("Pitched Down");
            gameManagerObject.GetComponent<AudioSource>().pitch = 1.0f;
            wolfIndicatorPanel.SetActive(false);
            changeAlpha = false;
        }
    }
}
