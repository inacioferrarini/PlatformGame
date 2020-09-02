using UnityEngine;

/// <summary>
/// Adds game loop capabilities. Is used to update game related objects during
/// the game loop.
/// </summary>
public class LevelController : MonoBehaviour
{
    public LevelObjects levelobjects;
    public Joystick joystick;

    private void Start()
    {
        joystick.enabled = Application.isMobilePlatform;
        GameManager.Instance.ResetLevel(levelobjects.timeToComplete);
    }

    private void Update()
    {
        GameManager.Instance.Update();
    }
}
