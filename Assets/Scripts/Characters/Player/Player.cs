using UnityEngine;

/// <summary>
/// Provides control and player behaviour.
/// The character will move based on game input.
/// </summary>
public class Player : Character
{
    public int jumpForce;
    public Joystick joystick;

    // Player State
    private bool isJumping = false;
    private bool isAlive = true;
    private bool levelCompleted = false;
    private bool isRunning = false;

    // Player Audio
    public AudioClip winFx;
    public AudioClip dieFx;
    public AudioClip jumpFx;

    private bool PlayerCanMove { get { return isAlive && !levelCompleted; } }

    private void Awake()
    {
        GameManager.Instance.SetPlayer(gameObject.GetComponent<Player>());
    }

    private void Update()
    {
        grounded = Physics2D.OverlapCircle(groundCheck.position, radiusCheck, groundLayer);
        animator.SetBool(AnimationVariables.isAlive, isAlive);
        animator.SetBool(AnimationVariables.isGrounded, grounded);
        animator.SetBool(AnimationVariables.isLevelComplete, levelCompleted);
        animator.SetBool(AnimationVariables.isRunning, isRunning);

        bool jumpInput = (Input.GetButtonDown(Constants.Input.Keys.jump) || joystick.Vertical > 0.5f) && grounded;
        if (jumpInput)
        {
            isJumping = true;
            if (PlayerCanMove)   // TODO: Decision - pass to GameManager
            {
                SoundManager.Instance.PlayFxPlayer(jumpFx);  // TODO: Create a Wrapper variable?
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerCanMove)
        {
            float move = 0;
            if (Input.GetAxis(Constants.Input.Axis.horizontal) != 0)
            {
                move = Input.GetAxis(Constants.Input.Axis.horizontal);
            }
            else if (joystick.Horizontal >= 0.2f)
            {
                move = 1;
            }
            else if (joystick.Horizontal <= -0.2f)
            {
                move = -1;
            }
            float velocity = move * speed;
            rigidBody.velocity = new Vector2(velocity, rigidBody.velocity.y);
            isRunning = (velocity != 0);


            if ((move < 0 && isFacingRight) || (move > 0 && !isFacingRight))
            {
                Flip();
            }

            if (isJumping)
            {
                rigidBody.AddForce(new Vector2(0f, jumpForce));
                isJumping = false;
            }
        }
        else
        {
            rigidBody.velocity = new Vector2(0, rigidBody.velocity.y);
            isRunning = false;
        }
    }

    //
    // Collisions
    //

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.Instance.HandleCollision(gameObject, other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.Instance.HandleCollision(gameObject, other.gameObject);
    }

    //
    // Animations
    //

    private void Flip()
    {
        isFacingRight = !isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// The current level was completed
    /// </summary>
    public void LevelCompleted()
    {
        levelCompleted = true;
    }

    /// <summary>
    /// Player died.
    /// </summary>
    public void PlayerDie()
    {
        isAlive = false;
    }

    /// <summary>
    /// Die animation finished.
    /// </summary>
    void DieAnimationFinished()
    {
        if (GameManager.Instance.IsTimeOver)
        {
            GameManager.Instance.SetGameStatus(GameManager.GameStatus.LOSE);
        }
        else
        {
            GameManager.Instance.SetGameStatus(GameManager.GameStatus.DIE);
        }
    }

    /// <summary>
    /// Celebrate animation finished.
    /// </summary>
    void CelebrationAnimationFinished()
    {
        GameManager.Instance.SetGameStatus(GameManager.GameStatus.WIN);
    }

    /// <summary>
    /// Player Animation Variables.
    /// </summary>
    static class AnimationVariables
    {
        public const string isRunning = "isRunning";
        public const string isGrounded = "isGrounded";
        public const string isLevelComplete = "isLevelComplete";
        public const string isAlive = "isAlive";
    }

}
