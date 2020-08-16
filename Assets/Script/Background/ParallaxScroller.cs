using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Script for a scrolling background, causing a parallax effect.
/// </summary>
public class ParallaxScroller : MonoBehaviour
{

    // TODO: backgroundLayerList and parallaxVelocityList can become a model[] ?

    /// <summary>
    /// The background layers used for parallax effect.
    /// </summary>
    public Transform[] backgroundLayerList;

    /// <summary>
    /// The velocity to apply to each background layer.
    /// </summary>
    public float[] parallaxVelocityList;

    /// <summary>
    /// How long to start applying parallax.
    /// </summary>
    public float delay;

    /// <summary>
    /// The parallax effect observer's reference.
    /// </summary>
    public new Transform camera;

    private Vector3 previewCamera;

    private void Start()
    {
        previewCamera = camera.position;
    }

    private void Update()
    {
        for (int i = 0; i < backgroundLayerList.Length; i++)
        {
            float parallax = (previewCamera.x - camera.position.x) * parallaxVelocityList[i];
            float targetXPos = backgroundLayerList[i].position.x - parallax;
            Vector3 targetPos = new Vector3(targetXPos, backgroundLayerList[i].position.y, backgroundLayerList[i].position.z);
            backgroundLayerList[i].position = Vector3.Lerp(backgroundLayerList[i].position, targetPos, delay * Time.deltaTime);
        }

        previewCamera = camera.position;
    }

}
