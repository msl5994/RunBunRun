using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour {

    public Text scoreText;
    public Text carrotScoreText;
    public Text featherScoreText;
    public Text finalScoreText;
    public Text newHighScoreText;
    public Text highScoreText;

    public float score = 0;
    public int carrotScoreNum = 0;
    public int featherScoreNum = 0;

    public GameObject gameManager;

	// Use this for initialization
	void Start ()
    {
        // reset scores
        featherScoreNum = 0;
        carrotScoreNum = 0;
        carrotScoreText.text = "Carrots: " + carrotScoreNum;
        featherScoreText.text = "Feathers: " + featherScoreNum;
        gameManager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update ()
    {
		if(gameManager.GetComponent<GameManager>().gameState == GameManager.GameState.Game)
        {
            UpdateScore();
        }
        
	}

    public void UpdateScore()
    {
        score += gameManager.GetComponent<GameManager>().currentSpeed * Time.deltaTime;
        scoreText.text = "Score: " + Mathf.RoundToInt(score);
    }

    // method to update the carrot score
    public void UpdateCarrotScore()
    {
        carrotScoreNum++;
        carrotScoreText.text = "Carrots: " + carrotScoreNum;
        // adding to player score as well
        score += 500;
    }

    // method to update the score
    public void UpdateFeatherScore()
    {
        featherScoreNum++;
        gameManager.GetComponent<GameManager>().currentFeatherCount++;
        featherScoreText.text = "Feathers: " + featherScoreNum;
        // adding to player score as well
        score += 500;
    }
}
