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
        private GameManager gameManager;

        public OverlayManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void SetOverlay()
        {
            switch (gameManager.gameStatus)
            {
                case GameStatus.WIN:
                    gameManager.levelObjects.overlay.enabled = true;
                    gameManager.levelObjects.overlay.sprite = gameManager.levelObjects.winOverlaySprite;
                    break;
                case GameStatus.LOSE:
                    gameManager.levelObjects.overlay.enabled = true;
                    gameManager.levelObjects.overlay.sprite = gameManager.levelObjects.loseOverlaySprite;
                    break;
                case GameStatus.DIE:
                    gameManager.levelObjects.overlay.enabled = true;
                    gameManager.levelObjects.overlay.sprite = gameManager.levelObjects.dieOverlaySprite;
                    break;
                case GameStatus.PLAY:
                    gameManager.levelObjects.overlay.enabled = false;
                    break;
            }

        }

    }

}
