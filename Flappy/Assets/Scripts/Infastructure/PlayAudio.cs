using UnityEngine;
using Zenject;

public class PlayAudio : MonoBehaviour
{
    [SerializeField] private string soundName = "";
    private AudioManager audio;

    [Inject]
    public void Constructor(AudioManager audioManager)
    {
        audio = audioManager;
    }

    public void Play()
    {
        audio.PlaySound(soundName);
    }
}