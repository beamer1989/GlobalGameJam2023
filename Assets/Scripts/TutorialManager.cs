using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TutorialManager : MonoBehaviour
{
    public GameObject mainCharacter;
    public Text TutorialText;
    public Image LifePointPanel;
    public RawImage Minimap;
    public GameObject Collectables;
    public Sprite Vignette01;
    public Sprite Vignette02;
    private int trackLivesCounter;
    private int minimapCounter;
    private int sunCounter;
    private int waterCounter;
    private enum tuteStatuses
    {
        ClicknHold,
        DragnRelease,
        TrackLivesA,
        TrackLivesB,
        MinimapA,
        MinimapB,
        CollectItems,
        GotSunA,
        GotSunB,
        GotSunC,
        GotWaterA,
        GotWaterB,
        GotWaterC,
        ReturnHome,
        AllDone
    }
    private tuteStatuses tuteStatus;

    void Start()
    {
        StrokeCounter.tutorialMode = true;

        tuteStatus = tuteStatuses.ClicknHold;
        TutorialText.text = "Click and hold...";

        LifePointPanel.gameObject.SetActive(false);
        Minimap.enabled = false;
        Collectables.SetActive(false);
    }

    // Update is called once per frame 
    void Update()
    {
        switch (tuteStatus)
        {
            case tuteStatuses.ClicknHold:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.DragnRelease;
                    TutorialText.text = "Drag...and release";
                    //VignetteCanvas01.transform;
                }
                break;
            case tuteStatuses.DragnRelease:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.MinimapA;
                    TutorialText.text = "You are in a big tree...try exploring a bit.";
                    Minimap.enabled = true;
                }
                break;
            case tuteStatuses.MinimapA:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (minimapCounter == 0)
                        minimapCounter++;
                    if (minimapCounter < 5)
                        tuteStatus = tuteStatuses.MinimapB;
                    else
                    {
                        tuteStatus = tuteStatuses.CollectItems;
                        TutorialText.text = "Try moving toward a blue or yellow dot on the mini-map.";
                        Collectables.SetActive(true);
                    }
                }
                break;
            case tuteStatuses.MinimapB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.MinimapA;
                    minimapCounter++;
                }
                break;
            case tuteStatuses.CollectItems:
                {
                    var charElemDat = mainCharacter.GetComponent<CharacterElementData>();
                    if (charElemDat.CurrentElementState == ElementState.Sun)
                    {
                        tuteStatus = tuteStatuses.GotSunA;
                        TutorialText.text = "What's this?  You collected sun!";
                    } else if (charElemDat.CurrentElementState == ElementState.Water)
                    {
                        tuteStatus = tuteStatuses.ReturnHome;
                        TutorialText.text = "What's this?  You collected water!";
                    }
                }
                break;
            case tuteStatuses.GotSunA:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (sunCounter == 0)
                        sunCounter++;
                    if (sunCounter < 5)
                        tuteStatus = tuteStatuses.GotSunB;
                    else
                    {
                        tuteStatus = tuteStatuses.ReturnHome;
                        TutorialText.text = "Bring your cargo to the heart of the tree.";
                        Collectables.SetActive(true);
                    }
                }
                break;
            case tuteStatuses.GotSunB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.GotSunA;
                    TutorialText.text = "Sun energy makes you move further, try it a bit";
                    sunCounter++;
                }
                break;
            case tuteStatuses.GotWaterA:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (waterCounter == 0)
                        waterCounter++;
                    if (waterCounter < 5)
                        tuteStatus = tuteStatuses.GotWaterB;
                    else
                    {
                        tuteStatus = tuteStatuses.ReturnHome;
                        TutorialText.text = "Bring your cargo to the heart of the tree.";
                        Collectables.SetActive(true);
                    }
                }
                break;
            case tuteStatuses.GotWaterB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.GotWaterA;
                    TutorialText.text = "Water energy makes you bounce more, try it a bit";
                    waterCounter++;
                }
                break;
            case tuteStatuses.ReturnHome:
                if (DepositController.CurrentTotalCollected > 0)
                {
                    tuteStatus = tuteStatuses.TrackLivesA;
                    TutorialText.text = "Delivering to the heart of the tree gave you energy!";
                    LifePointPanel.gameObject.SetActive(true);
                }
                break;
            case tuteStatuses.TrackLivesA:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (trackLivesCounter == 0)
                        trackLivesCounter++;
                    if (trackLivesCounter < 5)
                        tuteStatus = tuteStatuses.TrackLivesB;
                    else
                    {
                        tuteStatus = tuteStatuses.AllDone;
                        TutorialText.text = "You get the idea, have fun!";
                    }
                }
                break;
            case tuteStatuses.TrackLivesB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.TrackLivesA;
                    TutorialText.text = "But every launch, you will lose energy...try it out a bit.";
                    trackLivesCounter++;
                }
                break;
        }
    }
}