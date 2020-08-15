using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// TODO: Create a interface for items that play items

/// <summary>
/// A kind of item that can be collected during the game.
/// </summary>
public class CollectableItem : MonoBehaviour
{
    /// <summary>
    /// Sound to play when item is collected.
    /// </summary>
    public AudioClip collectFx;

    /// <summary>
    /// How many points to increase the player score.
    /// </summary>
    public int points;

    /// <summary>
    /// Something collided with the item.
    /// </summary>
    /// <param name="collision"></param>
    void OnTriggerEnter2D(Collider2D collision)
    {
        GameManager.instance.HandleCollision(gameObject, collision);
    }
}
