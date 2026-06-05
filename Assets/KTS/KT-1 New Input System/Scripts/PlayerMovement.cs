using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private InputReader inputReader;
    [SerializeField] private Transform cameraTransform;
    [SerializeField] private float moveSpeed = 5f;
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
        var horizontal = (camForward * moveInput.y + camRight * moveInput.x);

        characterController.Move((horizontal * moveSpeed + Vector3.up * verticalVelocity) * Time.deltaTime);
    }

    private void OnMove(Vector2 input) => moveInput = input;
    private void OnJump() { if (characterController.isGrounded) verticalVelocity = jumpForce; }
}