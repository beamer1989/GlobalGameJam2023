using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
	[Header("This determines the type of Element")]
	public ElementState MyElement = ElementState.Default;

	[Header("Element Display Values")]
	[SerializeField] private Sprite sunCollectableSprite;
	[SerializeField] private Sprite waterCollectableSprite;

	[Header("UI")]
	[SerializeField] private SpriteRenderer _spriteRenderer;

	private void Awake()
	{
		switch (MyElement)
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

	private void OnTriggerEnter2D(Collider2D col)
	{
		CharacterElementData playerCharacter = col.gameObject.GetComponent<CharacterElementData>();
		if (playerCharacter == null)
		{
			return;
		}
		
		playerCharacter.SetCurrentElementState(MyElement);
		
		//TODO: Play some Pickup Animation here
		
		Destroy(this.gameObject);
	}
}
