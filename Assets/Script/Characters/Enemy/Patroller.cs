using UnityEngine;

/// <summary>
/// Script for patroller objects.
/// A patroller is an object that moves from one side to the other.
/// </summary>
public class Patroller : Character
{
    /// <summary>
    /// Is visible on screen?
    /// </summary>
    private bool isVisible = false;

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);

        if (!grounded)
        {
            Flip();
        }
    }

    private void FixedUpdate()
    {
        if (isVisible)
        {
            rigidBody.velocity = new Vector2(speed, rigidBody.velocity.y);
        }
        else
        {
            rigidBody.velocity = new Vector2(0f, rigidBody.velocity.y);
        }
    }

    /// <summary>
    /// Switches the side the patroller is facing.
    /// </summary>
    void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
        speed *= -1;
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
        isVisible = true;
        animator.Play("Run");
    }

    void StopEnemy()
    {
        isVisible = false;
        animator.Play("Idle");
    }

}
