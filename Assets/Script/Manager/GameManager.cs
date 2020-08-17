using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager : MonoBehaviour
{
    /// <summary>
    /// Singleton access.
    /// </summary>
    public static GameManager instance;

    /// <summary>
    /// Possible game status.
    /// </summary>
    public enum GameStatus
    {
        WIN, LOSE, DIE, PLAY
    }

    public float mp_time;
    private int mp_score;

    private LevelObjects mp_levelObjects;

    public void SetLevelObjects(LevelObjects p_levelObjects)
    {
        mp_levelObjects = p_levelObjects;
    }

    /// <summary>
    /// Current status of the game.
    /// </summary>
    private GameStatus mp_gameStatus;

    private CollisionManager mp_collisionManager;
    private OverlayManager mp_overlayManager;

    //
    //
    // I Will need a GameLoop
    //
    private void Awake()
    {
        // TODO: Improve logic. Use singleton without using a GameObject.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //
    //
    // I Will need a GameLoop
    //
    private void Start()
    {
        mp_collisionManager = new CollisionManager(this);
        mp_overlayManager = new OverlayManager(this);

        mp_time = 30f; // TODO: Implement a per-level approach
        mp_score = 0;
        mp_gameStatus = GameStatus.PLAY;
        mp_levelObjects.m_overlay.enabled = false;
    }

    //
    //
    // I Will need a GameLoop
    //
    private void Update()
    {
        if (mp_gameStatus == GameStatus.PLAY)
        {
            mp_time -= Time.deltaTime;
            int timeInt = (int)mp_time;

            if (timeInt >= 0)
            {
                mp_levelObjects.m_timeHudText.text = string.Format("Time: {0}", timeInt);
                mp_levelObjects.m_scoreHudText.text = string.Format("Score: {0}", mp_score);
            }
        }
        else if (Input.GetButton(Constants.Input.Keys.jump))
        {
            if (mp_gameStatus == GameStatus.WIN)
            {
                Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // TODO: Implement a Scene name approach
            }
            else
            {
                Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); // TODO: Implement a Scene name approach
            }
        }
    }

    /// <summary>
    /// Handles collisions between two gameObjects.
    /// </summary>
    /// <param name="object1">One gameObject</param>
    /// <param name="object2">The Other gameObject</param>
    public void HandleCollision(GameObject object1, GameObject object2)
    {
        mp_collisionManager.HandleCollision(object1, object2);
    }

    /// <summary>
    /// Updates the Game Status
    /// </summary>
    /// <param name="p_gameStatus">The GameStatus to use the Overlay</param>
    public void SetGameStatus(GameStatus p_gameStatus)
    {
        mp_gameStatus = p_gameStatus;
        mp_overlayManager.SetOverlay();
    }

}