using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(Rigidbody2D))]
public class ChasingEnemy : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float detectionRadius = 5f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isChasing = false;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update() => Chase();

    void Chase()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        isChasing = distance <= detectionRadius;

        Vector2 direction = (player.position - transform.position).normalized;
        rb.linearVelocity = isChasing ? direction * moveSpeed : Vector2.zero;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Player")) SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
