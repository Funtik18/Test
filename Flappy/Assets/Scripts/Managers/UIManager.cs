using UnityEngine;
using UnityEngine.Events;

using Zenject;

public class UIManager : MonoBehaviour
{
    public UnityAction onPlay;
    public UnityAction onPause;

    public UnityAction onWindowPauseClosed;

    [SerializeField] private WindowStart windowStart;
    [SerializeField] private WindowGame windowGame;
    [SerializeField] private WindowGameOver windowGameOver;
    [Space]
    [SerializeField] private SceneTransition sceneTransition;

    public WindowGameOver WindowGameOver => windowGameOver;

    private ScenesManager scenesManager;
    private AudioManager audioManager;

    [Inject]
    public void Construct(ScenesManager scenesManager, AudioManager audioManager)
    {
        this.scenesManager = scenesManager;
        this.audioManager = audioManager;
    }

    private void Awake()
    {
        windowStart.onStart += Play;
        
        windowGame.onPause += Pause;

        windowGame.WindowPause.onOk += WindowPauseOk;
        windowGame.WindowPause.onMenu += RestartGame;
        windowGame.WindowPause.onMusicChanged += MusicChanged;
        windowGame.WindowPause.onSoundChanged += SoundChanged;
        windowGame.WindowPause.onVibrationChanged += VibrationChanged;
        windowGame.WindowPause.Init(DataManager.IsMusic, DataManager.IsSound, DataManager.IsVibration);

        windowGameOver.onOk += RestartGame;

        sceneTransition.FadeOut();

        audioManager.Ready();
    }

    public void SetScore(string text)
    {
        windowGame.SetScore(text);
    }

    public void ShowGameOver()
    {
        windowGame.Close();
        windowGameOver.Open();
    }


    private void MusicChanged(bool trigger)
    {
        DataManager.IsMusic = trigger;
    }
    private void SoundChanged(bool trigger)
    {
        DataManager.IsSound = trigger;
    }
    private void VibrationChanged(bool trigger)
    {
        DataManager.IsVibration = trigger;
    }

    private void Play()
    {
        onPlay?.Invoke();
        windowStart.Close();
        windowGame.Open();
    }

    private void WindowPauseOk()
    {
        onWindowPauseClosed?.Invoke();

        windowGame.EnablePauseButton(true);
        windowGame.WindowPause.Close();
    }
    private void Pause()
    {
        windowGame.EnablePauseButton(false);
        windowGame.WindowPause.Open();
        onPause?.Invoke();
    }
    private void RestartGame()
    {
        sceneTransition.FadeIn(scenesManager.ReloadCurrentScene);
    }
}