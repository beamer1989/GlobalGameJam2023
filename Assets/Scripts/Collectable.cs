using UnityEngine;


[RequireComponent(typeof(Collider2D))]
public class Collectable : MonoBehaviour
{
	[Header("This determines the type of Element")]
	public ElementState MyElement = ElementState.Default;

	[Header("Element Display Values")]
	[SerializeField] private GameObject sunCollectableDisplay;
	[SerializeField] private GameObject waterCollectableDisplay;
	
	[Header("Minimap")]
	[SerializeField] private Sprite minimapSprite;

	[Header("UI")]
	[SerializeField] private SpriteRenderer _minimapSpriteRenderer;

	private void Awake()
	{
		switch (MyElement)
		{
			case ElementState.Sun:
				{
					sunCollectableDisplay.SetActive(true);
					waterCollectableDisplay.SetActive(false);
					//Debug.Log(_spriteRenderer.sprite);
					//Debug.Log("ff" + _minimapSpriteRenderer);
					_minimapSpriteRenderer.color = new Color(255, 226, 0);
					_minimapSpriteRenderer.sprite = minimapSprite;
					//Debug.Log(_minimapSpriteRenderer.sprite);
					//Debug.Log(_minimapSpriteRenderer.color); 
				}
				break;
			case ElementState.Water:
				{
					sunCollectableDisplay.SetActive(false);
					waterCollectableDisplay.SetActive(true);
					_minimapSpriteRenderer.color = new Color(0, 0, 255);
					_minimapSpriteRenderer.sprite = minimapSprite;
					
					//Debug.Log(_minimapSpriteRenderer.color); 
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
		
		StrokeCounter.IncreaseStrokesPowerup();

		playerCharacter.CurrentElementState = MyElement;
		
		Destroy(this.gameObject);
	}
}
