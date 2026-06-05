using DG.Tweening;
using UnityEngine;

public class Rotator : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 360f;
    [SerializeField] private Ease ease = Ease.Linear;

    void Start()
    {
        transform.DORotate(new Vector3(0f, 0f, -360f), rotationSpeed / 360f, RotateMode.FastBeyond360)
            .SetEase(ease)
            .SetLoops(-1);
    }

    void OnDestroy()
    {
        transform.DOKill();
    }
}