using UnityEngine;

public class FallThroughButton : MonoBehaviour
{
    [SerializeField] private Collider floorCollider;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (floorCollider != null) floorCollider.enabled = false;
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        if (floorCollider != null) floorCollider.enabled = true;
    }
}