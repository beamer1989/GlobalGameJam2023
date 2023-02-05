using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
	[Header("This determines the type of Element")]
	public ElementState MyElement = ElementState.Default;

	[Header("Element Display Values")]
	[SerializeField] private Sprite sunCollectableSprite;
	[SerializeField] private Sprite waterCollectableSprite;
	
	[Header("Element Display Values")]
	[SerializeField] private Sprite minimapSprite;

	[Header("UI")]
	[SerializeField] private SpriteRenderer _spriteRenderer;
	[SerializeField] private SpriteRenderer _minimapSpriteRenderer;

	private void Awake()
	{
		switch (MyElement)
		{
			case ElementState.Sun:
				{
					_spriteRenderer.sprite = sunCollectableSprite;
					//Debug.Log(_spriteRenderer.sprite);
					//Debug.Log("ff" + _minimapSpriteRenderer);
					_minimapSpriteRenderer.color = new Color(255, 226, 0);
					_minimapSpriteRenderer.sprite = minimapSprite;
					//Debug.Log(_minimapSpriteRenderer.sprite);
					Debug.Log(_minimapSpriteRenderer.color); 
				}
				break;
			case ElementState.Water:
				{
					_spriteRenderer.sprite = waterCollectableSprite;
					_minimapSpriteRenderer.sprite = minimapSprite;
					_minimapSpriteRenderer.color = Color.blue;//new Color(162, 255, 255);
					Debug.Log(_minimapSpriteRenderer.color); 
				}
				break;
			default:
				Debug.LogError("This Collectable incorrectly has the Default Element type: " + gameObject.name);
				break;
		}
	}

	private void OnTriggerEnter2D(Collider2D col)
	{
		CharacterElementData playerCharacter = col.gameObject.GetComponent<CharacterElementData>();
		if (playerCharacter == null || !playerCharacter.CanPickUpElement())
		{
			return;
		}
		
		playerCharacter.CurrentElementState = MyElement;
		
		//TODO: Play some Pickup Animation here
		
		Destroy(this.gameObject);
	}
}
