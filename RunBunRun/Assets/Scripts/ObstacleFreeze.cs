using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// this script freezes obstacles in place after they spawn in and drop to the ground level
public class ObstacleFreeze : MonoBehaviour {

    Rigidbody rb;
    float timer;

	// Use this for initialization
	void Start ()
    {
        rb = GetComponent<Rigidbody>();
        timer = 0.0f;
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;

        if (timer >= 3.0f)
        {
            rb.constraints = RigidbodyConstraints.FreezeAll;
        }
	}
}
