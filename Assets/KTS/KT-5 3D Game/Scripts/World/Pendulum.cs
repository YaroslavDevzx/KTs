using UnityEngine;
using UnityEngine.SceneManagement;

public class Pendulum : MonoBehaviour
{
    [SerializeField] private float angle = 45f;
    [SerializeField] private float speed = 1f;

    private Quaternion _rotA;
    private Quaternion _rotB;

    private void Start()
    {
        _rotA = Quaternion.Euler(0f, 0f, angle);
        _rotB = Quaternion.Euler(0f, 0f, -angle);
    }

    private void Update()
    {
        transform.localRotation = Quaternion.Lerp(_rotA, _rotB, (Mathf.Sin(Time.time * speed) + 1f) / 2f);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}