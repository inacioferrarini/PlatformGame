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
        public void SetOverlay(GameStatus p_gameStatus)
        {
            mp_gameManager.gameStatus = p_gameStatus;          // TODO: Improves this logic.
            mp_gameManager.overlay.enabled = true;             // TODO: Improves this logic.
            mp_gameManager.overlay.sprite = mp_gameManager.overlaySpriteList[(int)p_gameStatus]; // TODO: This should not be here. The GameManager should control the status.
        }

    }

}