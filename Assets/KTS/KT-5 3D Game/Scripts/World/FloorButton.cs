using UnityEngine;
using UnityEngine.Events;

public class FloorButton : MonoBehaviour
{
    [SerializeField] private UnityEvent OnTriggerAction;
    [SerializeField] private UnityEvent OnTriggerExitAction;

    private int _count;

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Box")) return;
        _count++;
        OnTriggerAction?.Invoke();
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player") && !other.CompareTag("Box")) return;
        _count--;
        if (_count <= 0)
        {
            _count = 0;
            OnTriggerExitAction?.Invoke();
        }
    }
}