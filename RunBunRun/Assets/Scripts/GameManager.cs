using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image staminaRing;
    public Text carrotScoreText;
    public int carrotScoreNum = 0;
    public Text featherScoreText;
    public int featherScoreNum = 0;
    public float maxStamina = 60.0f; // 1 minute for now
    public float staminaTimer = 0.0f;
    public float wolfTimer = 0.0f;

    public GameObject player;
    private PlayerMovement playerMovement;
    private WolfSpawner wolfSpawner;

    // Use this for initialization
    void Start ()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
        wolfSpawner = gameObject.GetComponent<WolfSpawner>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        // update the timer
        staminaTimer += Time.deltaTime;

        // updates the visual
        staminaRing.fillAmount = staminaTimer / maxStamina;

        // check for stamina
        if(staminaTimer >= maxStamina)
        {
            // clamp the value
            staminaTimer = maxStamina;

            // let the playerMovement script know
            playerMovement.outOfStamina = true;
        }

        wolfTimer += Time.deltaTime;

        if(wolfTimer >= 5.0f)
        {
            wolfSpawner.SpawnWolf();
            wolfTimer = 0.0f;
        }
    }

    // method to update the carrot score
    public void UpdateCarrotScore()
    {
        carrotScoreNum++;
        carrotScoreText.text = "Carrots: " + carrotScoreNum;
    }

    // method to update the score
    public void UpdateFeatherScore()
    {
        featherScoreNum++;
        featherScoreText.text = "Feathers: " + featherScoreNum;
    }

    // call this method when a carrot has been picked up
    public void ResetStamina()
    {
        staminaTimer = 0.0f; // reset timer
        playerMovement.outOfStamina = false; // reset boolean
    }
}
