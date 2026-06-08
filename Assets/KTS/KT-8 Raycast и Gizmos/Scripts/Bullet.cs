using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 12f;
    [SerializeField] private float lifetime = 4f;
    [SerializeField] private LayerMask targetLayer;

    private void Start()
    {
        Destroy(gameObject, lifetime);
    }

    private void Update()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision col)
    {
        if ((targetLayer.value & (1 << col.gameObject.layer)) == 0) return;

        Destroy(col.gameObject);
        Destroy(gameObject);
    }
}