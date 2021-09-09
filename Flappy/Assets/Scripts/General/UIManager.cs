using System.Collections;

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
    [SerializeField] private WindowPause windowPause;
    [SerializeField] private WindowGameOver windowGameOver;
    [Space]
    [SerializeField] private CanvasGroup fader;

    public WindowGameOver WindowGameOver => windowGameOver;

    private IScenesService scenesService;
    private IAudioService audioService;
    private DataHolder dataHolder;

    private ITransition sceneTransition;

    [Inject]
    public void Construct(IScenesService scenesService, IAudioService audioService, DataHolder dataHolder)
    {
        this.scenesService = scenesService;
        this.audioService = audioService;
        this.dataHolder = dataHolder;

        windowPause.Initialize(dataHolder.IsMusic, dataHolder.IsSound, dataHolder.IsVibration);
        windowPause.onMusicChanged += (x) => { PlayUISound(); };
        windowPause.onSoundChanged += (x) => { PlayUISound(); };
        windowPause.onVibrationChanged += (x) => { PlayUISound(); };

        windowPause.onMusicChanged += dataHolder.ChandeMusic;
        windowPause.onSoundChanged += dataHolder.ChandeSound;
        windowPause.onVibrationChanged += dataHolder.ChandeVibration;

        windowStart.onStart += PlayUISound;
        windowGame.onPause += PlayUISound;
        windowPause.onOk += PlayUISound;
        windowPause.onMenu += PlayUISound;
        windowGameOver.onOk += PlayUISound;
    }

    private void Awake()
    {
        windowStart.onStart += Play;
        
        windowGame.onPause += Pause;

        windowPause.onOk += WindowPauseOk;
        windowPause.onMenu += RestartGame;
        
        windowGameOver.onOk += RestartGame;

        sceneTransition = new FadeTransition(this, fader);
        sceneTransition.onIn += ReloadScene;
        sceneTransition.TransitionOut();
    }

    public void SetGameScore(string text)
    {
        windowGame.SetScore(text);
    }
    public void TryUpdateBestScore(int score)
    {
        StartCoroutine(UpdateScore(score));
    }

    public void ShowGameOver()
    {
        windowGame.Close();
        windowGameOver.Open();
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
        windowPause.Close();
    }
    private void Pause()
    {
        windowGame.EnablePauseButton(false);
        windowPause.Open();
        onPause?.Invoke();
    }

    private IEnumerator UpdateScore(int score)
    {
        int lastscoreBest = dataHolder.ScoreBest;
        windowGameOver.SetBestScore(lastscoreBest.ToString());

        if (lastscoreBest < score)
        {
            dataHolder.ScoreBest = score;
        }

        windowGameOver.SetScore("0");

        ShowGameOver();

        yield return new WaitForSeconds(0.5f);


        for (int i = 1; i <= score; i++)
        {
            windowGameOver.SetScore(i.ToString());

            if (lastscoreBest < i)
            {
                windowGameOver.SetBestScore(i.ToString());
            }

            yield return new WaitForSeconds(0.2f);
        }
    }

    private void PlayUISound()
    {
        audioService.PlaySound("Click");
    }

    private void RestartGame()
    {
        sceneTransition.TransitionIn();
    }
    private void ReloadScene()
    {
        scenesService.ReloadCurrentScene();
    }
}