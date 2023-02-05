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

    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;

    
    [Header("UI Elements")]
    //[SerializeField] private SpriteRenderer characterSprite;
    [SerializeField] private MeshRenderer _meshRenderer;
    
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
                //characterSprite.color = waterColour; 
                _meshRenderer.material = blueMaterial;
                break;
            case ElementState.Sun:
                //characterSprite.color = sunColour;
                _meshRenderer.material = yellowMaterial;
                break;
            default:
                //characterSprite.color = defaultColour; 
                _meshRenderer.material = defaultMaterial;
                break;
        }
    }
}
