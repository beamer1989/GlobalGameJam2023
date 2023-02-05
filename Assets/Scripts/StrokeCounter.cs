using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StrokeCounter : MonoBehaviour
{

    static public int strokesAvailable;
    public int startingStrokes = 10;

    void Awake()
    {
        strokesAvailable = startingStrokes;
    }

    static public void ReduceStrokes()
    {
        strokesAvailable -= 1;
        print(strokesAvailable);
        if (strokesAvailable == 0)
        {
            GameOver();
        }
    }
    
    static public void IncreaseStrokes()
    {
        strokesAvailable += 10;
        print(strokesAvailable);
    }

    static public void GameOver()
    {
        SceneManager.LoadScene(3); // hardcoded scene id for now
    }
}
