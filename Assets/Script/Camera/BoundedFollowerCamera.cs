using UnityEngine;

/// <summary>
/// Script for a camera that will follow the given `target`, but will not
/// beyond the defined limits.
/// </summary>
public class BoundedFollowerCamera : MonoBehaviour
{
    public Transform m_target;
    public Vector2 m_delay;
    public Vector2 m_minLimit;
    public Vector2 m_maxLimit;

    private Vector2 mp_velocity;

    private void Start()
    {
        transform.position = new Vector3(m_target.position.x, m_target.position.y, transform.position.z);
    }

    private void Update()
    {
        if (m_target != null)
        {
            float posX = Mathf.SmoothDamp(transform.position.x, m_target.position.x, ref mp_velocity.x, m_delay.x);
            float posY = Mathf.SmoothDamp(transform.position.y, m_target.position.y, ref mp_velocity.y, m_delay.y);

            transform.position = new Vector3(
                Mathf.Clamp(posX, m_minLimit.x, m_maxLimit.x),
                Mathf.Clamp(posY, m_minLimit.y, m_maxLimit.y),
                transform.position.z);
        }
    }

}
