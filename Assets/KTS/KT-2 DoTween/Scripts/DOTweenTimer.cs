using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class DOTweenTimer : MonoBehaviour
{
    [SerializeField] private float duration = 10f;
    [SerializeField] private Slider timerSlider;

    private void Start()
    {
        timerSlider.value = 1f;

        DOTween.To(() => timerSlider.value, x => timerSlider.value = x, 0f, duration).SetEase(Ease.Linear).OnComplete(() => Debug.Log("Конце таймера"));
    }
}