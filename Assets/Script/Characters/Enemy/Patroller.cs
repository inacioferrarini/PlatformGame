﻿using UnityEngine;

/// <summary>
/// Script for patroller objects.
/// A patroller is an object that moves from one side to the other.
/// </summary>
public class Patroller : Character
{
    private bool mp_isVisible = false;

    private void Update()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, m_radiusCheck, m_groundLayer);

        if (!m_grounded)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (mp_isVisible)
        {
            m_rigidBody.velocity = new Vector2(m_speed, m_rigidBody.velocity.y);
        }
        else
        {
            m_rigidBody.velocity = new Vector2(0f, m_rigidBody.velocity.y);
        }
    }

    void Flip()
    {
        m_isFacingRight = !m_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        m_speed *= -1;
    }

    void OnBecameVisible()
    {
        Invoke("MoveEnemy", 3f);
    }

    void OnBecameInvisible()
    {
        Invoke("StopEnemy", 3f);
    }

    void MoveEnemy()
    {
        mp_isVisible = true;
        m_animator.Play(Animations.run);
    }

    void StopEnemy()
    {
        mp_isVisible = false;
        m_animator.Play(Animations.idle);
    }

    static class Animations
    {
        public const string idle = "Idle";
        public const string run = "Run";
    }

}
