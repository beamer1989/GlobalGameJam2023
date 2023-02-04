using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DepositController : MonoBehaviour
{
	private void OnTriggerEnter2D(Collider2D col)
	{
		CharacterElementData playerController = col.gameObject.GetComponent<CharacterElementData>();

		if (playerController == null || playerController.CanPickUpElement())
		{
			return;
		}

		playerController.CurrentElementState = ElementState.Default;
	}
}
