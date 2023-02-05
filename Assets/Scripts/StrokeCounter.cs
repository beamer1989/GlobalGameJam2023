using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        print("GAME OVER");
    }
}
