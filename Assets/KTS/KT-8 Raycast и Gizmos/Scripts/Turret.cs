using UnityEngine;

public class Turret : MonoBehaviour
{
    [SerializeField] private Transform turretHead;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float rayDistance = 20f;
    [SerializeField] private Color highlightColor = Color.red;
    [SerializeField] private float minPitch = -10f;
    [SerializeField] private float maxPitch = 45f;
    [SerializeField] private LayerMask highlightLayer;

    private Camera _cam;
    private Renderer _highlighted;
    private Color _originalColor;
    private Vector3 _gizmoEnd;
    private GameObject _highlightedObject;

    private void Awake()
    {
        _cam = Camera.main;
        _gizmoEnd = transform.forward * rayDistance;
    }

    private void Update()
    {
        AimAtCursor();
        UpdateHighlight();

        if (Input.GetMouseButtonDown(0)) Shoot();
    }

    private void AimAtCursor()
    {
        Ray ray = _cam.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, transform.position);

        if (!plane.Raycast(ray, out float dist)) return;

        Vector3 worldTarget = ray.GetPoint(dist);
        Vector3 flatDir = worldTarget - transform.position;
        flatDir.y = 0f;

        if (flatDir != Vector3.zero) transform.rotation = Quaternion.LookRotation(flatDir);

        Ray mouseRay = _cam.ScreenPointToRay(Input.mousePosition);
        Vector3 target3D;

        if (Physics.Raycast(mouseRay, out RaycastHit hit, 200f)) target3D = hit.point;
        else target3D = mouseRay.GetPoint(50f);

        Vector3 dirToTarget = target3D - turretHead.position;
        float pitch = -Mathf.Atan2(dirToTarget.y, new Vector2(dirToTarget.x, dirToTarget.z).magnitude) * Mathf.Rad2Deg;
        pitch = Mathf.Clamp(pitch, minPitch, maxPitch);

        turretHead.localRotation = Quaternion.Euler(pitch, 0f, 0f);
    }

    private void UpdateHighlight()
    {
        Ray ray = new Ray(shootPoint.position, shootPoint.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, rayDistance, highlightLayer))
        {
            _gizmoEnd = hit.point;
            GameObject hitObject = hit.collider.gameObject;

            if (hitObject != _highlightedObject)
            {
                ResetHighlight();
                _highlightedObject = hitObject;
                _highlighted = hitObject.GetComponent<Renderer>();

                if (_highlighted != null)
                {
                    _originalColor = _highlighted.material.color;
                    _highlighted.material.color = highlightColor;
                }
            }
        }
        else
        {
            _gizmoEnd = shootPoint.position + shootPoint.forward * rayDistance;
            ResetHighlight();
        }
    }

    private void ResetHighlight()
    {
        if (_highlighted != null)
        {
            _highlighted.material.color = _originalColor;
            _highlighted = null;
        }
        _highlightedObject = null;
    }

    private void Shoot()
    {
        Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation);
    }

    private void OnDrawGizmos()
    {
        if (shootPoint == null) return;
        Gizmos.color = Color.yellow;
        Gizmos.DrawLine(shootPoint.position, _gizmoEnd);
    }
}