﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Quadrant2 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    // assign obstacles, wolves, and players, and collectibles to a quadrant
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            other.gameObject.GetComponent<PlayerMovement>().quadrant = 2;
        }
        else if (other.gameObject.tag == "Wolf")
        {
            other.gameObject.GetComponent<WolfMovement>().quadrant = 2;
        }
        else if (other.gameObject.tag == "Obstacle")
        {
            other.gameObject.GetComponent<Obstacle>().quadrant = 2;
            other.gameObject.transform.parent = gameObject.transform;
        }
    }
}
