using UnityEngine;

/// <summary>
/// Script for a scrolling background, causing a parallax effect.
/// </summary>
public class ParallaxScroller : MonoBehaviour
{
    public Transform[] backgroundLayerList;
    public float[] parallaxVelocityList;
    public float delay;
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
