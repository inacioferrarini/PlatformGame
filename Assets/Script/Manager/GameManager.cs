using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// 
/// </summary>
public class GameManager : MonoBehaviour
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

    /// <summary>
    /// Game time itself.
    /// </summary>
    public float time;   // TODO: must be private

    /// <summary>
    /// Player score.
    /// </summary>
    public int score;   // TODO: must be private

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

    /// <summary>
    /// Initialization.
    /// </summary>
    void Awake()
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

    /// <summary>
    /// Initialization.
    /// </summary>
    void Start()
    {
        time = 30f; // TODO: Implement a per-level approach
        score = 0;
        gameStatus = GameStatus.PLAY;
        overlay.enabled = false;
        Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
    }

    /// <summary>
    /// Frame-based update. Called once per frame.
    /// </summary>
    void Update()
    {
        if (gameStatus == GameStatus.PLAY)
        {
            time -= Time.deltaTime;
            int timeInt = (int)time;

            if (timeInt >= 0)
            {
                timeHudText.text = "Time: " + timeInt.ToString(); // TODO: Use a string formatter
                scoreHudText.text = "Score: " + score.ToString(); // TODO: Use a string formatter
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
    /// Sets the overlay.
    /// </summary>
    /// <param name="p_gameStatus">The GameStatus to use the Overlay</param>
    public void SetOverlay(GameStatus p_gameStatus)
    {
        gameStatus = p_gameStatus; // TODO: This should not be here. The GameManager should control the status.
        overlay.enabled = true;
        overlay.sprite = overlaySpriteList[(int)gameStatus];
    }

}
