using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class DeathZone : MonoBehaviour
{
    [SerializeField] private float delay = 1.5f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) StartCoroutine(ReloadAfterDelay(other.gameObject));
    }

    private IEnumerator ReloadAfterDelay(GameObject player)
    {
        player.GetComponent<Collider>().enabled = false;
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}