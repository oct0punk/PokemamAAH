using System;
using UnityEngine;

[Serializable]
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

    private void Update()
    {
        for (int i = 0; i < sounds.Length; i++)
        {
            if (sounds[i].source == null)
            {
                AudioSource s = gameObject.AddComponent<AudioSource>();
                s.clip = sounds[i].clip;
                s.loop = sounds[i].loop;
                sounds[i].source = s;
            }
            sounds[i].source.volume = sounds[i].volume;
            if (!sounds[i].source.loop)
                sounds[i].loop = true;
            if (!sounds[i].source.isPlaying)
                sounds[i].source.Play();
        }
    }

    public void Play(string clipName)
    {
        Sound s  = Array.Find(sounds, s => s.name == clipName);
        if (s.source != null)
        {
            if (!s.source.isPlaying) s.source.Play();
            if (s.loop)
                s.source.volume = s.volume;
            else
                s.source.Play(); 
        }
        else Debug.LogWarning("Sound " + clipName + " not found");
    }

    public void Stop(string clipName)
    {
        Sound s = Array.Find(sounds, s => s.name == clipName);
        if (s.source != null)
        {
            if (s.loop)
                s.source.volume = 0;
            else
                s.source.Stop();
        }
        else Debug.LogWarning("Sound " + clipName + " not found");
    }
}
