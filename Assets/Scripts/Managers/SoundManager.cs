using UnityEngine;

public class SoundManager
{
    private static SoundManager instance = null;
    public static SoundManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = new SoundManager();
            }
            return instance;
        }
    }

    private SoundManager() { }
    private LevelObjects levelObjects;

    public void SetLevelObjects(LevelObjects levelObjects)
    {
        this.levelObjects = levelObjects;
    }

    public void PlayFxPlayer(AudioClip clip)
    {
        levelObjects.playerAudioPlayer.clip = clip;
        levelObjects.playerAudioPlayer.Play();
    }

    public void PlayFxItem(AudioClip clip)
    {
        levelObjects.itemAudioPlayer.clip = clip;
        levelObjects.itemAudioPlayer.Play();
    }

}
