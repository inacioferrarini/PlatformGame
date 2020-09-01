using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager
{
    /// <summary>
    /// Manages Level usage.
    /// </summary>
    class LevelManager
    {
        enum Level
        {
            LEVEL_1 = 0,
            LEVEL_2,
            COUNT = 3
        }

        private GameManager gameManager;
        private Level currentLevel = Level.LEVEL_1;

        public LevelManager(GameManager gameManager)
        {
            this.gameManager = gameManager;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene((int)currentLevel, LoadSceneMode.Single);
        }

        public void LoadNextLevel()
        {
            Level? optionalNextLevel = NextLevel(currentLevel);
            if (optionalNextLevel != null)
            {
                Level nextLevel = (Level)optionalNextLevel;
                currentLevel = nextLevel;
                SceneManager.LoadScene((int)nextLevel);
            }
        }

        private Level? NextLevel(Level p_currentLevel)
        {
            int nextLevel = (int)p_currentLevel + 1;
            if (nextLevel < (int)Level.COUNT)
            {
                return (Level)nextLevel;
            }
            else
            {
                Debug.Log(string.Format("<color=red>Fatal error:</color> Scene with index {0} not found. IGNORING. Check Scenes in build settings.", nextLevel));
            }
            return null;
        }

    }

}