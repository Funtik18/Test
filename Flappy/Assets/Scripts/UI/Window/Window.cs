using UnityEngine;

public class Window : MonoBehaviour
{
    [SerializeField] private CanvasGroup canvasGroup;

    [ContextMenu("Open")]
    public void Open()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    [ContextMenu("Close")]
    public void Close()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}