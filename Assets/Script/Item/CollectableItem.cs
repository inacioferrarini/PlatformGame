using UnityEngine;

// TODO: Create a interface for items that play audio

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

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
    }
}
