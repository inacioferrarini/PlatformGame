using UnityEngine;

/// <summary>
/// Base for a character in the game.
/// Provides basic state shared for player and enemy.
/// </summary>
public class Character : MonoBehaviour
{
    public float speed;
    public Transform groundCheck;
    public LayerMask groundLayer;
    public float radiusCheck;
    protected bool grounded;
    protected bool isFacingRight = true;
    protected Rigidbody2D rigidBody;
    protected Animator animator;

    protected void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

}
