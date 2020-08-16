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
        time = 30f; // TODO: Implement a per-level approach
        score = 0;
        gameStatus = GameStatus.PLAY;
        overlay.enabled = false;
        Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
    }

    private void Update()
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

    // TODO: Create a OverlayManager to handle this.
    // TODO: Create a Collision Manager to handle this.

    public void HandleCollision(GameObject sender, GameObject other)
    {
        // TODO: collisionManager.EvaluateCollision(sender, other)

        // TODO: Rename to object1, object2

        if ((sender.CompareTag(Constants.Collision.Tags.player) && other.CompareTag(Constants.Collision.Tags.gem))
            || (sender.CompareTag(Constants.Collision.Tags.gem) && other.CompareTag(Constants.Collision.Tags.player)))
        {
            CollectableItem item = sender.GetComponent<CollectableItem>();
            if (other.CompareTag(Constants.Collision.Tags.gem))  // TODO: Change this tag to Item
            {
                item = other.GetComponent<CollectableItem>();
            }

            HandlePlayerGetGem(item);
        }

        if ((sender.CompareTag(Constants.Collision.Tags.player) && other.CompareTag(Constants.Collision.Tags.exit))   // TODO: Improve this . Create a method that returns an Enum.
            || (sender.CompareTag(Constants.Collision.Tags.exit) && other.CompareTag(Constants.Collision.Tags.player)))
        {
            Player player = sender.GetComponent<Player>();
            if (other.CompareTag(Constants.Collision.Tags.player))
            {
                player = other.GetComponent<Player>();
            }

            HandlePlayerExit(player);
        }

        if ((sender.CompareTag(Constants.Collision.Tags.player) && other.CompareTag(Constants.Collision.Tags.enemy))   // TODO: Improve this . Create a method that returns an Enum.
            || (sender.CompareTag(Constants.Collision.Tags.enemy) && other.CompareTag(Constants.Collision.Tags.player)))
        {
            Player player = sender.GetComponent<Player>();
            if (other.CompareTag(Constants.Collision.Tags.player))
            {
                player = other.GetComponent<Player>();
            }

            HandlePlayerDie(player);
        }
    }

    private void HandlePlayerGetGem(CollectableItem item)
    {
        score += item.points;
        SoundManager.instance.PlayFxItem(item.collectFx);
        Destroy(item.gameObject);
    }

    private void HandlePlayerExit(Player player)
    {
        player.LevelCompleted();
        SoundManager.instance.PlayFxItem(player.fxWin);
    }

    private void HandlePlayerDie(Player player)
    {
        player.PlayerDie();
        Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy);
        SoundManager.instance.PlayFxPlayer(player.fxDie);
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
