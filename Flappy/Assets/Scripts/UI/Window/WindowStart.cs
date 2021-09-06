using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class WindowStart : Window
{
    public UnityAction onStart;

    [SerializeField] private Button play;

    private void Awake()
    {
        play.onClick.AddListener(Click);
    }
    private void Click()
    {
        onStart?.Invoke();
    }
}