using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour
{
    [Header("ShotCounter")]
    [SerializeField] private Text shotCounter;

    // Update is called once per frame
    void Update()
    {
        shotCounter.text = StrokeCounter.strokesAvailable.ToString();
    }
}
