using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;

public class ShootingEnemy : MonoBehaviour
{
    [SerializeField] private float attackSpeed = 3f;
    [SerializeField] private float detectionRadius = 5f;
    [SerializeField] private float aimSpeed = 2f;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private Transform shootPoint;
    [SerializeField] private LayerMask enemyMask;
    [SerializeField] private float damage = 2.5f;

    private Transform player;
    private Rigidbody2D rb;
    private bool isShooting = false;
    private float shootCooldown = 0f;

    private Vector2 direction;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void Update()
    {
        AttackPlayer();
        HandleShooting();
    }

    void AttackPlayer()
    {
        if (!player) return;

        float distance = Vector2.Distance(transform.position, player.position);

        isShooting = distance <= detectionRadius;

        if (isShooting)
        {
            direction = (player.position - transform.position).normalized;
            float targetAngle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg - 90f;

            float smoothAngle = Mathf.LerpAngle(rb.rotation, targetAngle, Time.deltaTime * aimSpeed);
            rb.rotation = smoothAngle;
        }


    }

    void HandleShooting()
    {
        if (!isShooting || projectilePrefab == null) return;

        shootCooldown -= Time.deltaTime;
        if (shootCooldown <= 0f)
        {
            Shoot();
            shootCooldown = attackSpeed;
        }
    }

    void Shoot()
    {
        Projectile bullet = Instantiate(projectilePrefab, shootPoint.position, shootPoint.rotation).GetComponent<Projectile>();
        bullet.Shoot(damage, direction, enemyMask, this.gameObject);
    }


    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectionRadius);
    }
}
