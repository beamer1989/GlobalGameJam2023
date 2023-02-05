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
    [SerializeField] private Material defaultMaterial;
    [SerializeField] private Material yellowMaterial;
    [SerializeField] private Material blueMaterial;

    
    [Header("Character Targeting Line Colours")]
    [SerializeField] private Material defaultLine;
    [SerializeField] private Material waterLine;
    [SerializeField] private Material sunLine;

    [Header("UI Elements")]
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
        Renderer shootingLineRenderer =
            this.gameObject.transform.Find("ShootingLine").gameObject.GetComponent<Renderer>();
        
        switch (_currentElementState)
        {
            case ElementState.Water:
                _meshRenderer.material = blueMaterial;
                shootingLineRenderer.material = waterLine;
                GetComponent<PlaySoundEffects>().playWater();
                break;
            case ElementState.Sun:
                _meshRenderer.material = yellowMaterial;
                shootingLineRenderer.material = sunLine;
                GetComponent<PlaySoundEffects>().playSun();
                break;
            default:
                _meshRenderer.material = defaultMaterial;
                shootingLineRenderer.material = defaultLine;
                break;
        }
    }
}
