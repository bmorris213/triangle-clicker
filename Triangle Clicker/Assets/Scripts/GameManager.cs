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
    public double triangleCount = 0;
    public double triangleProduction = 0;
    public double clickPower = 0.1;

    // public game objects
    public TMP_Text triangleCountText;
    public TMP_Text triangleProductionText;
    public TMP_Text clickPowerText;

    public void TriangleClick()
    {
        triangleCount += clickPower;
        triangleCount = Math.Round(triangleCount, 2);
        triangleCountText.text = "Triangles: " + triangleCount;
    }
}