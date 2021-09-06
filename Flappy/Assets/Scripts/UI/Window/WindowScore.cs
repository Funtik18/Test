using UnityEngine;

public class WindowScore : Window
{
    [SerializeField] private TMPro.TextMeshProUGUI score;
    [SerializeField] private TMPro.TextMeshProUGUI scoreBest;

    public void SetScore(string value)
    {
        score.text = value;
    }
    public void SetBestScore(string value)
    {
        scoreBest.text = value;
    }
}