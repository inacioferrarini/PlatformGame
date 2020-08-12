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

    /// <summary>
    /// Initialization.
    /// </summary>
    void Start()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
    }

    /// <summary>
    /// Frame-based update. Called once per frame
    /// </summary>
    void Update()
    {
        if (target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, target.position.x, ref velocity.x, delay.x);
            float posY = Mathf.SmoothDamp(transform.position.y, target.position.y, ref velocity.y, delay.y);

            transform.position = new Vector3(posX, posY, transform.position.z);
        }
    }

}
