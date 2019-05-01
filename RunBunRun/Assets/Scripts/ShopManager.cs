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
    public int[] staminaUpgradeCosts;
    private float[] speedUpgrades;
    private float[] turnUpgrades;
    private float[] jumpUpgrades;
    private float[] jumpTimerUpgrades;
    private float[] staminaUpgrades;

    public Text currentFeatherCountText;
    public Text jumpCostText;
    public Text turnCostText;
    public Text speedCostText;
    public Text staminaCostText;
    public Text jumpLvText;
    public Text turnLvText;
    public Text speedLvText;
    public Text staminaLvText;

    public Button upgradeJumpButton;
    public Button upgradeTurnButton;
    public Button upgradeSpeedButton;
    public Button upgradeStaminaButton;

    // Start is called before the first frame update
    void Start()
    {
        speedUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        turnUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        jumpUpgradeCosts = new int[] { 3, 5, 7, 9, 11, 13, 15, 17, 19 };
        staminaUpgradeCosts = new int[] { 3, 7, 11, 15, 19 };
        speedUpgrades = new float[] { 28.0f, 31.0f, 34.0f, 37.0f, 40.0f, 43.0f, 46.0f, 49.0f, 52.0f };
        turnUpgrades = new float[] { 34.0f, 38.0f, 42.0f, 46.0f, 50.0f, 54.0f, 58.0f, 62.0f, 66.0f };
        jumpUpgrades = new float[] { 45.0f, 50.0f, 55.0f, 60.0f, 65.0f, 70.0f, 75.0f, 80.0f, 85.0f };
        staminaUpgrades = new float[] { 66.0f, 72.0f, 78.0f, 84.0f, 90.0f };
        //jumpTimerUpgrades = new float[] { 0.7f, 0.9f, 1.1f, 1.3f, 1.5f, 1.7f, 1.9f, 2.1f, 2.3f };

        playerMovement = player.GetComponent<PlayerMovement>();

        /*
        PlayerPrefs.SetFloat("CurrentSpeed", speedUpgrades[0] - 3.0f);
        PlayerPrefs.SetFloat("CurrentTurnRate", turnUpgrades[0] - 4.0f);
        PlayerPrefs.SetFloat("CurrentJumpForce", jumpUpgrades[0] - 5.0f);

        */   
        
        jumpLvText.text = "Jump Lv: " + gameManager.jumpLvl;
        turnLvText.text = "Turn Lv: " + gameManager.turnLvl;
        speedLvText.text = "Speed Lv: " + gameManager.speedLvl;
        staminaLvText.text = "Stamina Lv: " + gameManager.staminaLvl;
        CheckMaxStats();
    }

    public void CanAffordUpgrades()
    {
        if(gameManager.jumpLvl - 1 != jumpUpgrades.Length)
        {
            if (gameManager.currentFeatherCount < jumpUpgradeCosts[gameManager.jumpLvl - 1])
            {
                upgradeJumpButton.enabled = false;
            }
            else
            {
                upgradeJumpButton.enabled = true;
            }
        }
        if(gameManager.turnLvl - 1 != turnUpgrades.Length)
        {
            if (gameManager.currentFeatherCount < turnUpgradeCosts[gameManager.turnLvl - 1])
            {
                upgradeTurnButton.enabled = false;
            }
            else
            {
                upgradeTurnButton.enabled = true;
            }
        }
        if(gameManager.speedLvl - 1 != speedUpgrades.Length)
        {
            if (gameManager.currentFeatherCount < speedUpgradeCosts[gameManager.speedLvl - 1])
            {
                upgradeSpeedButton.enabled = false;
            }
            else
            {
                upgradeSpeedButton.enabled = true;
            }
        }
        if(gameManager.staminaLvl - 1 != staminaUpgrades.Length)
        {
            if (gameManager.currentFeatherCount < staminaUpgradeCosts[gameManager.staminaLvl - 1])
            {
                upgradeStaminaButton.enabled = false;
            }
            else
            {
                upgradeStaminaButton.enabled = true;
            }
        }
        
    }

    // adding text to the shop screen
    private void CheckMaxStats()
    {
        if (gameManager.speedLvl - 1 == speedUpgrades.Length && !upgradeSpeedButton.IsDestroyed())
        {
            Destroy(upgradeSpeedButton.gameObject);
            Destroy(speedCostText);
            speedLvText.text = "Speed Lv: MAX";
        }
        else
        {
            speedCostText.text = "Speed Cost: " + speedUpgradeCosts[gameManager.speedLvl - 1];
        }

        if (gameManager.turnLvl - 1 == turnUpgrades.Length && !upgradeTurnButton.IsDestroyed())
        {
            Destroy(upgradeTurnButton.gameObject);
            Destroy(turnCostText);
            turnLvText.text = "Turn Lv: MAX";
        }
        else
        {
            turnCostText.text = "Turn Cost: " + turnUpgradeCosts[gameManager.turnLvl - 1];
        }

        if (gameManager.jumpLvl - 1 == jumpUpgrades.Length && !upgradeJumpButton.IsDestroyed())
        {
            Destroy(upgradeJumpButton.gameObject);
            Destroy(jumpCostText);
            jumpLvText.text = "Jump Lv: MAX";
        }
        else
        {
            jumpCostText.text = "Jump Cost: " + jumpUpgradeCosts[gameManager.jumpLvl - 1];
        }

        if (gameManager.staminaLvl - 1 == staminaUpgrades.Length && !upgradeStaminaButton.IsDestroyed())
        {
            Destroy(upgradeStaminaButton.gameObject);
            Destroy(staminaCostText);
            staminaLvText.text = "Stamina Lv: MAX";
        }
        else
        {
            staminaCostText.text = "Stamina Cost: " + staminaUpgradeCosts[gameManager.staminaLvl - 1];
        }
    }

    public void UpgradeSpeed()
    {
        gameManager.currentFeatherCount -= speedUpgradeCosts[gameManager.speedLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.currentSpeed = speedUpgrades[gameManager.speedLvl - 1];
        PlayerPrefs.SetFloat("CurrentSpeed", gameManager.currentSpeed);
        gameManager.speedLvl++;
        PlayerPrefs.SetInt("SpeedLvl", gameManager.speedLvl);
        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;

        if ((gameManager.speedLvl - 1) == speedUpgrades.Length)
        {
            speedLvText.text = "Speed Lv: MAX";
            Destroy(upgradeSpeedButton.gameObject);
            Destroy(speedCostText);
           // Debug.Log("Active: " + upgradeSpeedButton.IsActive());
        }
        else
        {
            speedLvText.text = "Speed Lv: " + gameManager.speedLvl;
            speedCostText.text = "Speed Cost: " + speedUpgradeCosts[gameManager.speedLvl - 1];
        }

        CanAffordUpgrades();
    }
    public void UpgradeTurn()
    {
        gameManager.currentFeatherCount -= turnUpgradeCosts[gameManager.turnLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.currentTurnRate = turnUpgrades[gameManager.turnLvl - 1];
        PlayerPrefs.SetFloat("CurrentTurnRate", gameManager.currentTurnRate);
        gameManager.turnLvl++;
        PlayerPrefs.SetInt("TurnLvl", gameManager.turnLvl);

        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;
        

        if ((gameManager.turnLvl - 1) == turnUpgrades.Length)
        {
            turnLvText.text = "Turn Lv: MAX";
            Destroy(upgradeTurnButton.gameObject);
            Destroy(turnCostText);
            //upgradeTurnButton.enabled = false;
        }
        else
        {
            turnLvText.text = "Turn Lv: " + gameManager.turnLvl;
            turnCostText.text = "Turn Cost: " + turnUpgradeCosts[gameManager.turnLvl - 1];
        }

        CanAffordUpgrades();
    }
    public void UpgradeJump()
    {
        gameManager.currentFeatherCount -= jumpUpgradeCosts[gameManager.jumpLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.currentJumpForce = jumpUpgrades[gameManager.jumpLvl - 1];
        //playerMovement.maxJumpTimer = jumpTimerUpgrades[gameManager.jumpLvl - 1];
        PlayerPrefs.SetFloat("CurrentJumpForce", gameManager.currentJumpForce);
        //PlayerPrefs.SetFloat("MaxJumpTimer", playerMovement.maxJumpTimer);
        gameManager.jumpLvl++;
        PlayerPrefs.SetInt("JumpLvl", gameManager.jumpLvl);

        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;
        

        if ((gameManager.jumpLvl - 1) == jumpUpgrades.Length)
        {
            jumpLvText.text = "Jump Lv: MAX";
            Destroy(upgradeJumpButton.gameObject);
            Destroy(jumpCostText);
        }
        else
        {
            jumpLvText.text = "Jump Lv: " + gameManager.jumpLvl;
            jumpCostText.text = "Jump Cost: " + jumpUpgradeCosts[gameManager.jumpLvl - 1];
        }
        CanAffordUpgrades();
    }

    public void UpgradeStamina()
    {
        gameManager.currentFeatherCount -= staminaUpgradeCosts[gameManager.staminaLvl - 1];
        PlayerPrefs.SetInt("Feathers", gameManager.currentFeatherCount);
        gameManager.maxStamina = staminaUpgrades[gameManager.staminaLvl - 1];
        PlayerPrefs.SetFloat("MaxStamina", gameManager.maxStamina);
        gameManager.staminaLvl++;
        PlayerPrefs.SetInt("StaminaLvl", gameManager.staminaLvl);

        currentFeatherCountText.text = "Feathers: " + gameManager.currentFeatherCount;
        
        if ((gameManager.staminaLvl - 1) == staminaUpgrades.Length)
        {
            staminaLvText.text = "Stamina Lv: MAX";
            Destroy(upgradeStaminaButton.gameObject);
            Destroy(staminaCostText);
        }
        else
        {
            staminaLvText.text = "Stamina Lv: " + gameManager.staminaLvl;
            staminaCostText.text = "Stamina Cost: " + staminaUpgradeCosts[gameManager.staminaLvl - 1];
        }

        CanAffordUpgrades();
    }
    
}
