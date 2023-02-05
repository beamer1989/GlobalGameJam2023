using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupAnimationBehaviour : StateMachineBehaviour
{
	// public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	// {
	// 	animator.SetBool("animationFinished" , false);
	// }

	public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
	{
		//animator.SetBool("animationFinished" , true);
		var spriteRenderer = animator.gameObject.GetComponent<SpriteRenderer>();
		spriteRenderer.sprite = null;

		animator.gameObject.SetActive(false);
	}
}
