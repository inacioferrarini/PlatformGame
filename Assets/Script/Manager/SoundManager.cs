using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    public AudioSource fxPlayer;
    public AudioSource fxItemmCollect;

    void Awake()
    {
        // TODO: Improve logic. Use singleton without using a GameObject.
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        DontDestroyOnLoad(gameObject);
    }

    public void PlayFxPlayer(AudioClip clip)
    {
        fxPlayer.clip = clip;
        fxPlayer.Play();
    }

    public void PlayFxItem(AudioClip clip)
    {
        fxItemmCollect.clip = clip;
        fxItemmCollect.Play();
    }


}
