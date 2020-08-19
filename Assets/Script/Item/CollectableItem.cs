using UnityEngine;

/// <summary>
/// An item that can be collected.
///
/// Can have 'points' defined, to increase player score and also a 
/// Fx clip to be played upon collecting.
/// </summary>
public class CollectableItem : MonoBehaviour
{
    public AudioClip m_collectFx;
    public int m_points;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
    }
}
