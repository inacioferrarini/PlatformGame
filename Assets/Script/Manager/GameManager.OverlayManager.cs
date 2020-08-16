using UnityEngine;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager
{
    /// <summary>
    /// Manages overlay usage.
    /// </summary>
    class OverlayManager
    {
        private GameManager mp_gameManager;

        public OverlayManager(GameManager p_gameManager)
        {
            mp_gameManager = p_gameManager;
        }

        /// <summary>
        /// Sets the overlay.
        /// </summary>
        /// <param name="p_gameStatus">The GameStatus to use the Overlay</param>
        public void SetOverlay()
        {
            switch (mp_gameManager.mp_gameStatus) //WIN, LOSE, DIE, PLAY
            {
                case GameStatus.WIN:
                    mp_gameManager.overlay.enabled = true;
                    mp_gameManager.overlay.sprite = mp_gameManager.overlaySpriteList[0];
                    break;
                case GameStatus.LOSE:
                    mp_gameManager.overlay.enabled = true;
                    mp_gameManager.overlay.sprite = mp_gameManager.overlaySpriteList[1];
                    break;
                case GameStatus.DIE:
                    mp_gameManager.overlay.enabled = true;
                    mp_gameManager.overlay.sprite = mp_gameManager.overlaySpriteList[2];
                    break;
                case GameStatus.PLAY:
                    mp_gameManager.overlay.enabled = false;
                    break;
            }

        }

    }

}
