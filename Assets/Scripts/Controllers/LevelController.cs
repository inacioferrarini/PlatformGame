using UnityEngine;

/// <summary>
/// Adds game loop capabilities. Is used to update game related objects during
/// the game loop.
/// </summary>
public class LevelController : MonoBehaviour
{
    public LevelObjects m_levelobjects;

    private void Start()
    {
        GameManager.instance.ResetLevel(m_levelobjects.m_timeToComplete);
    }

    private void Update()
    {
        GameManager.instance.Update();
    }
}
