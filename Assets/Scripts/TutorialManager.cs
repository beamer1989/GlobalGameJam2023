using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TutorialManager : MonoBehaviour
{
    public GameObject tutorialTextPrefab;
    public GameObject playerCursor;
    private GameObject TutorialText;
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
        TutorialText = Instantiate(tutorialTextPrefab, playerCursor.transform);
    }

    // Update is called once per frame 
    void Update()
    {
        if (tuteStatus == tuteStatuses.ClicknHold)
        {
            if (playerCursor.GetComponent<character_movement>().isBeingHeld)
            {
                tuteStatus = tuteStatuses.DragnRelease;
                TutorialText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Drag...and release.");
            }
        }
        else if (tuteStatus == tuteStatuses.DragnRelease)
        {
            if (!playerCursor.GetComponent<character_movement>().isBeingHeld)
            {
                tuteStatus = tuteStatuses.CollectItems;
                TutorialText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Collect a sundrop or a water drop");
            }
        }
        else if (tuteStatus == tuteStatuses.CollectItems)
        {
            var charElemDat = playerCursor.GetComponent<CharacterElementData>();
            if (charElemDat.CurrentElementState != ElementState.Default)
            {
                tuteStatus = tuteStatuses.ReturnHome;
                TutorialText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("Bring your cargo to the heart of the tree.");
            }
        }
        else if (false)//tuteStatus == tuteStatuses.ReturnHome)
        {
            tuteStatus = tuteStatuses.AllDone;
            TutorialText.transform.GetChild(0).GetComponent<TextMeshProUGUI>().SetText("You get the idea, have fun!");
        }
    }
}