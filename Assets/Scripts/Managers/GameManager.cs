using UnityEngine;

/// <summary>
/// Manages the Game itself.
/// </summary>
public partial class GameManager
{
    private static GameManager mp_instance = null;
    public static GameManager instance
    {
        get
        {
            if (mp_instance == null)
            {
                mp_instance = new GameManager();
            }
            return mp_instance;
        }
    }

    private GameManager()
    {
        mp_collisionManager = new CollisionManager(this);
        mp_overlayManager = new OverlayManager(this);
        mp_levelManager = new LevelManager(this);
        mp_inputManager = new InputManager(this);
    }

    public enum GameStatus
    {
        WIN, LOSE, DIE, PLAY
    }

    private float mp_time;
    private int mp_score;
    public bool IsTimeOver { get; set; }

    private LevelObjects mp_levelObjects;
    public void SetLevelObjects(LevelObjects p_levelObjects)
    {
        mp_levelObjects = p_levelObjects;
    }

    private Player mp_player;
    public void SetPlayer(Player p_player)
    {
        mp_player = p_player;
    }

    private GameStatus mp_gameStatus;
    private CollisionManager mp_collisionManager;
    private OverlayManager mp_overlayManager;
    private LevelManager mp_levelManager;
    private InputManager mp_inputManager;

    public float RemainingTime()
    {
        return mp_time;
    }

    public void ResetLevel(float p_time)
    {
        IsTimeOver = false;
        mp_time = p_time;
        mp_score = 0;
        SetGameStatus(GameStatus.PLAY);
    }

    public void Update()
    {
        //mp_inputManager.HandleUserInput();

        if (((int)GameManager.instance.RemainingTime() <= 0) && !IsTimeOver)
        {
            IsTimeOver = true;
            mp_player.PlayerDie();
        }

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
                mp_levelManager.LoadNextLevel();
            }
            else
            {
                Physics2D.IgnoreLayerCollision(Constants.Collision.Layers.player, Constants.Collision.Layers.enemy, false);
                mp_levelManager.RestartLevel();
            }
        }
    }

    public void HandleCollision(GameObject object1, GameObject object2)
    {
        mp_collisionManager.HandleCollision(object1, object2);
    }

    public void SetGameStatus(GameStatus p_gameStatus)
    {
        mp_gameStatus = p_gameStatus;
        mp_overlayManager.SetOverlay();
    }

}