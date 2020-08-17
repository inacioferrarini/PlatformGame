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
                    mp_gameManager.mp_levelObjects.m_overlay.enabled = true;
                    mp_gameManager.mp_levelObjects.m_overlay.sprite = mp_gameManager.mp_levelObjects.m_winOverlaySprite;
                    break;
                case GameStatus.LOSE:
                    mp_gameManager.mp_levelObjects.m_overlay.enabled = true;
                    mp_gameManager.mp_levelObjects.m_overlay.sprite = mp_gameManager.mp_levelObjects.m_loseOverlaySprite;
                    break;
                case GameStatus.DIE:
                    mp_gameManager.mp_levelObjects.m_overlay.enabled = true;
                    mp_gameManager.mp_levelObjects.m_overlay.sprite = mp_gameManager.mp_levelObjects.m_dieOverlaySprite;
                    break;
                case GameStatus.PLAY:
                    mp_gameManager.mp_levelObjects.m_overlay.enabled = false;
                    break;
            }

        }

    }

}
