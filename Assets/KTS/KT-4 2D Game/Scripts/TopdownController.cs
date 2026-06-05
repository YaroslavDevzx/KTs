using UnityEngine;

public class TopdownController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float moveSpeed = 8f;
    [SerializeField] private float acceleration = 50f;
    [SerializeField] private float deceleration = 30f;

    [Header("Dash")]
    [SerializeField] private float dashSpeed = 25f;
    [SerializeField] private float dashDuration = 0.15f;
    [SerializeField] private float dashCooldown = 0.3f;
    [SerializeField] private AnimationCurve dashCurve = AnimationCurve.EaseInOut(0, 1, 1, 0.3f);

    private Rigidbody2D _rb;
    private Vector2 _input;
    private Vector2 _velocity;
    private Vector2 _dashDir;
    private float _dashTimer;
    private float _dashCooldownTimer;
    private bool _dashing;

    void Awake()
    {
        _rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        _input = new Vector2(h, v).normalized;

        if (_dashCooldownTimer > 0f) _dashCooldownTimer -= Time.deltaTime;


        if (_dashing)
        {
            _dashTimer -= Time.deltaTime;
            if (_dashTimer <= 0f) _dashing = false;
        }

        if (Input.GetKeyDown(KeyCode.LeftShift)) StartDash();
    }

    void FixedUpdate()
    {
        if (_dashing)
        {
            float t = 1f - (_dashTimer / dashDuration);
            _rb.linearVelocity = _dashDir * dashSpeed * dashCurve.Evaluate(t);
            return;
        }

        bool moving = _input.magnitude > 0.1f;
        Vector2 target = moving ? _input * moveSpeed : Vector2.zero;
        float rate = moving ? acceleration : deceleration;

        _velocity = Vector2.MoveTowards(_velocity, target, rate * Time.fixedDeltaTime);
        _rb.linearVelocity = _velocity;
    }

    void StartDash()
    {
        if (_dashing || _dashCooldownTimer > 0f) return;
        if (_input.magnitude < 0.1f) return;

        _dashing = true;
        _dashTimer = dashDuration;
        _dashCooldownTimer = dashCooldown;
        _dashDir = _input;
    }
}