using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HedgeSoundPlayer : MonoBehaviour {

    private AudioSource audioSource;
    public AudioClip bushRustle;
    private GameManager gameManager;
    // Use this for initialization
    void Start ()
    {
        audioSource = gameObject.GetComponent<AudioSource>();
        gameManager = gameObject.GetComponent<GameManager>();
    }
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // play clip on collision
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            audioSource.PlayOneShot(bushRustle, gameManager.sfxSlider.value);
        }
    }
}
