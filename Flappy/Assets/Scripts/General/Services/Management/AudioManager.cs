using System.Collections.Generic;

using UnityEngine;

public class AudioManager : IAudioService
{
    private Transform parent;
    private Dictionary<string, AudioSource> audioDictionary = new Dictionary<string, AudioSource>();
    
    private DataHolder dataHolder;

    public AudioManager(DataHolder dataHolder)
    {
        this.dataHolder = dataHolder;

        Initialize();
    }

    public void PlaySound(string clipName)
    {
        if (dataHolder.IsSound)
        {
            FindAndPlayByName(clipName);
        }
    }

    private void Initialize()
    {
        parent = new GameObject("_AudioManager").transform;
        Object.DontDestroyOnLoad(parent.gameObject);

        AudioClip[] clips = SaveLoadManager.LoadAllAudio();
        for (int i = 0; i < clips.Length; i++)
        {
            AudioSource source = new GameObject("_AudioSource-" + clips[i].name).AddComponent<AudioSource>();
            source.transform.parent = parent;
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