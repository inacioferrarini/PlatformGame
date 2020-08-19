using UnityEngine;

/// <summary>
/// Script for a scrolling background, causing a parallax effect.
/// </summary>
public class ParallaxScroller : MonoBehaviour
{
    public Transform[] m_backgroundLayerList;
    public float[] m_parallaxVelocityList;
    public float m_delay;
    public Transform m_camera;

    private Vector3 mp_previewCamera;

    private void Start()
    {
        mp_previewCamera = m_camera.position;
    }

    private void Update()
    {
        for (int i = 0; i < m_backgroundLayerList.Length; i++)
        {
            float parallax = (mp_previewCamera.x - m_camera.position.x) * m_parallaxVelocityList[i];
            float targetXPos = m_backgroundLayerList[i].position.x - parallax;
            Vector3 targetPos = new Vector3(targetXPos, m_backgroundLayerList[i].position.y, m_backgroundLayerList[i].position.z);
            m_backgroundLayerList[i].position = Vector3.Lerp(m_backgroundLayerList[i].position, targetPos, m_delay * Time.deltaTime);
        }

        mp_previewCamera = m_camera.position;
    }

}
