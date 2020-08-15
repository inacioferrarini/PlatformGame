using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A kind of item that can be collected.
/// </summary>
public class CollectableItem : MonoBehaviour
{

    public AudioClip fxCollect;

    void OnTriggerEnter2D(Collider2D collision)
    {
        // TODO: Delegate this to the game manager.
        if (collision.CompareTag(Constants.Collision.Tags.player))
        {
            GameManager.instance.score++;
            SoundManager.instance.PlayFxItem(fxCollect);
            Destroy(gameObject);
        }
    }

}
