using UnityEngine;

public class PlayerMovement3pp : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 10f;
    [SerializeField] private float jumpForce = 5f;
    [SerializeField] private float gravity = -9.81f;

    private CharacterController characterController;
    private Vector2 moveInput;
    private float verticalVelocity;

    private void Awake() => characterController = GetComponent<CharacterController>();

    private void OnEnable()
    {
        inputReader.MovePerformed += OnMove;
        inputReader.JumpPerformed += OnJump;
    }

    private void OnDisable()
    {
        inputReader.MovePerformed -= OnMove;
        inputReader.JumpPerformed -= OnJump;
    }

    private void Update()
    {
        if (characterController.isGrounded && verticalVelocity < 0f) verticalVelocity = -2f;
        verticalVelocity += gravity * Time.deltaTime;

        var camForward = Vector3.ProjectOnPlane(cameraTransform.forward, Vector3.up).normalized;
        var camRight = Vector3.ProjectOnPlane(cameraTransform.right, Vector3.up).normalized;
        var horizontal = camForward * moveInput.y + camRight * moveInput.x;

        if (horizontal.sqrMagnitude > 0.01f)
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(horizontal), rotationSpeed * Time.deltaTime);

        characterController.Move((horizontal * moveSpeed + Vector3.up * verticalVelocity) * Time.deltaTime);
    }

    private void OnMove(Vector2 input) => moveInput = input;
    private void OnJump() { if (characterController.isGrounded) verticalVelocity = jumpForce; }
}