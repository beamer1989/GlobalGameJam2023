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
    private int doneCounter;
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
        AllDoneA,
        AllDoneB
    }
    private tuteStatuses tuteStatus;

    void Start()
    {
        StrokeCounter.tutorialMode = true;

        tuteStatus = tuteStatuses.ClicknHold;
        TutorialText.text = "Click and hold on the green tree sprite...";

        LifePointPanel.gameObject.SetActive(false);
        Minimap.enabled = false;
        //Collectables.SetActive(false);
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
                        //Collectables.SetActive(true);
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
                        tuteStatus = tuteStatuses.GotWaterA;
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
                    }
                }
                break;
            case tuteStatuses.GotSunB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (DepositController.CurrentTotalCollected > 0)
                    {
                        tuteStatus = tuteStatuses.ReturnHome;
                    }
                    else
                    {
                        tuteStatus = tuteStatuses.GotSunA;
                        TutorialText.text = "Sun energy makes you shoot stronger, and water energy makes you slide more. You can only hold one energy at a time.";
                        sunCounter++;
                    }
                }
                break;
            case tuteStatuses.GotWaterA:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    Debug.Log("waterCounter: " + waterCounter);
                    if (waterCounter == 0)
                        waterCounter++;
                    if (waterCounter < 5)
                        tuteStatus = tuteStatuses.GotWaterB;
                    else
                    {
                        tuteStatus = tuteStatuses.ReturnHome;
                        TutorialText.text = "Bring your cargo to the heart of the tree.";
                    }
                }
                break;
            case tuteStatuses.GotWaterB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (DepositController.CurrentTotalCollected > 0)
                    {
                        tuteStatus = tuteStatuses.ReturnHome;
                    }
                    else
                    {
                        tuteStatus = tuteStatuses.GotWaterA;
                        TutorialText.text = "Water energy makes you slide more, and sun energy makes you shoot stronger. You can only hold one energy at a time.";
                        waterCounter++;
                    }
                }
                break;
            case tuteStatuses.ReturnHome:
                if (DepositController.CurrentTotalCollected > 0)
                {
                    tuteStatus = tuteStatuses.TrackLivesA;
                    TutorialText.text = "Delivering to the heart of the tree gave you more shots! Picking up an energy will give you shots too.";
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
                        tuteStatus = tuteStatuses.AllDoneA;
                        TutorialText.text = "Collecting and deliverying all the energy in the tree and you win!";

                        StrokeCounter.tutorialMode = false;
                    }
                }
                break;
            case tuteStatuses.TrackLivesB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.TrackLivesA;
                    TutorialText.text = "But every launch, you will lose shots. If you run out, you lose.";
                    trackLivesCounter++;
                }
                break;
            case tuteStatuses.AllDoneA:
                if (mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    if (doneCounter == 0)
                        doneCounter++;
                    if (doneCounter < 5)
                        tuteStatus = tuteStatuses.AllDoneB;
                    else
                    {
                        TutorialText.enabled = false;
                    }
                }
                break;
            case tuteStatuses.AllDoneB:
                if (!mainCharacter.GetComponent<character_movement>().isBeingHeld)
                {
                    tuteStatus = tuteStatuses.AllDoneA;
                    doneCounter++;
                }
                break;
        }
    }
}