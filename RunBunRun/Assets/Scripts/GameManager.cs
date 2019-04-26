using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Image staminaRing;
    
    
    public float maxStamina = 60.0f; // 1 minute for now
    public float staminaTimer = 0.0f;
    public float wolfTimer = 0.0f;
    public float wolfSpawnTimer = 30.0f;
    private float balanceTimer = 0.0f;

    // player pref data variables
    public int currentFeatherCount = 0;
    public float currentSpeed = 0.0f;
    public float currentTurnRate = 0.0f;
    public float currentJumpForce = 0.0f;
    public float currentMaxJumpTimer = 0.5f;

    public GameObject player;
    public GameObject arrow;
    public GameObject minimapArrow;
    private PlayerMovement playerMovement;
    private WolfSpawner wolfSpawner;
    private SquirrelSpawner squirrelSpawner;
    private ScoreManager scoreManager;

    public bool gameOver = false;

    public GameObject gameOverPanel;
    public GameObject splashScreenPanel;
    public GameObject helpScreenPanel;
    public GameObject creditsScreenPanel;
    public GameObject optionsPanel;
    public GameObject shopPanel;
    // more script references
    private GenerateObstacles obstacleGenerator;
    private CollectibleSpawner collectibleSpawner;

    // game states enum
    public enum GameState {SplashScreen, MainMenu, Game, GameOver, Help, Credits, Options, Shop};

    public GameState gameState;
    public GameState prevGameState;

    // audio variables
    private AudioSource audioSource;
    public AudioClip menuMusic;
    public AudioClip runMusic;
    public AudioClip gameOverMusic;
    public Slider musicSlider;
    public Slider sfxSlider;
    public bool firstFrame;

    public int speedLvl;
    public int turnLvl;
    public int jumpLvl;

    // check for the data as the app loads up
    private void Awake()
    {
        //PlayerPrefs.SetInt("Feathers", 0);
        if (PlayerPrefs.GetInt("Feathers") > 0)
        {
            currentFeatherCount = PlayerPrefs.GetInt("Feathers"); // kept result of 3 after a test run
        }
        else
        {
            currentFeatherCount = 0;
        }
        if(PlayerPrefs.GetFloat("CurrentSpeed") > 25.0f)
        {
            currentSpeed = PlayerPrefs.GetFloat("CurrentSpeed");
        }
        else
        {
            currentSpeed = 25.0f; // playerMovement.speed;
        }
        if(PlayerPrefs.GetFloat("CurrentTurnRate") > 30.0f)
        {
            currentTurnRate = PlayerPrefs.GetFloat("CurrentTurnRate");
        }
        else
        {
            currentTurnRate = 30.0f;
        }
        if(PlayerPrefs.GetFloat("CurrentJumpForce") > 40.0f)
        {
            currentJumpForce = PlayerPrefs.GetFloat("CurrentJumpForce");
        }
        else
        {
            currentJumpForce = 40.0f;
        }
        if(PlayerPrefs.GetInt("SpeedLvl") > 1)
        {
            speedLvl = PlayerPrefs.GetInt("SpeedLvl");
        }
        else
        {
            speedLvl = 1;
        }
        if (PlayerPrefs.GetInt("TurnLvl") > 1)
        {
            turnLvl = PlayerPrefs.GetInt("TurnLvl");
        }
        else
        {
            turnLvl = 1;
        }
        if (PlayerPrefs.GetInt("JumpLvl") > 1)
        {
            jumpLvl = PlayerPrefs.GetInt("JumpLvl");
        }
        else
        {
            jumpLvl = 1;
        }

        if(!(PlayerPrefs.GetInt("HighScore") > 0))
        {
            PlayerPrefs.SetInt("HighScore", 0);
        }
        

        Debug.Log("Jump: " + currentJumpForce + " Turn: " + currentTurnRate + " Speed: " + currentSpeed + " JumpTimer: " + currentMaxJumpTimer);
    }

    // Use this for initialization
    void Start ()
    {
        // set the game state
        gameState = GameState.SplashScreen;
        prevGameState = GameState.GameOver;
        helpScreenPanel.SetActive(false);
        creditsScreenPanel.SetActive(false);
        optionsPanel.SetActive(false);
        shopPanel.SetActive(false);

        playerMovement = player.GetComponent<PlayerMovement>();
        playerMovement.enabled = false; // start the player as not moving
        wolfSpawner = gameObject.GetComponent<WolfSpawner>();
        squirrelSpawner = gameObject.GetComponent<SquirrelSpawner>();
        obstacleGenerator = gameObject.GetComponent<GenerateObstacles>();
        collectibleSpawner = gameObject.GetComponent<CollectibleSpawner>();
        audioSource = gameObject.GetComponent<AudioSource>();
        scoreManager = gameObject.GetComponent<ScoreManager>();
        firstFrame = true;
	}
	
	// Update is called once per frame
	void Update ()
    {
        audioSource.volume = musicSlider.value;
        if (gameState == GameState.SplashScreen)
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
            if (wolfTimer >= wolfSpawnTimer && wolfSpawner.wolfList.Count < 10)
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
            if(collectibleSpawner.featherCollectibles.Count > 0)
            {
                foreach (GameObject feather in collectibleSpawner.featherCollectibles)
                {
                    feather.transform.LookAt(player.transform.position);
                }
            }
            GameObject nearestCarrot = FindNearestCarrot();
            arrow.transform.LookAt(nearestCarrot.transform.position);
            minimapArrow.transform.LookAt(new Vector3(nearestCarrot.transform.position.x, minimapArrow.transform.position.y, nearestCarrot.transform.position.z));
        }

        if(gameState == GameState.GameOver)
        {
            if(firstFrame)
            {
                gameOverPanel.SetActive(true);
                audioSource.Stop(); // stop the run music first
                audioSource.PlayOneShot(gameOverMusic, musicSlider.value);
                audioSource.loop = false;
                firstFrame = false;
            }
        }
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
        // save data to the player prefs
        PlayerPrefs.SetInt("Feathers",currentFeatherCount);
        //PlayerPrefs.SetFloat("CurrentSpeed",currentMaxSpeed);
        //PlayerPrefs.SetFloat("CurrentTurnRate", currentTurnSpeed);
        //PlayerPrefs.SetFloat("CurrentJumpHeight", player.JumpHeight);
        PlayerPrefs.Save();

        // destory and reset collectible and wolf lists
        foreach (GameObject wolf in wolfSpawner.wolfList)
        {
            // stop all of the wolves movement
            wolf.GetComponent<WolfMovement>().enabled = false;
            wolf.GetComponent<Rigidbody>().isKinematic = true;
            Destroy(wolf);
        }

        foreach (GameObject squirrel in squirrelSpawner.squirrelList)
        {
            Destroy(squirrel);
        }
        squirrelSpawner.squirrelList.Clear();

        foreach (GameObject feather in collectibleSpawner.featherCollectibles)
        {
            Destroy(feather);
        }

        foreach (GameObject carrot in collectibleSpawner.carrotCollectibles)
        {
            Destroy(carrot);
        }

        if (scoreManager.score <= PlayerPrefs.GetInt("HighScore"))
        {
            scoreManager.newHighScoreText.text = "";
        }
        else
        {
            scoreManager.newHighScoreText.text = "NEW HIGH SCORE!!!";
            PlayerPrefs.SetInt("HighScore", Mathf.RoundToInt(scoreManager.score));
        }

        scoreManager.finalScoreText.text = "Final Score: " + Mathf.RoundToInt(scoreManager.score);
        scoreManager.highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore");
        //wolfSpawner.wolfList.Clear();
        // stop the player movement
        playerMovement.isJumping = false;
        playerMovement.isGrounded = true;
        playerMovement.currentJumpTimer = 0.0f;
        playerMovement.enabled = false;
        player.GetComponent<Animator>().enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;
        scoreManager.score = 0;
        scoreManager.featherScoreNum = 0;
        scoreManager.carrotScoreNum = 0;

        // reset all necessary timers
        wolfTimer = 0.0f;

        
        // set the UI panel to be active
        gameOverPanel.SetActive(true);

        // reset audio to normal speed
        audioSource.pitch = 1.0f;
    }

    // method to show the splash screen
    public void SplashScreen()
    {
        splashScreenPanel.SetActive(true);
        helpScreenPanel.SetActive(false);
        creditsScreenPanel.SetActive(false);
        gameOverPanel.SetActive(false);
        optionsPanel.SetActive(false);
        shopPanel.SetActive(false);

        // stop the player movement
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        playerMovement.enabled = false;
        player.GetComponent<MeshRenderer>().enabled = false;

        // reset audio to normal speed
        audioSource.pitch = 1.0f;
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

    // method to show the options screen
    public void OptionsScreen()
    {
        optionsPanel.SetActive(true);
        splashScreenPanel.SetActive(false);
    }

    // method to show the shop
    public void ShopScreen()
    {
        shopPanel.SetActive(true);
        splashScreenPanel.SetActive(false);
        this.GetComponent<ShopManager>().currentFeatherCountText.text = "Feathers: " + currentFeatherCount;
    }
    // method to start the game
    public void GamePlayStart()
    {
        // disable the other screens
        splashScreenPanel.SetActive(false);
        gameOverPanel.SetActive(false);

        // set the player's movement parameters to match the saved settings
        playerMovement.speed = currentSpeed;
        playerMovement.angleIncrement = currentTurnRate;
        playerMovement.jumpForce = currentJumpForce;

        // increase the run animation's speed to match
        playerMovement.anim.speed = 4.0f;

        // reset the timers
        staminaTimer = 0.0f;
        wolfTimer = 0.0f;

        // reset the wolf and squirrel and obstacle lists
        foreach (GameObject wolf in wolfSpawner.wolfList)
        {
            // stop all of the wolves movement
            Destroy(wolf);
        }
        wolfSpawner.wolfList.Clear();
        for (int i = 0; i < obstacleGenerator.LargeObstacles.Count; i++)
        {
            GameObject temp = obstacleGenerator.LargeObstacles[i];
            Destroy(temp);
            obstacleGenerator.LargeObstacles.Remove(temp);
        }
        for (int i = 0; i < obstacleGenerator.MediumObstacles.Count; i++)
        {
            GameObject temp = obstacleGenerator.MediumObstacles[i];
            Destroy(temp);
            obstacleGenerator.MediumObstacles.Remove(temp);
        }
        for (int i = 0; i < obstacleGenerator.SmallObstacles.Count; i++)
        {
            GameObject temp = obstacleGenerator.SmallObstacles[i];
            Destroy(temp);
            obstacleGenerator.SmallObstacles.Remove(temp);
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
        /*
        foreach (GameObject squirrel in squirrelSpawner.squirrelList)
        {
            Destroy(squirrel);
        }*/
        

        // reset the player variables
        player.transform.position = new Vector3(0.0f, 1.0f, 0.0f);
        playerMovement.enabled = true;
        player.GetComponent<MeshRenderer>().enabled = true;
        player.GetComponent<Animator>().enabled = true;
        playerMovement.outOfStamina = false;
        

        // spawn new objects
        obstacleGenerator.SpawnObstacles(); // this method calls the collectible spawner as well

        // spawn new squirrels
        for(int i = 1; i <= squirrelSpawner.numToSpawn; i++)
        {
            squirrelSpawner.SpawnSquirrel();
        }

        scoreManager.carrotScoreText.text = "Carrots: " + scoreManager.carrotScoreNum;
        scoreManager.featherScoreText.text = "Feathers: " + scoreManager.featherScoreNum;
        
    }

    private GameObject FindNearestCarrot()
    {
        float shortestDistance = 10000000000f;
        GameObject nearestCarrot = null;
        foreach (GameObject carrot in collectibleSpawner.carrotCollectibles)
        {
            if(Vector3.Distance(player.transform.position, carrot.transform.position) < shortestDistance)
            {
                shortestDistance = Vector3.Distance(player.transform.position, carrot.transform.position);
                nearestCarrot = carrot;
            }
        }
        return nearestCarrot;
    }
}
