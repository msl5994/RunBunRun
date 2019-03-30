using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PickUpObject : MonoBehaviour {

    private GameObject gameManagerObject;
    private GameManager gameManager;
    private CollectibleSpawner collectibleSpawner;
    private AudioSource audioSource;
    private AudioClip audioClip;
    private bool timerActive;
    private float timer;

	// Use this for initialization
	void Start ()
    {
        // use this method to find the object instead of making it a public gameobject above
        // because this script will be attached to a prefab
        gameManagerObject = GameObject.Find("GameManager");
        collectibleSpawner = gameManagerObject.GetComponent<CollectibleSpawner>();
        gameManager = gameManagerObject.GetComponent<GameManager>();
        audioSource = gameObject.GetComponent<AudioSource>();
        audioClip = audioSource.clip;
        timerActive = false;
	}
	
	// Update is called once per frame
	void Update ()
    {
        // just update the timer
        if(timerActive)
        {
            timer += Time.deltaTime;
        }

        // destroy the gameobject after 3 seconds to allow the sound effect to play
        if(timer >= 3.0f)
        {
            // delete the object
            Destroy(gameObject);
        }
    }

    // check for collisions
    private void OnCollisionEnter(Collision collision)
    {
        // if this object is hit by the player
        if(collision.gameObject.tag == "Player" && gameObject.tag == "Carrot")
        {
            // play the sound
            audioSource.PlayOneShot(audioClip, gameManager.sfxSlider.value);

            // disable the renderers
            foreach (GameObject carrot in collectibleSpawner.carrotCollectibles)
            {
                if (carrot == gameObject)
                {
                    // disable mesh renderer and collider
                    carrot.transform.GetChild(0).gameObject.SetActive(false);
                    carrot.GetComponent<CapsuleCollider>().enabled = false;
                    carrot.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }

            // start the timer to destroy after 3 seconds
            timerActive = true;

            // remove it from the collectible list
            collectibleSpawner.carrotCollectibles.Remove(gameObject);

            // add to the score
            gameManager.UpdateCarrotScore();

            // reset the stamina timer
            gameManager.ResetStamina();

            // spawn a new collectible to replace this one
            collectibleSpawner.SpawnCarrotCollectible();            
        }
        else if(collision.gameObject.tag == "Player" && gameObject.tag == "Feather")
        {
            // play the sound
            audioSource.PlayOneShot(audioClip, gameManager.sfxSlider.value * 5.0f);

            // disable the renderers
            foreach (GameObject feather in collectibleSpawner.featherCollectibles)
            {
                if (feather == gameObject)
                {
                    // disable mesh renderer and collider
                    feather.GetComponent<SpriteRenderer>().enabled = false;
                    feather.GetComponent<SphereCollider>().enabled = false;
                    //feather.GetComponentInChildren<MeshRenderer>().enabled = false;
                }
            }

            // start the timer to destroy after 3 seconds
            timerActive = true;

            // remove it from the collectible list
            collectibleSpawner.featherCollectibles.Remove(gameObject);

            // add to the score
            gameManager.UpdateFeatherScore();

            // spawn a new collectible to replace this one
            collectibleSpawner.SpawnFeatherCollectible();            
        }
    }
}
