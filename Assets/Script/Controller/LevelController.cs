using UnityEngine;

public class LevelController : MonoBehaviour
{

    private void Start()
    {
        GameManager.instance.ResetLevel(31f); // TODO: Get the time from Level Data
    }

    private void Update()
    {
        GameManager.instance.Update();
    }
}
