using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FeatherFog : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // if it hits a feather
    public void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Feather")
        {
            //Debug.Log("Found Feather");
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Feather")
        {
            //Debug.Log("Still in Feather Sight Range");
            other.gameObject.GetComponent<SpriteRenderer>().enabled = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Feather")
        {
            //Debug.Log("Lost Feather");
            other.gameObject.GetComponent<SpriteRenderer>().enabled = false;
        }
    }
}
