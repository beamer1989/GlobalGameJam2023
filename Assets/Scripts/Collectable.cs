using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
	[Header("This determines the type of Element")]
	public ElementState Element = ElementState.Default;

	[Header("Element Display Values")]
	[SerializeField] private Sprite sunCollectableSprite;
	[SerializeField] private Sprite waterCollectableSprite;

	[Header("UI")]
	[SerializeField] private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		switch (Element)
		{
			case ElementState.Sun:
				_spriteRenderer.sprite = sunCollectableSprite;
				break;
			case ElementState.Water:
				_spriteRenderer.sprite = waterCollectableSprite;
				break;
			default:
				Debug.LogError("This Collectable incorrectly has the Default Element type: " + gameObject.name);
				break;
		}
	}

}
