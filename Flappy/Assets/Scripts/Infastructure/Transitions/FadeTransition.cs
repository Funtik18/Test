using System.Collections;

using UnityEngine;
using UnityEngine.Events;

public class FadeTransition : ITransition
{
    public event UnityAction onIn;
    public event UnityAction onOut;

    private Coroutine transitionCoroutine = null;
    public bool IsTransitionProccess => transitionCoroutine != null;

    private MonoBehaviour owner;
    private CanvasGroup canvasGroup;

    private float fadeOutTime = 1f;
    private float fadeInTime = 1f;

    public FadeTransition(MonoBehaviour owner, CanvasGroup canvasGroup, float inTime = 0.5f, float outTime = 0.25f)
    {
        this.owner = owner;
        this.canvasGroup = canvasGroup;

        fadeInTime = inTime;
        fadeOutTime = outTime;
    }


    public void TransitionIn()
    {
        FadeIn(onIn);
    }

    public void TransitionOut()
    {
        FadeOut(onOut);
    }


    private void FadeIn(UnityAction onEnd = null)
    {
        owner.StartCoroutine(Fade(0, 1, fadeInTime, onEnd));
    }
    private void FadeOut(UnityAction onEnd = null)
    {
        owner.StartCoroutine(Fade(1, 0, fadeOutTime, onEnd));
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

        StopFade();

        onEnd?.Invoke();
    }
    private void StopFade()
    {
        if (IsTransitionProccess)
        {
            owner.StopCoroutine(transitionCoroutine);
            transitionCoroutine = null;
        }
    }
}