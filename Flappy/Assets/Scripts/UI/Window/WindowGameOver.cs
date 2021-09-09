using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowGameOver : Window
{
    public UnityAction onOk;

    [SerializeField] private TMPro.TextMeshProUGUI score;
    [SerializeField] private TMPro.TextMeshProUGUI scoreBest;

    [SerializeField] private Button ok;

    private void Awake()
    {
        ok.onClick.AddListener(Ok);
    }


    public void SetScore(string value)
    {
        score.text = value;
    }
    public void SetBestScore(string value)
    {
        scoreBest.text = value;
    }

    private void Ok()
    {
        onOk?.Invoke();
    }
}