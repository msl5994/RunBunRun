using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameManager gameManager;
    private PlayerMovement playerMovement;

    private int[] speedUpgradeCosts;
    private int[] turnUpgradeCosts;
    private int[] jumpUpgradeCosts;
    private float[] speedUpgrades;
    private float[] turnUpgrades;
    private float[] jumpUpgrades;

    public Text currentFeatherCount;
    public Text jumpCost;
    public Text turnCost;
    public Text speedCost;
    public Text jumpLv;
    public Text turnLv;
    public Text speedLv;

    // Start is called before the first frame update
    void Start()
    {
        speedUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        turnUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        jumpUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        speedUpgrades = new float[] { 28.0f, 31.0f, 34.0f, 37.0f, 40.0f, 43.0f, 46.0f, 49.0f };
        turnUpgrades = new float[] { 34.0f, 38.0f, 42.0f, 46.0f, 50.0f, 54.0f, 58.0f, 62.0f, 66.0f };
        jumpUpgrades = new float[] { 45.0f, 50.0f, 55.0f, 60.0f, 65.0f, 70.0f, 75.0f, 80.0f, 85.0f };

        // adding text to the shop screen
    }


    public void UpgradeSpeed()
    {
        gameManager.currentFeatherCount -= speedUpgradeCosts[gameManager.speedLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        playerMovement.speed = speedUpgrades[gameManager.speedLvl - 1];
        PlayerPrefs.SetFloat("CurrentSpeed", playerMovement.speed);
        gameManager.speedLvl++;

    }
    public void UpgradeTurn()
    {
        gameManager.currentFeatherCount -= turnUpgradeCosts[gameManager.turnLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        playerMovement.angleIncrement = turnUpgrades[gameManager.turnLvl - 1];
        PlayerPrefs.SetFloat("CurrentTurnRate", playerMovement.angleIncrement);
        gameManager.turnLvl++;
    }
    public void UpgradeJump()
    {
        gameManager.currentFeatherCount -= jumpUpgradeCosts[gameManager.jumpLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        playerMovement.jumpForce = jumpUpgrades[gameManager.jumpLvl - 1];
        PlayerPrefs.SetFloat("CurrentJumpHeight", playerMovement.jumpForce);
        gameManager.jumpLvl++;
    }
}
