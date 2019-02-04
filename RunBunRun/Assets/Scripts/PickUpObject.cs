using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour {

    private GameObject gameManager;
    private ScoreManager scoreManager;
    private CollectibleSpawner collectibleSpawner;

	// Use this for initialization
	void Start ()
    {
        // use this method to find the object instead of making it a public gameobject above
        // because this script will be attached to a prefab
        gameManager = GameObject.Find("GameManager");
        collectibleSpawner = gameManager.GetComponent<CollectibleSpawner>();
        scoreManager = gameManager.GetComponent<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // check for collisions
    private void OnCollisionEnter(Collision collision)
    {
        // if this object is hit by the player
        if(collision.gameObject.tag == "Player")
        {
            // add to the score
            scoreManager.UpdateScore();

            // remove it from the collectible list
            collectibleSpawner.collectibles.Remove(gameObject);

            // spawn a new collectible to replace this one
            collectibleSpawner.SpawnCollectible();

            // delete the object
            Destroy(gameObject);
        }
    }
}
