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

        private GameManager mp_gameManager;
        private Level mp_currentLevel = Level.LEVEL_1;

        public LevelManager(GameManager p_gameManager)
        {
            mp_gameManager = p_gameManager;
        }

        public void RestartLevel()
        {
            SceneManager.LoadScene((int)mp_currentLevel, LoadSceneMode.Single);
        }

        public void LoadNextLevel()
        {
            Level? optionalNextLevel = NextLevel(mp_currentLevel);
            if (optionalNextLevel != null)
            {
                Level nextLevel = (Level)optionalNextLevel;
                mp_currentLevel = nextLevel;
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