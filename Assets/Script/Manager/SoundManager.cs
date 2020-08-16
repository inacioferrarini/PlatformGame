using UnityEngine;

public class SoundManager
{
    private static SoundManager mp_instance = null;
    public static SoundManager instance
    {
        get
        {
            if (mp_instance == null)
            {
                mp_instance = new SoundManager();
            }
            return mp_instance;
        }
    }

    private AudioSource m_playerAudio;
    private AudioSource m_itemAudio;

    private SoundManager() { }

    public void SetPlayerAudioPlayer(AudioSource p_audioPlayer)
    {
        m_playerAudio = p_audioPlayer;
    }

    public void SetItemAudioPlayer(AudioSource p_audioPlayer)
    {
        m_itemAudio = p_audioPlayer;
    }

    public void PlayFxPlayer(AudioClip clip)
    {
        m_playerAudio.clip = clip;
        m_playerAudio.Play();
    }

    public void PlayFxItem(AudioClip clip)
    {
        m_itemAudio.clip = clip;
        m_itemAudio.Play();
    }

}
