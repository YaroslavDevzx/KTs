using System.Collections.Generic;
using UnityEngine;

public class ExplosiveBox : MonoBehaviour
{
    private Health health;

    [SerializeField] private GameObject explodeParticle;
    [SerializeField] private float explodeRadius = 2f;
    [SerializeField] private float explodeDamage = 10f;

    void Awake()
    {
        health = GetComponent<Health>();
        health.OnHealthChanged += Explode;
    }

    void Explode(float currentHealth, float maxHealth)
    {
        if (currentHealth > 0) return;

        health.OnHealthChanged -= Explode;

        Collider2D[] hits = Physics2D.OverlapCircleAll(transform.position, explodeRadius);
        List<Health> target = new List<Health>();

        foreach (Collider2D hit in hits)
        {
            Health h = hit.GetComponentInParent<Health>();
            if (h != null && !target.Contains(h)) target.Add(h);
        }

        foreach (Health target_ in target) target_.TakeDamage(explodeDamage);

        if (explodeParticle != null) Instantiate(explodeParticle, transform.position, Quaternion.identity);

        Destroy(gameObject);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, explodeRadius);
    }
}
