using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour {

    private GameObject gameManagerObject;
    private GameManager gameManager;
    private CollectibleSpawner collectibleSpawner;

	// Use this for initialization
	void Start ()
    {
        // use this method to find the object instead of making it a public gameobject above
        // because this script will be attached to a prefab
        gameManagerObject = GameObject.Find("GameManager");
        collectibleSpawner = gameManagerObject.GetComponent<CollectibleSpawner>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // check for collisions
    private void OnCollisionEnter(Collision collision)
    {
        // if this object is hit by the player
        if(collision.gameObject.tag == "Player" && gameObject.tag == "Carrot")
        {
            // add to the score
            gameManager.UpdateCarrotScore();

            // reset the stamina timer
            gameManager.ResetStamina();

            // remove it from the collectible list
            collectibleSpawner.carrotCollectibles.Remove(gameObject);

            // spawn a new collectible to replace this one
            collectibleSpawner.SpawnCarrotCollectible();

            // delete the object
            Destroy(gameObject);
        }
        else if(collision.gameObject.tag == "Player" && gameObject.tag == "Feather")
        {
            // add to the score
            gameManager.UpdateFeatherScore();

            // remove it from the collectible list
            collectibleSpawner.featherCollectibles.Remove(gameObject);

            // spawn a new collectible to replace this one
            collectibleSpawner.SpawnFeatherCollectible();

            // delete the object
            Destroy(gameObject);
        }
    }
}
