using UnityEngine;

public class BoxPickup : MonoBehaviour
{
    [SerializeField] private float pickupRadius = 2f;
    [SerializeField] private KeyCode pickupKey = KeyCode.E;

    private Rigidbody heldBox;
    private Collider _playerCollider;

    private void Awake() => _playerCollider = GetComponent<Collider>();

    private void Update()
    {
        if (Input.GetKeyDown(pickupKey))
        {
            if (heldBox != null) Drop();
            else TryPickup();
        }

        if (heldBox != null)
            heldBox.MovePosition(transform.position + transform.forward * 1.5f);
    }

    private void TryPickup()
    {
        var cols = Physics.OverlapSphere(transform.position, pickupRadius);
        foreach (var col in cols)
        {
            if (!col.CompareTag("Box") && !col.CompareTag("WrongBox")) continue;
            heldBox = col.GetComponent<Rigidbody>();
            if (heldBox == null) continue;
            heldBox.isKinematic = true;
            Physics.IgnoreCollision(_playerCollider, col, true);
            return;
        }
    }

    private void Drop()
    {
        heldBox.isKinematic = false;
        Physics.IgnoreCollision(_playerCollider, heldBox.GetComponent<Collider>(), false);
        heldBox = null;
    }
}