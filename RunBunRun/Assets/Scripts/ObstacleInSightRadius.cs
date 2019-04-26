using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleInSightRadius : MonoBehaviour
{
    private Component[] meshes;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // trigger method
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            //Debug.Log("Obstacle Found");
            meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach(MeshRenderer mesh in meshes)
            {
                mesh.enabled = true;
            }
        }
        if (other.gameObject.tag == "Plant")
        {
            //Debug.Log("Plant Found");
            meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Obstacle")
        {
            //Debug.Log("Obstacle Found");
            meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
        }
        if (other.gameObject.tag == "Plant")
        {
            //Debug.Log("Plant Found");
            meshes = other.gameObject.GetComponentsInChildren<MeshRenderer>();
            foreach (MeshRenderer mesh in meshes)
            {
                mesh.enabled = false;
            }
        }
    }
}
