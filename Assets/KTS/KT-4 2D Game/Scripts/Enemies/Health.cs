using System;
using System.Data;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Health : MonoBehaviour, IDamageable
{
    public event Action<float, float> OnHealthChanged;
    public event Action OnDeath;

    [SerializeField] private float health;
    [SerializeField] private bool destroyOnDeath = true;

    private float maxHealth;


    void Awake()
    {
        maxHealth = health;
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            if (gameObject.tag == "Player") SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            OnDeath?.Invoke();
            if (destroyOnDeath)
            {
                Destroy(gameObject);
            }
        }

        OnHealthChanged?.Invoke(health, maxHealth);
    }



}

public interface IDamageable
{
    void TakeDamage(float damage);
}