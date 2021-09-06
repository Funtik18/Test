using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SceneTransition : MonoBehaviour
{

    [SerializeField] private CanvasGroup canvasGroup;

    [SerializeField] private float fadeOutTime = 1f;
    [SerializeField] private float fadeInTime = 1f;

    private Coroutine transitionCoroutine = null;
    public bool IsTransitionProccess => transitionCoroutine != null;

    public void FadeIn(UnityAction onEnd = null)
    {
        StartCoroutine(Fade(0, 1, fadeInTime, onEnd));
    }
    public void FadeOut(UnityAction onEnd = null)
    {
        StartCoroutine(Fade(1, 0, fadeOutTime, onEnd));
    }

    private IEnumerator Fade(float start, float end, float fadeTime, UnityAction onEnd = null)
    {
        canvasGroup.blocksRaycasts = start == 1 ? true : false;

        float startTime = Time.time;
        float currentTime = Time.time - startTime;

        while (currentTime <= fadeTime)
        {
            currentTime = Time.time - startTime;

            canvasGroup.alpha = Mathf.Lerp(start, end, currentTime / fadeTime);

            yield return null;
        }

        canvasGroup.alpha = end;
        canvasGroup.blocksRaycasts = end == 1 ? true : false;

        onEnd?.Invoke();
    }


    [ContextMenu("Open")]
    private void Open()
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }
    [ContextMenu("Close")]
    private void Close()
    {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
}