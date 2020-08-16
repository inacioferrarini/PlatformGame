using UnityEngine;

public class LevelObjects : MonoBehaviour
{
    public AudioSource m_playerAudioPlayer;
    public AudioSource m_itemAudioPlayer;

    private void Start()
    {
        SoundManager.instance.SetPlayerAudioPlayer(m_playerAudioPlayer);
        SoundManager.instance.SetItemAudioPlayer(m_itemAudioPlayer);
    }

}
