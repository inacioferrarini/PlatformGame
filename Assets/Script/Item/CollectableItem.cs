using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A kind of item that can be collected.
/// </summary>
public class CollectableItem : MonoBehaviour
{

    // TODO: Create tags

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(Player.compareTag))
        {
            Destroy(gameObject);
        }
    }

}
