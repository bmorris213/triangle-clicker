using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

// station manager is used by each station to control its function
public class StationManager : MonoBehaviour
{
    // station statistics
    public int baseCost;
    public int costIncreasePercent;
    public int level;
    public double baseEffectAmount;
    public string description;
    public bool effectsTPS;
    public bool multiplicative;

    private double currentCost;
    private double effectAmount;

    /* upgrade statistics
    public double milestoneRate;
    public int nextMilestone;
    private int upgradeCount;
    private bool canBuyNextUpgrade;
    */

    // public game objects
    public TMP_Text currentCostText;
    public TMP_Text levelText;
    public TMP_Text effectText;
    public TMP_Text descriptionText;
    public TMP_Text nextMilestoneText;

    /*
    public TMP_Text upgradeCostText;
    public Button upgradeButton;
    */

    // called once at startup
    void Start()
    {
        currentCost = baseCost;
        descriptionText.text = description + effectAmount.ToString();
        effectText.text = effectAmount.ToString();
        levelText.text = level.ToString();
        currentCostText.text = baseCost.ToString();
        effectAmount = baseEffectAmount;
        
        /* WIP upgrade values
        upgradeCount = 0;
        canBuyNextUpgrade = false;

        nextMilestoneText.text = nextMilestone.ToString();
        double upgradeCost = GetUpgradeCost();
        upgradeCostText.text = upgradeCost.ToString();
        */
    }

    // called anytime the user attempts to purchase a station level
    public void TryPurchase()
    {
        // verify user can afford purchase
        double currentTriangles = GameManager.Instance.triangleCount;

        if (currentCost > currentTriangles)
        {
            return;
        }

        // update current triangle balance
        currentTriangles -= currentCost;
        currentTriangles = Math.Round(currentTriangles, 2); // correct floating point errors
        GameManager.Instance.triangleCount = currentTriangles;
        GameManager.Instance.triangleCountText.text = "Triangles: " + currentTriangles.ToString();
        
        double effectResult = 0;

        // apply effect
        if (effectsTPS)
        {
            effectResult = GameManager.Instance.triangleProduction;
        }
        else
        {
            effectResult = GameManager.Instance.clickPower;
        }
        
        if (multiplicative)
        {
            effectResult *= effectAmount;
        }
        else
        {
            effectResult += effectAmount;
        }
        
        effectResult = Math.Round(effectResult, 2); // correct floating point errors
        
        if (effectsTPS)
        {
            GameManager.Instance.triangleProduction = effectResult;
            GameManager.Instance.triangleProductionText.text = "Triangles per second: " + effectResult.ToString();
        }
        else
        {
            GameManager.Instance.clickPower = effectResult;
            GameManager.Instance.clickPowerText.text = "Triangles per click: " + effectResult.ToString();
        }

        // update stats for station
        level += 1;
        levelText.text = level.ToString();

        double totalEffect = effectAmount * level;
        totalEffect = Math.Round(totalEffect, 2); // correct floating point errors
        effectText.text = totalEffect.ToString();

        currentCost = GetStationCost();
        currentCostText.text = currentCost.ToString();

        /* if we have reached a milestone, enable upgrade button
        if (!canBuyNextUpgrade && level >= nextMilestone)
        {
            canBuyNextUpgrade = true;
            upgradeButton.interactable = true;
        } */
    }

    // used to calculate costs for new station purchases
    private double GetStationCost()
    {
        double costIncreaseRate = 1 + (costIncreasePercent / 100);
        double costAmount = baseCost * Math.Pow(costIncreaseRate, level);
        return Math.Round(costAmount, 2);
    }

    /*  WIP upgrade math??

    // used to calculate cost for upgrade based on next milestone
    private double GetUpgradeCost()
    {
        
    }

    // called anytime the user attempts to purchase a station upgrade
    public void TryUpgrade()
    {
        // validate upgrade can be purchased
        if (!canBuyNextUpgrade)
        {
            // if you cannot, button is disabled and it should be impossible to reach this condition
            return;
        }

        // validate balance is enough
        double currentTriangles = GameManager.Instance.triangleCount;
        double upgradeCost = GetUpgradeCost();

        if (upgradeCost > currentTriangles)
        {
            return;
        }
        
        // update current triangle balance
        currentTriangles -= upgradeCost;
        currentTriangles = Math.Round(currentTriangles, 2); // correct floating point errors
        GameManager.Instance.triangleCount = currentTriangles;
        GameManager.Instance.triangleCountText.text = "Triangles: " + currentTriangles.ToString();

        // update effect
        effectAmount *= 2;
        effectAmount = Math.Round(effectAmount, 2); // correct floating point errors
        descriptionText.text = description + effectAmount.ToString();
        
        double totalEffect = effectAmount * level;
        totalEffect = Math.Round(totalEffect, 2); // correct floating point errors
        effectText.text = totalEffect.ToString();

        // apply updated effect
        if (effectsTPS)
        {
            GameManager.Instance.triangleProduction = totalEffect;
            GameManager.Instance.triangleProductionText.text = "Triangles per second: " + effectResult.ToString();
        }
        else
        {
            GameManager.Instance.clickPower = effectResult;
            GameManager.Instance.clickPowerText.text = "Triangles per click: " + effectResult.ToString();
        }
    } */
}
