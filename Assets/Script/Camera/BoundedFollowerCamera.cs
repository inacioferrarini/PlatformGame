using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for a camera that will follow the given `target`, respecting the defined
/// bounds.
/// </summary>
public class BoundedFollowerCamera : MonoBehaviour
{
    /// <summary>
    /// The current camera velocity.
    /// </summary>
    private Vector2 velocity;

    /// <summary>
    /// What the camera will follow, respecting the bounds.
    /// </summary>
    public Transform target;

    /// <summary>
    /// How long to start following target.
    /// </summary>
    public Vector2 delay;   // was smoothTime

    /// <summary>
    /// lower bound for camera limit.
    /// </summary>
    public Vector2 minLimit; // lower bounds

    /// <summary>
    /// Higher bound for camera limit.
    /// </summary>
    public Vector2 maxLimit; // higher bounds

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
