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
    public double effectAmount;
    public string description;
    public bool effectsTPS;
    public bool multiplicative;

    private double currentCost;

    // public game objects
    public TMP_Text currentCostText;
    public TMP_Text levelText;
    public TMP_Text effectText;
    public TMP_Text descriptionText;

    // called once at startup
    void Start()
    {
        currentCost = baseCost;
        descriptionText.text = description;
        effectText.text = effectAmount.ToString();
        levelText.text = level.ToString();
        currentCostText.text = baseCost.ToString();
    }

    // called anytime the user attempts to purchase a station level
    public void TryUpgrade()
    {
        // verify user can afford upgrade
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
            GameManager.Instance.triangleProductionText.text = "Triangles per second: " + effectResult.ToString();
        }
        else
        {
            GameManager.Instance.clickPowerText.text = "Triangles per click: " + effectResult.ToString();
        }

        // update stats for station
        level += 1;
        levelText.text = level.ToString();

        double totalEffect = effectAmount * level;
        totalEffect = Math.Round(totalEffect, 2); // correct floating point errors
        effectText.text = totalEffect.ToString();

        currentCost = getCost();
        currentCostText.text = currentCost.ToString();
    }

    // used to calculate costs for new station purchases
    private double getCost()
    {
        double costIncreaseRate = 1 + (costIncreasePercent / 100);
        double costAmount = baseCost * Math.Pow(costIncreaseRate, level);
        return Math.Round(costAmount, 2);
    }
}
