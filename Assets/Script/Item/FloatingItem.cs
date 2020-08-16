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
    private Vector3 itemPosition;

    private void Start()
    {
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 0.2f));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;

        itemPosition = transform.position;
    }

    private void Update()
    {
        if (inverted)
        {
            transform.position = new Vector3(itemPosition.x, itemPosition.y - curve.Evaluate(Time.time), itemPosition.z);
        }
        else
        {
            transform.position = new Vector3(itemPosition.x, itemPosition.y + curve.Evaluate(Time.time), itemPosition.z);
        }
    }

}
