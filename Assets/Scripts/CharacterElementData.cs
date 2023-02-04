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
    
    [Header("UI Elements")]
    [SerializeField] private SpriteRenderer characterSprite;

    private ElementState _currentElementState;


    public void SetCurrentElementState(ElementState newElementState)
    {
        _currentElementState = newElementState;
        RefreshElementColour();
    }
    
    // Start is called before the first frame update
    void Start()
    {
        RefreshElementColour();
    }

    private void RefreshElementColour()
    {
        switch (_currentElementState)
        {
            case ElementState.Water:
                characterSprite.color = waterColour; 
                break;
            case ElementState.Sun:
                characterSprite.color = sunColour;
                break;
            default:
                characterSprite.color = defaultColour; 
                break;
        }
    }
}
