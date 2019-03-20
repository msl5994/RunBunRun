using System;
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
    public float wolfSpawnTimer = 30.0f;
    private float balanceTimer = 0.0f;

    public GameObject player;
    private PlayerMovement playerMovement;
    private WolfSpawner wolfSpawner;

    public bool gameOver = false;

    public GameObject gameOverPanel;
    public GameObject splashScreenPanel;
    public GameObject helpScreenPanel;
    public GameObject creditsScreenPanel;
    // more script references
    private GenerateObstacles obstacleGenerator;
    private CollectibleSpawner collectibleSpawner;

    // game states enum
    public enum GameState {SplashScreen, MainMenu, Game, GameOver, Help, Credits};

    public GameState gameState;
    public GameState prevGameState;

    // audio variables
    private AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip runMusic;
    public AudioClip gameOverMusic;
    public bool firstFrame;

    // Use this for initialization
    void Start ()
    {
        // set the game state
        gameState = GameState.SplashScreen;
        prevGameState = GameState.GameOver;
        helpScreenPanel.SetActive(false);
        creditsScreenPanel.SetActive(false);

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false; // start the player as not moving
        wolfSpawner = gameObject.GetComponent<WolfSpawner>();
        obstacleGenerator = gameObject.GetComponent<GenerateObstacles>();
        collectibleSpawner = gameObject.GetComponent<CollectibleSpawner>();
        audioSource = gameObject.GetComponent<AudioSource>();
        firstFrame = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        if(gameState == GameState.SplashScreen)
        {
            if(firstFrame && prevGameState == GameState.GameOver)
            {
                splashScreenPanel.SetActive(true);
                audioSource.clip = menuMusic;
                audioSource.Play();
                audioSource.loop = true;
                firstFrame = false;
            }
        }

        // only update timers while the game is running
        if(gameState == GameState.Game)
        {
            if(firstFrame)
            {
                audioSource.clip = runMusic;
                audioSource.Play();
                audioSource.loop = true;
                firstFrame = false;
            }

            // update the timer
            staminaTimer += Time.deltaTime;

            // updates the visual
            staminaRing.fillAmount = staminaTimer / maxStamina;

            // check for stamina
            if (staminaTimer >= maxStamina)
            {
                // clamp the value
                staminaTimer = maxStamina;

                // let the playerMovement script know
                playerMovement.outOfStamina = true;
            }

            // wolf spawning
            wolfTimer += Time.deltaTime;
            if (wolfTimer >= wolfSpawnTimer)
            {
                wolfSpawner.SpawnWolf();
                wolfTimer = 0.0f;
            }

            /*
            // bunny running out of stamina quicker for ramping difficulty - maybe implemented in the future
            balanceTimer += Time.deltaTime;
            if (balanceTimer >= 60.0f)
            {
                maxStamina -= 2.0f;
                balanceTimer = 0.0f;
            }
            */
        }

        if(gameState == GameState.GameOver)
        {
            if(firstFrame)
            {
                gameOverPanel.SetActive(true);
                audioSource.Stop(); // stop the run music first
                audioSource.PlayOneShot(gameOverMusic, 1.0f);
                audioSource.loop = false;
                firstFrame = false;
            }
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

    // method to end the game
    public void GameOver()
    {
        //List<GameObject> wolves = wolfSpawner.wolfList;
        
        foreach(GameObject wolf in wolfSpawner.wolfList)
        {
            // stop all of the wolves movement
            wolf.GetComponent<WolfMovement>().enabled = false;
            wolf.GetComponent<Rigidbody>().isKinematic = true;
        }
        //wolfSpawner.wolfList.Clear();
        // stop the player movement
        playerMovement.enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;

        // reset all necessary timers
        wolfTimer = 0.0f;

        // set the UI panel to be active
        gameOverPanel.SetActive(true);
    }

    // method to show the splash screen
    public void SplashScreen()
    {
        splashScreenPanel.SetActive(true);
        helpScreenPanel.SetActive(false);
        creditsScreenPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // stop the player movement
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        playerMovement.enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;
    }

    // method to show the help screen
    public void HelpScreen()
    {
        helpScreenPanel.SetActive(true);
        splashScreenPanel.SetActive(false);
    }

    // method to show the credits screen
    public void CreditsScreen()
    {
        creditsScreenPanel.SetActive(true);
        splashScreenPanel.SetActive(false);
    }
    // method to start the game
    public void GamePlayStart()
    {
        // disable the other screens
        splashScreenPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // reset the timers
        staminaTimer = 0.0f;
        wolfTimer = 0.0f;

        // reset scores
        featherScoreNum = 0;
        carrotScoreNum = 0;
        carrotScoreText.text = "Carrots: " + carrotScoreNum;
        featherScoreText.text = "Feathers: " + featherScoreNum;

        // reset the wolf and obstacle lists
        foreach (GameObject wolf in wolfSpawner.wolfList)
        {
            // stop all of the wolves movement
            Destroy(wolf);
        }
        wolfSpawner.wolfList.Clear();
        for (int i = 0; i < obstacleGenerator.Obstacles.Count; i++)
        {
            GameObject temp = obstacleGenerator.Obstacles[i];
            Destroy(temp);
            obstacleGenerator.Obstacles.Remove(temp);
        }
        for (int i = 0; i < collectibleSpawner.carrotCollectibles.Count; i++)
        {
            GameObject temp = collectibleSpawner.carrotCollectibles[i];
            Destroy(temp);
            collectibleSpawner.carrotCollectibles.Remove(temp);
        }

        for (int i = 0; i < collectibleSpawner.featherCollectibles.Count; i++)
        {
            GameObject temp = collectibleSpawner.featherCollectibles[i];
            Destroy(temp);
            collectibleSpawner.featherCollectibles.Remove(temp);
        }

        // reset the player variables
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        playerMovement.enabled = true;
        player.GetComponent<MeshRenderer>().enabled = true;
        playerMovement.outOfStamina = false;

        // spawn new objects
        obstacleGenerator.SpawnObstacles(); // this method calls the collectible spawner as well
    }
}
