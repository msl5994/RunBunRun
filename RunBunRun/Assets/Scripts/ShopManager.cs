using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameManager gameManager;
    private PlayerMovement playerMovement;
    public GameObject player;

    public int[] speedUpgradeCosts;
    public int[] turnUpgradeCosts;
    public int[] jumpUpgradeCosts;
    private float[] speedUpgrades;
    private float[] turnUpgrades;
    private float[] jumpUpgrades;
    private float[] jumpHeightUpgrades;

    public Text currentFeatherCountText;
    public Text jumpCostText;
    public Text turnCostText;
    public Text speedCostText;
    public Text jumpLvText;
    public Text turnLvText;
    public Text speedLvText;

    public Button upgradeJumpButton;
    public Button upgradeTurnButton;
    public Button upgradeSpeedButton;

    // Start is called before the first frame update
    void Start()
    {
        speedUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        turnUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        jumpUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        speedUpgrades = new float[] { 28.0f, 31.0f, 34.0f, 37.0f, 40.0f, 43.0f, 46.0f, 49.0f, 52.0f };
        turnUpgrades = new float[] { 34.0f, 38.0f, 42.0f, 46.0f, 50.0f, 54.0f, 58.0f, 62.0f, 66.0f };
        jumpUpgrades = new float[] { 45.0f, 50.0f, 55.0f, 60.0f, 65.0f, 70.0f, 75.0f, 80.0f, 85.0f };
        jumpHeightUpgrades = new float[] { 8.0f, 9.0f, 10.0f, 11.0f, 12.0f, 13.0f, 14.0f, 15.0f, 16.0f };

        playerMovement = player.GetComponent<PlayerMovement>();

        /*
        PlayerPrefs.SetFloat("CurrentSpeed", speedUpgrades[0] - 3.0f);
        PlayerPrefs.SetFloat("CurrentTurnRate", turnUpgrades[0] - 4.0f);
        PlayerPrefs.SetFloat("CurrentJumpHeight", jumpUpgrades[0] - 5.0f);
        */

        // adding text to the shop screen
        jumpCostText.text = "Jump Cost: " + jumpUpgradeCosts[gameManager.jumpLvl - 1];
        turnCostText.text = "Turn Cost: " + turnUpgradeCosts[gameManager.turnLvl - 1];
        speedCostText.text = "Speed Cost: " + speedUpgradeCosts[gameManager.speedLvl - 1];
        jumpLvText.text = "Jump Lv: " + gameManager.jumpLvl;
        turnLvText.text = "Turn Lv: " + gameManager.turnLvl;
        speedLvText.text = "Speed Lv: " + gameManager.speedLvl;
    }

    public void CanAffordUpgrades()
    {
        if (gameManager.currentFeatherCount < jumpUpgradeCosts[gameManager.jumpLvl - 1])
        {
            upgradeJumpButton.enabled = false;
        }
        else
        {
            upgradeJumpButton.enabled = true;
        }
        if (gameManager.currentFeatherCount < turnUpgradeCosts[gameManager.turnLvl - 1])
        {
            upgradeTurnButton.enabled = false;
        }
        else
        {
            upgradeTurnButton.enabled = true;
        }
        if (gameManager.currentFeatherCount < speedUpgradeCosts[gameManager.speedLvl - 1])
        {
            upgradeSpeedButton.enabled = false;
        }
        else
        {
            upgradeSpeedButton.enabled = true;
        }
    }


    public void UpgradeSpeed()
    {
        gameManager.currentFeatherCount -= speedUpgradeCosts[gameManager.speedLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.currentSpeed = speedUpgrades[gameManager.speedLvl - 1];
        PlayerPrefs.SetFloat("CurrentSpeed", playerMovement.speed);
        gameManager.speedLvl++;

        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;
        speedCostText.text = "Speed Cost: " + speedUpgradeCosts[gameManager.speedLvl - 1];

        if (gameManager.speedLvl == speedUpgrades.Length)
        {
            speedLvText.text = "Speed Lv: MAX";
            Destroy(upgradeSpeedButton);
            Destroy(speedCostText);
            //upgradeSpeedButton.enabled = false;
        }
        else
        {
            speedLvText.text = "Speed Lv: " + gameManager.speedLvl;
        }

        CanAffordUpgrades();
    }
    public void UpgradeTurn()
    {
        gameManager.currentFeatherCount -= turnUpgradeCosts[gameManager.turnLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.currentTurnRate = turnUpgrades[gameManager.turnLvl - 1];
        PlayerPrefs.SetFloat("CurrentTurnRate", playerMovement.angleIncrement);
        gameManager.turnLvl++;

        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;
        turnCostText.text = "Turn Cost: " + turnUpgradeCosts[gameManager.turnLvl - 1];

        if (gameManager.turnLvl == turnUpgrades.Length)
        {
            turnLvText.text = "Turn Lv: MAX";
            Destroy(upgradeTurnButton);
            Destroy(turnCostText);
            //upgradeTurnButton.enabled = false;
        }
        else
        {
            turnLvText.text = "Turn Lv: " + gameManager.turnLvl;
        }

        CanAffordUpgrades();
    }
    public void UpgradeJump()
    {
        gameManager.currentFeatherCount -= jumpUpgradeCosts[gameManager.jumpLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.currentJumpHeight = jumpUpgrades[gameManager.jumpLvl - 1];
        playerMovement.maxJumpHeight = jumpHeightUpgrades[gameManager.jumpLvl - 1];
        PlayerPrefs.SetFloat("CurrentJumpHeight", playerMovement.jumpForce);
        PlayerPrefs.SetFloat("MaxJumpHeight", playerMovement.maxJumpHeight);
        gameManager.jumpLvl++;

        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;
        jumpCostText.text = "Jump Cost: " + jumpUpgradeCosts[gameManager.jumpLvl - 1];

        if(gameManager.jumpLvl == jumpUpgrades.Length)
        {
            jumpLvText.text = "Jump Lv: MAX";
            Destroy(upgradeJumpButton);
            Destroy(jumpCostText);
            //upgradeJumpButton.enabled = false;
        }
        else
        {
            jumpLvText.text = "Jump Lv: " + gameManager.jumpLvl;
        }
        CanAffordUpgrades();
    }
    
}
