using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// An item that floats.
/// 
/// The float movement will be defined by the `inverted` parameter.
/// If `inverted` is true, it will float up.
/// Else, it will float down.
/// 
/// </summary>
public class FloatingItem : MonoBehaviour
{
    public bool inverted;

    private AnimationCurve curve;
    private Vector3 gemPosition;

    /// <summary>
    /// Initialization.
    /// </summary>
    void Start()
    {
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 0.2f));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;

        gemPosition = transform.position;
    }

    /// <summary>
    /// Frame-based update. Called once per frame
    /// </summary>
    void Update()
    {
        if (inverted)
        {
            transform.position = new Vector3(gemPosition.x, gemPosition.y - curve.Evaluate(Time.time), gemPosition.z);
        }
        else
        {
            transform.position = new Vector3(gemPosition.x, gemPosition.y + curve.Evaluate(Time.time), gemPosition.z);
        }


    }

}
