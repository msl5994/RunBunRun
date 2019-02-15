using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour {

    // Use this for initialization
    public Vector3 velocity, acceleration, direction, wolfPos;
    public float mass;

    public float maxSpeed;

    private Rigidbody rb;

    private GameObject bunny;

    private float angle;
    void Start () {
        wolfPos = transform.position;
        rb = gameObject.GetComponent<Rigidbody>();
        bunny = GameObject.FindGameObjectWithTag("Player");
        angle = Random.Range(0f, 2f * Mathf.PI);
    }
	
	// Update is called once per frame
	void Update () {

        //velocity += acceleration; // add a to v
        //wolfPos += velocity; // add v to p
        //direction = velocity.normalized; // get d from v
        //transform.rotation = Quaternion.Euler(direction.x, direction.y, direction.z);
        //transform.position = wolfPos; // update transform.position
        //acceleration = Vector3.zero; // start fresh each frame

        //Vector3 seekForce = SeekForce(bunny.transform.position);
        //ApplyForce(seekForce);

    }

    // for physics calculations
    private void FixedUpdate()
    {
        wolfPos = transform.position;
        //direction = bunny.transform.position - wolfPos;
        direction = rb.velocity.normalized;
        //direction = direction.normalized;
        rb.AddForce(SeekForce(bunny.transform.position));
        //rb.AddForceAtPosition(SeekForce(bunny.transform.position), wolfPos);
        /*
        if (Vector3.Distance(wolfPos, bunny.transform.position) < 100)
        {
            Debug.Log("seeking");
            rb.AddForceAtPosition(SeekForce(bunny.transform.position), wolfPos);
        }*/
        /*
        else
        {
            rb.AddForceAtPosition(WanderForce(), wolfPos);
        }*/

        //rb.MoveRotation(Quaternion.Euler(direction.x, direction.y, direction.z));
        rb.rotation = Quaternion.Euler(direction.x, direction.y, direction.z);
        transform.forward = direction;

        // clamp the speed
        if (rb.velocity.magnitude > maxSpeed)
        {
            
        }
        if (rb.velocity.x > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, rb.velocity.z);
        }
        if (rb.velocity.y > maxSpeed)
        {
            rb.velocity = new Vector3(rb.velocity.x, maxSpeed, rb.velocity.z);
        }
        if (rb.velocity.z > maxSpeed)
        {
            rb.velocity = new Vector3(maxSpeed, rb.velocity.y, maxSpeed);
        }
    }

    private void ApplyForce(Vector3 force)
    {
        acceleration += force / mass;
    }

    private Vector3 SeekForce(Vector3 bunnyPos)
    {
        Vector3 desiredV = bunnyPos - wolfPos;
        desiredV = desiredV.normalized;

        desiredV *= maxSpeed;

        return desiredV - velocity;
    }

    public Vector3 WanderForce()
    {
        //Debug.Log("wandering");

        Vector3 circleCenter = wolfPos + this.gameObject.transform.forward;
        float radius = maxSpeed / 4;

        // slightly changing direction in which the wolf wanders
        float angleAdd = Random.Range(0.0f, 0.01f);

        // wandering left or right randomly
        int leftOrRight = (int)Random.Range(0.0f, 1.0f) * 100000;
        if(leftOrRight % 2 == 0)
        {
            angleAdd *= -1;
        }
        angle += angleAdd;

        float randX = radius * Mathf.Cos(angle);
        float yLoc = 0f;
        float randZ = radius * Mathf.Sin(angle);

        Vector3 target = circleCenter + new Vector3(randX, yLoc, randZ);

        return SeekForce(target);
    }
}
