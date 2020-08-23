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

    private SoundManager() { }
    private LevelObjects mp_levelObjects;

    public void SetLevelObjects(LevelObjects p_levelObjects)
    {
        mp_levelObjects = p_levelObjects;
    }

    public void PlayFxPlayer(AudioClip p_clip)
    {
        mp_levelObjects.m_playerAudioPlayer.clip = p_clip;
        mp_levelObjects.m_playerAudioPlayer.Play();
    }

    public void PlayFxItem(AudioClip p_clip)
    {
        mp_levelObjects.m_itemAudioPlayer.clip = p_clip;
        mp_levelObjects.m_itemAudioPlayer.Play();
    }

}
