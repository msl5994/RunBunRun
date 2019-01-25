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

    // Use this for initialization
    void Start ()
    {
        // get a reference to the rigidbody
        rb = GetComponent<Rigidbody>();
        // only rotate on the Y axis when we collide with something
        rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
        rb.rotation = Quaternion.identity;
	}
	
	// Update is called once per frame
	void Update ()
    {
		if(Input.GetKeyUp(KeyCode.A))
        {
            if (turnTimer < .2f)
            {
                transform.Rotate(new Vector3(0.0f, angleIncrement, 0.0f));
            }
            turnTimer = 0.0f;
            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y + 15.0f, transform.rotation.z, 0.0f);
            //transform.rotation = Quaternion.Euler(0.0f, rb.rotation.y + angleIncrement, 0.0f);


            //rb.MoveRotation(Quaternion.Euler(0.0f, angleIncrement%360, 0.0f));
            //rb.rotation = new Quaternion(rb.rotation.x, rb.rotation.y + 45.0f, rb.rotation.z, 0.0f);
        }
        else if(Input.GetKeyUp(KeyCode.D) && turnTimer < .3f)
        {
            //transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y - 15.0f, transform.rotation.z, 0.0f);
            //transform.rotation = Quaternion.Euler(0.0f, rb.rotation.y - angleIncrement, 0.0f);
            transform.Rotate(new Vector3(0.0f, -angleIncrement, 0.0f));
            //rb.MoveRotation(Quaternion.Euler(0.0f, -angleIncrement%360, 0.0f));
            //rb.rotation = new Quaternion(rb.rotation.x, rb.rotation.y - 45.0f, rb.rotation.z, 0.0f);
        }
        else if (Input.GetKey(KeyCode.A))
        {
            turnTimer += Time.deltaTime;
            if (turnTimer > .2f)
            {
                transform.Rotate(new Vector3(0.0f, -30.0f * Time.deltaTime, 0.0f));
            }
        }
    }

    // for rigidbody physics and movement
    private void FixedUpdate()
    {
        rb.MovePosition(transform.position + transform.forward * Time.deltaTime * speed);
    }
}
