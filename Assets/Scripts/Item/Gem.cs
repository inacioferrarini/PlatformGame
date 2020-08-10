using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Gem script.
/// </summary>
public class Gem : MonoBehaviour
{

    public bool inverted;

    private AnimationCurve curve;
    private Vector3 gemPosition;

    void Start()
    {
        curve = new AnimationCurve(new Keyframe(0, 0), new Keyframe(0.8f, 0.2f));
        curve.preWrapMode = WrapMode.PingPong;
        curve.postWrapMode = WrapMode.PingPong;

        gemPosition = transform.position;
    }

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
