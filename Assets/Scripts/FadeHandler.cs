using DG.Tweening;
using UnityEngine;

public class FadeHandler : MonoBehaviour
{
    [SerializeField] private CanvasGroup _canvasGroup;
    [SerializeField] private float _fadeDuration;

    private Tween _fadeTween;

    public void FadeOut()
    {
        Fade(0, _fadeDuration);
    }

    public void FadeIn()
    {
        Fade(1, _fadeDuration);
    }

    private void Fade(float value, float duration)
    {
        _fadeTween?.Kill();

        _fadeTween = _canvasGroup.DOFade(value, duration);
    }
}
