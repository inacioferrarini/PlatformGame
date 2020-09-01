using UnityEngine;

/// <summary>
/// Provides control and player behaviour.
/// The character will move based on game input.
/// </summary>
public class Player : Character
{
    public int jumpForce;

    // Player State
    private bool mp_isJumping = false;
    public bool IsJumping
    {
        get { return mp_isJumping; }
        set { mp_isJumping = value; }
    }

    private bool mp_isAlive = true;
    private bool mp_levelCompleted = false;
    private bool mp_isRunning = false;

    // Player Audio
    public AudioClip m_winFx;
    public AudioClip m_dieFx;
    public AudioClip m_jumpFx;

    private bool PlayerCanMove { get { return mp_isAlive && !mp_levelCompleted; } }

    private void Awake()
    {
        GameManager.instance.SetPlayer(gameObject.GetComponent<Player>());
    }

    private void Update()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, m_radiusCheck, m_groundLayer);
        m_animator.SetBool(AnimationVariables.isAlive, mp_isAlive);
        m_animator.SetBool(AnimationVariables.isGrounded, m_grounded);
        m_animator.SetBool(AnimationVariables.isLevelComplete, mp_levelCompleted);
        m_animator.SetBool(AnimationVariables.isRunning, mp_isRunning);

        if (Input.GetButtonDown(Constants.Input.Keys.jump) && m_grounded)
        {
            IsJumping = true;
            if (PlayerCanMove)   // TODO: Decision - pass to GameManager
            {
                SoundManager.instance.PlayFxPlayer(m_jumpFx);  // TODO: Create a Wrapper variable?
            }
        }
    }

    private void FixedUpdate()
    {
        if (PlayerCanMove)        // TODO: Decision - pass to GameManager
        {
            float move = Input.GetAxis(Constants.Input.Axis.horizontal);
            float velocity = move * m_speed;
            m_rigidBody.velocity = new Vector2(velocity, m_rigidBody.velocity.y);
            mp_isRunning = (velocity != 0);

            if ((move < 0 && m_isFacingRight) || (move > 0 && !m_isFacingRight))
            {
                Flip();
            }

            if (mp_isJumping)
            {
                m_rigidBody.AddForce(new Vector2(0f, jumpForce));
                mp_isJumping = false;
            }
        }
        else
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
            mp_isRunning = false;
        }
    }

    //
    // Collisions
    //

    private void OnCollisionEnter2D(Collision2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        GameManager.instance.HandleCollision(gameObject, other.gameObject);
    }

    //
    // Animations
    //

    private void Flip()
    {
        m_isFacingRight = !m_isFacingRight;
        transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
    }

    /// <summary>
    /// The current level was completed
    /// </summary>
    public void LevelCompleted()
    {
        mp_levelCompleted = true;
    }

    /// <summary>
    /// Player died.
    /// </summary>
    public void PlayerDie()
    {
        mp_isAlive = false;
    }

    /// <summary>
    /// Die animation finished.
    /// </summary>
    void DieAnimationFinished()
    {
        if (GameManager.instance.IsTimeOver)
        {
            GameManager.instance.SetGameStatus(GameManager.GameStatus.LOSE);
        }
        else
        {
            GameManager.instance.SetGameStatus(GameManager.GameStatus.DIE);
        }
    }

    /// <summary>
    /// Celebrate animation finished.
    /// </summary>
    void CelebrationAnimationFinished()
    {
        GameManager.instance.SetGameStatus(GameManager.GameStatus.WIN);
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
