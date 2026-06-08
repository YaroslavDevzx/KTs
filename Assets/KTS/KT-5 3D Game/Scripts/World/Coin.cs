using UnityEngine;

public class Coin : MonoBehaviour
{
    [SerializeField] private int value = 10;
    [SerializeField] private float collectRadius = 1.5f;
    [SerializeField] private float hoverAmplitude = 0.3f;
    [SerializeField] private float hoverSpeed = 2f;

    private Transform _player;
    private Vector3 _startPos;

    private void Start()
    {
        _startPos = transform.position;
        _player = GameObject.FindWithTag("Player").transform;
    }

    private void Update()
    {
        transform.position = _startPos + Vector3.up * (Mathf.Sin(Time.time * hoverSpeed) * hoverAmplitude);

        if (Vector3.Distance(transform.position, _player.position) < collectRadius)
        {
            WalletUI.Instance.Add(value);
            Destroy(gameObject);
        }
    }
}