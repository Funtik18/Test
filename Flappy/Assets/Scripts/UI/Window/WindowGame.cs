using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowGame : Window
{
    public UnityAction onPause;

    [SerializeField] private TMPro.TextMeshProUGUI score;
    [SerializeField] private Button pause;

    private void Awake()
    {
        pause.onClick.AddListener(Pause);
    }

    public void EnablePauseButton(bool trigger)
    {
        pause.gameObject.SetActive(trigger);
    }

    public void SetScore(string value)
    {
        score.text = value;
    }

    private void Pause()
    {
        onPause?.Invoke();
    }
}
