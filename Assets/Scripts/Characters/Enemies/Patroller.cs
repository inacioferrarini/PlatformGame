using UnityEngine;

/// <summary>
/// Provides an patroller behaviour.
///
/// The character will move from one side to the other of the platform where it is located,
/// avoiding it to fall from the platform.
/// </summary>
public class Patroller : Character
{
    private bool isVisible = false;

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, groundLayer);

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
        animator.SetBool(AnimationVariables.isRunning, true);
    }

    void StopEnemy()
    {
        isVisible = false;
        animator.SetBool(AnimationVariables.isRunning, false);
    }

    static class AnimationVariables
    {
        public const string isRunning = "isRunning";
    }

}
