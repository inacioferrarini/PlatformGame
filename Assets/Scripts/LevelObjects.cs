using UnityEngine;
using UnityEngine.UI;

public class LevelObjects : MonoBehaviour
{
    public int timeToComplete;
    public AudioSource playerAudioPlayer;
    public AudioSource itemAudioPlayer;
    public Sprite winOverlaySprite;
    public Sprite loseOverlaySprite;
    public Sprite dieOverlaySprite;
    public Image overlay;
    public Text timeHudText;
    public Text scoreHudText;

    private void Awake()
    {
        SoundManager.Instance.SetLevelObjects(gameObject.GetComponent<LevelObjects>());
        GameManager.Instance.SetLevelObjects(gameObject.GetComponent<LevelObjects>());
    }

}
