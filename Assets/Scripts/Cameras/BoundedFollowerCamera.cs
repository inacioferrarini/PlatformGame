using UnityEngine;

/// <summary>
/// Script for a camera that will follow the given `target`, but will not go
/// beyond the defined limits.
/// </summary>
public class BoundedFollowerCamera : MonoBehaviour
{
    public Transform target;
    public Vector2 delay;
    public Vector2 minLimit;
    public Vector2 maxLimit;

    private Vector2 velocity;

    private void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    private void Update()
    {
        if (target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, delay.x);
            float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, delay.y);

            transform.position = new Vector3(
                Mathf.Clamp(posX, minLimit.x, maxLimit.x),
                Mathf.Clamp(posY, minLimit.y, maxLimit.y),
                transform.position.z);
        }
    }

}
