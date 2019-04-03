using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    private GameManager gameManager;
    private ShopManager shopManager;
	// Use this for initialization
	void Start ()
    {
        gameManager = gameObject.GetComponent<GameManager>();
        shopManager = gameObject.GetComponent<ShopManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // method to load the gameplay scene
    public void StartGame()
    {
        gameManager.firstFrame = true;
        gameManager.GamePlayStart();
        gameManager.gameState = GameManager.GameState.Game;
        gameManager.prevGameState = GameManager.GameState.SplashScreen;
        //SceneManager.LoadScene("SampleScene");
    }

    // method to load the splashScreen/mainScreen
    public void LoadSplashScreen()
    {
        gameManager.firstFrame = true;
        gameManager.gameState = GameManager.GameState.SplashScreen;
        gameManager.SplashScreen();
        //SceneManager.LoadScene("SplashScreen");
    }

    // method to load the help screen
    public void LoadHelpScreen()
    {
        gameManager.gameState = GameManager.GameState.Help;
        gameManager.prevGameState = GameManager.GameState.SplashScreen;
        gameManager.HelpScreen();
    }

    // method to load the credits screen
    public void LoadCreditsScreen()
    {
        gameManager.gameState = GameManager.GameState.Credits;
        gameManager.prevGameState = GameManager.GameState.SplashScreen;
        gameManager.CreditsScreen();
    }

    // call this method in addition to the LoadSplashScreen method to reset the music properly
    public void OnGameOverMainMenuPress()
    {
        gameManager.prevGameState = GameManager.GameState.GameOver;
    }

    // method to load the options screen
    public void LoadOptionsScreen()
    {
        gameManager.gameState = GameManager.GameState.Options;
        gameManager.prevGameState = GameManager.GameState.SplashScreen;
        gameManager.OptionsScreen();
    }

    // method to load the options screen
    public void LoadShopScreen()
    {
        gameManager.gameState = GameManager.GameState.Shop;
        gameManager.prevGameState = GameManager.GameState.SplashScreen;
        gameManager.ShopScreen();
    }

    // method to mute the volume sliders
    public void MuteAll()
    {
        gameManager.musicSlider.value = 0;
        gameManager.sfxSlider.value = 0;
    }

    // methods that will call shopManager's upgrade methods
    public void UpgradeJump()
    {
        shopManager.UpgradeJump();
    }
    public void UpgradeTurn()
    {
        shopManager.UpgradeTurn();
    }
    public void UpgradeSpeed()
    {
        shopManager.UpgradeSpeed();
    }
}
