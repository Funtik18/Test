using System.Collections;
using System.Collections.Generic;
using System.Linq;

using UnityEngine;

public class AudioManager : MonoBehaviour
{
    private static AudioManager instance;
    public static AudioManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<AudioManager>();

                if (instance == null)
                {
                    instance = new GameObject("_AudioManager").AddComponent<AudioManager>();
                }

                DontDestroyOnLoad(instance);

            }
            return instance;
        }
    }

    private Dictionary<string, AudioSource> audioDictionary = new Dictionary<string, AudioSource>();

    private bool isReady = false;

    private void Awake()
    {
        Init();
    }

    public void Ready()
    {
        isReady = true;
    }

    public void PlaySound(string clipName)
    {
        if (isReady)
        {
            if (DataManager.IsSound)
            {
                FindAndPlayByName(clipName);
            }
        }
    }

    private void Init()
    {
        AudioClip[] clips = SaveLoadManager.LoadAllAudio();
        for (int i = 0; i < clips.Length; i++)
        {
            AudioSource source = new GameObject("_AudioSource-" + clips[i].name).AddComponent<AudioSource>();
            source.transform.parent = transform;
            source.clip = clips[i];
            audioDictionary.Add(clips[i].name, source);
        }
    }
    private void FindAndPlayByName(string clipName)
    {
        AudioSource source;
        if(audioDictionary.TryGetValue(clipName, out source))
        {
            source.Play();
        }
    }
}