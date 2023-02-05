using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This component keeps track of what Element the player is using
/// </summary>

public class CharacterElementData : MonoBehaviour
{
    [Header("Character Element Colours")]
    [SerializeField] private Color defaultColour = Color.white;
    [SerializeField] private Color waterColour = Color.blue;
    [SerializeField] private Color sunColour = Color.yellow;
    
    [Header("Character Targeting Line Colours")]
    [SerializeField] private Material defaultLine;
    [SerializeField] private Material waterLine;
    [SerializeField] private Material sunLine;

    [Header("UI Elements")]
    [SerializeField] private SpriteRenderer characterSprite;

    public ElementState CurrentElementState
    {
        get
        {
            return _currentElementState;
        }
        set
        {
            _currentElementState = value;
            RefreshElementColour();
        }
    }
    
    private ElementState _currentElementState;

    public bool CanPickUpElement()
    {
        return _currentElementState == ElementState.Default;
    }
    
    // Start is called before the first frame update
    private void Start()
    {
        RefreshElementColour();
    }

    private void RefreshElementColour()
    {
        switch (_currentElementState)
        {
            case ElementState.Water:
                characterSprite.color = waterColour; 
                this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<Renderer>().material = waterLine;
                break;
            case ElementState.Sun:
                characterSprite.color = sunColour;
                this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<Renderer>().material = sunLine;
                break;
            default:
                characterSprite.color = defaultColour; 
                this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<Renderer>().material = defaultLine;
                break;
        }
    }
}
