using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

using Zenject;

public class WindowPause : Window
{
    public UnityAction<bool> onMusicChanged;
    public UnityAction<bool> onSoundChanged;
    public UnityAction<bool> onVibrationChanged;

    public UnityAction onOk;
    public UnityAction onMenu;

    [SerializeField] private Toggle toggleMusic;
    [SerializeField] private Toggle toggleSound;
    [SerializeField] private Toggle toggleVibration;
    [Space]
    [SerializeField] private Button buttonOk;
    [SerializeField] private Button buttonMenu;

    private void Awake()
    {
        toggleMusic.onValueChanged.AddListener(MusicChanged);
        toggleSound.onValueChanged.AddListener(SoundChanged);
        toggleVibration.onValueChanged.AddListener(VibrationChanged);

        buttonOk.onClick.AddListener(Ok);
        buttonMenu.onClick.AddListener(Menu);
    }

    public void Initialize(bool music, bool sound, bool vibration)
    {
        toggleMusic.isOn = music;
        toggleSound.isOn = sound;
        toggleVibration.isOn = vibration;
    }

    private void MusicChanged(bool trigger)
    {
        onMusicChanged?.Invoke(trigger);
    }
    private void SoundChanged(bool trigger)
    {
        onSoundChanged?.Invoke(trigger);
    }
    private void VibrationChanged(bool trigger)
    {
        onVibrationChanged?.Invoke(trigger);
    }

    private void Ok()
    {
        onOk?.Invoke();
    }
    private void Menu()
    {
        onMenu?.Invoke();
    }
}