using System;
using UnityEngine;

[System.Serializable]
public struct Sound
{
    public string name;
    public AudioClip clip;
    [Range(0.0f, 1.0f)]public float volume; 
    public bool loop;
    public bool playOnAwake;
    [HideInInspector]public AudioSource source;
}

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    public Sound[] sounds;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            if (instance != this)
            {
                Destroy(gameObject);
            }
        }

        for (int i = 0; i < sounds.Length; i++)
        {
            AudioSource source = sounds[i].source = gameObject.AddComponent<AudioSource>();
            source.clip = sounds[i].clip;
            source.volume = sounds[i].volume;
            source.loop = sounds[i].loop;
            if (sounds[i].playOnAwake)
            {
                source.playOnAwake = true;
                source.Play();
            }
        }
    }

    public void Play(string clipName)
    {
        Sound s  = Array.Find(sounds, s => s.name == clipName);
        if (s.source != null) s.source.Play();
        else Debug.LogWarning("Sound " + clipName + " not found");
    }

    public void Stop(string clipName)
    {
        Sound s = Array.Find(sounds, s => s.name == clipName);
        if (s.source != null) s.source.Stop();
        else Debug.LogWarning("Sound " + clipName + " not found");
    }
}
