using UnityEngine;
using UnityEngine.UI;

public class LevelObjects : MonoBehaviour
{
    public AudioSource m_playerAudioPlayer;
    public AudioSource m_itemAudioPlayer;
    public Sprite m_winOverlaySprite;
    public Sprite m_loseOverlaySprite;
    public Sprite m_dieOverlaySprite;
    public Image m_overlay;
    public Text m_timeHudText;
    public Text m_scoreHudText;

    private void Awake()
    {
        // Sound Manager
        SoundManager.instance.SetLevelObjects(gameObject.GetComponent<LevelObjects>());

        // Game Manager
        GameManager.instance.SetLevelObjects(gameObject.GetComponent<LevelObjects>());
    }

}
