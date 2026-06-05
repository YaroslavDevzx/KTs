using UnityEngine;
using UnityEngine.Events;

public class InformationTrigger : MonoBehaviour
{

    [SerializeField] private bool showsAgain = false;
    [SerializeField] private UnityEvent unityEvent;

    private bool wasShown = false;

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag != "Player") return;
        if (!showsAgain && wasShown) return;

        unityEvent?.Invoke();
        wasShown = true;
    }

}
