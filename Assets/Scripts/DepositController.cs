using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class DepositController : MonoBehaviour
{
	public Transform CollectablesParent;
	public static int CurrentTotalCollected;
	private int _totalCollectablesToWin;

	private void Start()
	{
		_totalCollectablesToWin = CollectablesParent.childCount;
	}
	
	private void OnTriggerEnter2D(Collider2D col)
	{
		CharacterElementData playerController = col.gameObject.GetComponent<CharacterElementData>();

		if (playerController == null || playerController.CanPickUpElement())
		{
			return;
		}

		playerController.CurrentElementState = ElementState.Default;

		col.gameObject.GetComponent<PlaySoundEffects>().playDeliver();
                
        StrokeCounter.IncreaseStrokes();
        
		CheckIfGameWin();
	}
	
	private void CheckIfGameWin()
	{
		CurrentTotalCollected++;

		if (CurrentTotalCollected >= _totalCollectablesToWin)
		{
			//Trigger game win
			SceneManager.LoadScene(4);
		}
	}


}
