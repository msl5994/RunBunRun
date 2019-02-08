using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfMovement : MonoBehaviour {

    // Use this for initialization
    public Vector3 velocity, acceleration, direction, wolfPos;
    public float mass;

    public float maxSpeed;

    public GameObject bunny;
    void Start () {
        wolfPos = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        wolfPos = transform.position;
        velocity += acceleration; // add a to v
        wolfPos += velocity; // add v to p
        direction = velocity.normalized; // get d from v
        transform.rotation = Quaternion.Euler(direction.x, direction.y, direction.z);
        transform.position = wolfPos; // update transform.position
        acceleration = Vector3.zero; // start fresh each frame

        Vector3 seekForce = SeekForce(bunny.transform.position);
        ApplyForce(seekForce);
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
}
