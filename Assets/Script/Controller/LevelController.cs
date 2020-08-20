using UnityEngine;

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
