using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowGameOver : Window
{
    public UnityAction onOk;

    [SerializeField] private WindowScore windowScore;
    public WindowScore WindowScore => windowScore;

    [SerializeField] private Button ok;

    private void Awake()
    {
        ok.onClick.AddListener(Ok);
    }
    private void Ok()
    {
        onOk?.Invoke();
    }
}