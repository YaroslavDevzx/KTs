using UnityEngine;

public class ArrivalMessage : MonoBehaviour
{
    [SerializeField] private GameObject messagePanel;

    private bool _waiting;

    private void Start()
    {
        messagePanel.SetActive(true);
        _waiting = true;
    }

    private void Update()
    {
        if (_waiting && Input.anyKeyDown)
        {
            messagePanel.SetActive(false);
            _waiting = false;
        }
    }
}