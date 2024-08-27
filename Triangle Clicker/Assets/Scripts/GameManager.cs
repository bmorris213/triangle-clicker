using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using TMPro;
using System;

// game manager keeps track of all the stats and resources used in the game
// by delegating to various other managers
public class GameManager : MonoBehaviour
{
    // player resources
    public double triangleCount;
    public double triangleProduction;
    public double clickPower;

    // public game objects
    public TMP_Text triangleCountText;
    public TMP_Text triangleProductionText;
    public TMP_Text clickPowerText;

    // called once at startup
    void Start()
    {
        // begin production function on 1 second timer
        InvokeRepeating("AddProduction", 1f, 1f);
    }

    // called once each time program is given focus
    void Awake()
    {
        // instanciate game manager
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    // add production is the second to second production function
    public void AddProduction()
    {
        triangleCount += triangleProduction;
        triangleCount = Math.Round(triangleCount, 2); // correct floating point errors
        triangleCountText.text = "Triangles: " + triangleCount;
    }

    // triangle click is used whenever the player clicks the big triangle
    public void TriangleClick()
    {
        triangleCount += clickPower;
        triangleCount = Math.Round(triangleCount, 2); // correct floating point errors
        triangleCountText.text = "Triangles: " + triangleCount;
    }

    // instance of game manager
    public static GameManager Instance { get; private set; }
}