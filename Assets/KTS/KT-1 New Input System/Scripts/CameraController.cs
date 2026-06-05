using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform target;
    [SerializeField] private float sensitivity = 0.15f;
    [SerializeField] private float smoothSpeed = 10f;
    [SerializeField] private float zoomSpeed = 2f;
    [SerializeField] private float minDistance = 2f;
    [SerializeField] private float maxDistance = 12f;
    [SerializeField] private float minPitch = -30f;
    [SerializeField] private float maxPitch = 70f;

    private float yaw;
    private float pitch = 20f;
    private float distance = 5f;
    private float targetDistance;

    private void Awake() => targetDistance = distance;

    private void OnEnable()
    {
        inputReader.LookPerformed += OnLook;
        inputReader.ZoomPerformed += OnZoom;
    }

    private void OnDisable()
    {
        inputReader.LookPerformed -= OnLook;
        inputReader.ZoomPerformed -= OnZoom;
    }

    private void LateUpdate()
    {
        if (target == null) return;

        distance = Mathf.Lerp(distance, targetDistance, smoothSpeed * Time.deltaTime);

        var rotation = Quaternion.Euler(pitch, yaw, 0f);
        transform.position = target.position + rotation * new Vector3(0f, 0f, -distance);
        transform.LookAt(target.position);
    }

    private void OnLook(Vector2 delta) { yaw += delta.x * sensitivity; pitch = Mathf.Clamp(pitch - delta.y * sensitivity, minPitch, maxPitch); }
    private void OnZoom(float scroll) => targetDistance = Mathf.Clamp(targetDistance - scroll * zoomSpeed, minDistance, maxDistance);
}