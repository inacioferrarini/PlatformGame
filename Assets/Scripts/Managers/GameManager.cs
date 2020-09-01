using UnityEngine;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager
{
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new GameManager();
            }
            return instance;
        }
    }

    private GameManager()
    {
        collisionManager = new CollisionManager(this);
        overlayManager = new OverlayManager(this);
        levelManager = new LevelManager(this);
    }

    public enum GameStatus
    {
        WIN, LOSE, DIE, PLAY
    }

    private float time;
    private int score;
    public bool IsTimeOver { get; set; }

    private LevelObjects levelObjects;
    public void SetLevelObjects(LevelObjects levelObjects)
    {
        this.levelObjects = levelObjects;
    }

    private Player player;
    public void SetPlayer(Player player)
    {
        this.player = player;
    }

    private GameStatus gameStatus;
    private CollisionManager collisionManager;
    private OverlayManager overlayManager;
    private LevelManager levelManager;

    public float RemainingTime()
    {
        return time;
    }

    public void ResetLevel(float time)
    {
        IsTimeOver = false;
        this.time = time;
        score = 0;
        SetGameStatus(GameStatus.PLAY);
    }

    public void Update()
    {
        if (((int)GameManager.instance.RemainingTime() <= 0) && !IsTimeOver)
        {
            IsTimeOver = true;
            player.PlayerDie();
        }

        if (gameStatus == GameStatus.PLAY)
        {
            time -= Time.deltaTime;
            int timeInt = (int)time;

            if (timeInt >= 0)
            {
                levelObjects.timeHudText.text = string.Format("Time: {0}", timeInt);
                levelObjects.scoreHudText.text = string.Format("Score: {0}", score);
            }
        }
        else if (Input.GetButton(Constants.Input.Keys.jump))
        {
            if (gameStatus == GameStatus.WIN)
            {
                Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
                levelManager.LoadNextLevel();
            }
            else
            {
                Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
                levelManager.RestartLevel();
            }
        }
    }

    public void HandleCollision(GameObject object1, GameObject object2)
    {
        collisionManager.HandleCollision(object1, object2);
    }

    public void SetGameStatus(GameStatus p_gameStatus)
    {
        gameStatus = p_gameStatus;
        overlayManager.SetOverlay();
    }

}