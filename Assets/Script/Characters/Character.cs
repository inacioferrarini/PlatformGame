using UnityEngine;

/// <summary>
/// A Character
/// </summary>
public class Character : MonoBehaviour
{
    public float m_speed;
    public Transform m_groundCheck;
    public LayerMask m_groundLayer;
    public float m_radiusCheck;
    protected bool m_grounded;
    protected bool m_isFacingRight = true;
    protected Rigidbody2D m_rigidBody;
    protected Animator m_animator;

    protected void Start()
    {
        m_rigidBody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
    }

}
