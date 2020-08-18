using UnityEngine;

public class CollectableItem : MonoBehaviour
{
    public AudioClip collectFx;

    public int points;

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
    }
}
