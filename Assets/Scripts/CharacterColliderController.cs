using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Handles all collision events for the player character
/// </summary>

[RequireComponent(typeof(Rigidbody2D))]
public class CharacterColliderController : MonoBehaviour
{
    [SerializeField] private CharacterElementData _characterElementData;

    // private void OnCollisionEnter2D(Collision2D col)
    // {
    //     if (col == null || col.gameObject == null) return;
    //
    //     Collectable collectable = col.gameObject.GetComponent<Collectable>();
    //     if (collectable != null)
    //     {
    //         _characterElementData.SetCurrentElementState(collectable.MyElement);
    //         Destroy(col.gameObject);
    //     }
    // }
}
