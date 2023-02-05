using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialTextPrefab;
    public GameObject playerCursor;
    public GameObject TutorialText;
    public GameObject VignetteCanvas01;
    public GameObject VignetteCanvas02;
    private enum tuteStatuses
    {
        ClicknHold,
        DragnRelease,
        CollectItems,
        ReturnHome,
        AllDone
    }
    private tuteStatuses tuteStatus;

    void Start()
    {
        tuteStatus = tuteStatuses.ClicknHold;
        //Debug.Log(TutorialText);
        //TutorialText = Instantiate(tutorialTextPrefab, playerCursor.transform);
    }

    // Update is called once per frame 
    void Update()
    {
        if (tuteStatus == tuteStatuses.ClicknHold)
        {
            if (playerCursor.GetComponent<character_movement>().isBeingHeld)
            {
                tuteStatus = tuteStatuses.DragnRelease;
                TutorialText.GetComponent<TextMeshProUGUI>().SetText("Drag...and release.");
                //VignetteCanvas01.transform;
            }
        }
        else if (tuteStatus == tuteStatuses.DragnRelease)
        {
            if (!playerCursor.GetComponent<character_movement>().isBeingHeld)
            {
                tuteStatus = tuteStatuses.CollectItems;
                TutorialText.GetComponent<TextMeshProUGUI>().SetText("Collect a sundrop or a water drop");
            }
        }
        else if (tuteStatus == tuteStatuses.CollectItems)
        {
            var charElemDat = playerCursor.GetComponent<CharacterElementData>();
            if (charElemDat.CurrentElementState != ElementState.Default)
            {
                tuteStatus = tuteStatuses.ReturnHome;
                TutorialText.GetComponent<TextMeshProUGUI>().SetText("Bring your cargo to the heart of the tree.");
            }
        }
        else if (false)//tuteStatus == tuteStatuses.ReturnHome)
        {
            tuteStatus = tuteStatuses.AllDone;
            TutorialText.GetComponent<TextMeshProUGUI>().SetText("You get the idea, have fun!");
        }
    }
}