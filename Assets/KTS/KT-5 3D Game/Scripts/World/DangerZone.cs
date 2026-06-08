using UnityEngine;
using UnityEngine.SceneManagement;

public class DangerZone : MonoBehaviour
{
    [SerializeField] private GameObject fallingObjectPrefab;
    [SerializeField] private float spawnHeight = 5f;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;
        var pos = other.transform.position + Vector3.up * spawnHeight;
        Instantiate(fallingObjectPrefab, pos, Quaternion.identity);
    }
}