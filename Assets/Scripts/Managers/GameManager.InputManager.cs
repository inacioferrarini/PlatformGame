using UnityEngine;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager
{
    /// <summary>
    /// Manages user input.
    /// </summary>
    class InputManager
    {
        private GameManager mp_gameManager;

        public InputManager(GameManager p_gameManager)
        {
            mp_gameManager = p_gameManager;
        }

        public void HandleUserInput()
        {
            Player player = mp_gameManager.mp_player;

            //if (Input.GetButtonDown(Constants.Input.Keys.jump) && player.IsGrounded)
            //{
            //    player.IsJumping = true;
            //    if (player.PlayerCanMove)
            //    {
            //        SoundManager.instance.PlayFxPlayer(player.m_jumpFx);  // TODO: Create a Wrapper variable?
            //    }
            //}

        }

    }
}