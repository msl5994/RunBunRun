using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonManager : MonoBehaviour {

    private GameManager gameManager;

	// Use this for initialization
	void Start ()
    {
        gameManager = gameObject.GetComponent<GameManager>();
	}
	
	// Update is called once per frame
	void Update ()
    {
		
	}

    // method to load the gameplay scene
    public void StartGame()
    {
        gameManager.GamePlayStart();
        gameManager.gameState = GameManager.GameState.Game;
        //SceneManager.LoadScene("SampleScene");
    }

    // method to load the splashScreen/mainScreen
    public void LoadSplashScreen()
    {
        gameManager.gameState = GameManager.GameState.SplashScreen;
        gameManager.SplashScreen();
        //SceneManager.LoadScene("SplashScreen");
    }
}
