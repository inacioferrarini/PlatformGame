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
    /// Overlayes used by the game.
    /// </summary>
    public Sprite[] overlaySpriteList;

    /// <summary>
    /// Current overlay being displayed.
    /// </summary>
    public Image overlay;

    /// <summary>
    /// Time displayed on the hud.
    /// </summary>
    public Text timeHudText;

    /// <summary>
    /// Score displayed on the hud.
    /// </summary>
    public Text scoreHudText;

    public float mp_time;
    public int mp_score;

    /// <summary>
    /// Possible game status.
    /// </summary>
    public enum GameStatus
    {
        WIN, LOSE, DIE, PLAY
    }

    /// <summary>
    /// Current status of the game.
    /// </summary>
    public GameStatus gameStatus;

    private CollisionManager mp_collisionManager;
    private OverlayManager mp_overlayManager;

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

    private void Start()
    {
        mp_collisionManager = new CollisionManager(this);
        mp_overlayManager = new OverlayManager(this);

        mp_time = 30f; // TODO: Implement a per-level approach
        mp_score = 0;
        gameStatus = GameStatus.PLAY;
        overlay.enabled = false;
        Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
    }

    private void Update()
    {
        if (gameStatus == GameStatus.PLAY)
        {
            mp_time -= Time.deltaTime;
            int timeInt = (int)mp_time;

            if (timeInt >= 0)
            {
                timeHudText.text = string.Format("Time: {0}", timeInt);
                scoreHudText.text = string.Format("Score: {0}", mp_score);
            }
        }
        else if (Input.GetButton(Constants.Input.Keys.jump))
        {
            if (gameStatus == GameStatus.WIN)
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); // TODO: Implement a Scene name approach
            }
            else
            {
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

    //
    // TODO: Create a OverlayManager to handle Overlayes.
    //

    /// <summary>
    /// Sets the overlay.
    /// </summary>
    /// <param name="p_gameStatus">The GameStatus to use the Overlay</param>
    public void SetOverlay(GameStatus p_gameStatus)
    {
        mp_overlayManager.SetOverlay(p_gameStatus);
    }

}