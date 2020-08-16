using UnityEngine;

/// <summary>
/// Script user to control the player.
/// </summary>
public class Player : Character
{
    public int jumpForce;

    // Player State
    private bool mp_isJumping = false;
    private bool mp_isAlive = true;
    private bool mp_levelCompleted = false;
    private bool mp_timeIsOver = false;

    // Player Audio
    public AudioClip m_winFx;
    public AudioClip m_dieFx;
    public AudioClip m_jumpFx;

    private bool CanMove
    {
        get
        {
            return mp_isAlive && !mp_levelCompleted;
        }
    }

    private void Update()
    {
        m_grounded = Physics2D.OverlapCircle(m_groundCheck.position, 0.2f, m_groundLayer);

        if (Input.GetButtonDown(Constants.Input.Keys.jump) && m_grounded)
        {
            mp_isJumping = true;
            if (CanMove)
            {
                SoundManager.instance.PlayFxPlayer(m_jumpFx);
            }
        }

        if (((int)GameManager.instance.mp_time <= 0) && !mp_timeIsOver) // TODO: Fix. This logic is bizarre and weird.   TODO: mp_time must be private
        {
            mp_timeIsOver = true;
            PlayerDie();
        }

        PlayAnimations();
    }

    private void FixedUpdate()
    {
        if (CanMove)
        {
            float move = Input.GetAxis(Constants.Input.Axis.horizontal);
            m_rigidBody.velocity = new Vector2(move * m_speed, m_rigidBody.velocity.y);

            if ((move < 0 && m_isFacingRight) || (move > 0 && !m_isFacingRight))
            {
                Flip();
            }

            if (mp_isJumping)    // TODO: rename para mp_canJump or similar
            {
                m_rigidBody.AddForce(new Vector2(0f, jumpForce));
                mp_isJumping = false;
            }
        }
        else
        {
            m_rigidBody.velocity = new Vector2(0, m_rigidBody.velocity.y);
        }
    }










    /// <summary>
    /// Executes the player's animation, taking into consideration the player vars.
    /// </summary>
    void PlayAnimations() // TODO: Migrate this to Animator
    {
        if (mp_levelCompleted)
        {
            m_animator.Play(Animations.celebrate);
        }
        else if (!mp_isAlive)
        {
            m_animator.Play(Animations.die);
        }
        else if (m_grounded && m_rigidBody.velocity.x != 0)
        {
            m_animator.Play(Animations.run);
        }
        else if (m_grounded && m_rigidBody.velocity.x == 0)
        {
            m_animator.Play(Animations.idle);
        }
        else if (!m_grounded)
        {
            m_animator.Play(Animations.jump);
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
        if (mp_timeIsOver)
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
    /// Player animations.
    /// </summary>
    static class Animations
    {
        public const string celebrate = "Celebrate";
        public const string die = "Die";
        public const string idle = "Idle";
        public const string jump = "Jump";
        public const string run = "Run";
    }

}
