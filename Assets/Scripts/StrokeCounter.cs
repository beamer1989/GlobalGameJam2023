using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StrokeCounter : MonoBehaviour
{

    static public int strokesAvailable;
    public int startingStrokes = 10;
    public static bool tutorialMode = false;

    void Awake()
    {
        strokesAvailable = startingStrokes;
    }

    static public void ReduceStrokes()
    {
        strokesAvailable -= 1;
        strokesAvailable = strokesAvailable < 0 ? 0 : strokesAvailable; //prevent going below zero (for tutorial mode)
        print(strokesAvailable);
        if (strokesAvailable == 0)
        {
            GameOver();
        }
    }
    
    static public void IncreaseStrokes()
    {
        strokesAvailable += 10;
        strokesAvailable = strokesAvailable > 15 ? 15 : strokesAvailable; // prevent going above 15
        print(strokesAvailable);
    }

    static public void GameOver()
    {
        if (!tutorialMode)
            SceneManager.LoadScene(3); // hardcoded scene id for now
    }
}
