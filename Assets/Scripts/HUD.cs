using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("ShotCounter")]
    [SerializeField] private Image[] lifePoint;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 15; i++)
        {
            if (i >= StrokeCounter.strokesAvailable - 1)
                lifePoint[i].enabled = false;
            else
                lifePoint[i].enabled = true;
        }
        //gameObject.SetActive = false;
        //shotCounter.text = StrokeCounter.strokesAvailable.ToString();
    }
}
