using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioSource MusicSource;
    public static SoundController instance = null;

    /// <summary>
    /// Awake is called when the script instance is being loaded.
    /// </summary>
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else if(instance !=this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    public void Play(AudioClip clip)
    {
        MusicSource.clip = clip;
        MusicSource.Play();
    }
}
