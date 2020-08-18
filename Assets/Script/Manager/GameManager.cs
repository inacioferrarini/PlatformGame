using UnityEngine;
using UnityEngine.SceneManagement;

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
    }

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

    private GameStatus mp_gameStatus;

    private CollisionManager mp_collisionManager;
    private OverlayManager mp_overlayManager;

    public void ResetLevel(float p_time)
    {
        mp_time = p_time;
        mp_score = 0;
        SetGameStatus(GameStatus.PLAY);
    }

    public void Update() // TODO: Find a Better name
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