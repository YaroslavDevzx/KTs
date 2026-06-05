using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _smoothTime = 0.25f;
    [SerializeField] private Vector3 _offset;

    private Vector3 _vel;

    void LateUpdate()
    {
        if (_target == null) return;

        Vector3 targetPos = _target.position + _offset;
        transform.position = Vector3.SmoothDamp(transform.position, targetPos, ref _vel, _smoothTime);
    }
}