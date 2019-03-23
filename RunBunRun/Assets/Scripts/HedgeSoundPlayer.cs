using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeSoundPlayer : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip bushRustle;
    private GameObject gameManagerObject;
    private GameManager gameManager;
    // Use this for initialization
    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManagerObject = GameObject.Find("GameManager"); // have to do it this way because the hedge is a prefab while the game manager is not
        gameManager = gameManagerObject.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // play clip on collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !other.isTrigger) // so that the wolf radius doesn't trigger bush sounds
        {
            audioSource.PlayOneShot(bushRustle, gameManager.sfxSlider.value);
        }
    }
}
