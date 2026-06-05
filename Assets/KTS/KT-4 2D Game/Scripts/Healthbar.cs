using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Slider))]
public class Healthbar : MonoBehaviour
{
    [SerializeField] private Health health;
    [Space(5)]
    [SerializeField] private float duration = 0.25f;
    [SerializeField] private float fadeDuration = 0.5f;
    [SerializeField] private float hideTime = 2f;
    [SerializeField] private CanvasGroup canvasGroup;

    private Slider slider;
    private Tween fadeTween;
    private float maxHealth;

    void Awake()
    {
        slider = GetComponent<Slider>();
        health = GetComponentInParent<Health>();

        if (!slider || !health) enabled = false;

        if (health) health.OnHealthChanged += SetHealth;
        if (canvasGroup == null) canvasGroup = GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = gameObject.AddComponent<CanvasGroup>();

        canvasGroup.alpha = 0;
    }

    void Start()
    {
        maxHealth = slider.maxValue;
    }

    public void SetHealth(float current, float max)
    {
        maxHealth = max;

        float targetValue = current / max;

        slider.DOValue(targetValue, duration).SetEase(Ease.OutCubic);

        Show();

        if (current >= max) DOVirtual.DelayedCall(hideTime, Hide);
    }

    private void Show()
    {
        fadeTween?.Kill();
        fadeTween = canvasGroup.DOFade(1f, fadeDuration).SetEase(Ease.OutCubic);
    }

    private void Hide()
    {
        fadeTween?.Kill();
        fadeTween = canvasGroup.DOFade(0f, fadeDuration).SetEase(Ease.OutCubic);
    }

    void OnDestroy()
    {
        health.OnHealthChanged -= SetHealth;
    }

}
