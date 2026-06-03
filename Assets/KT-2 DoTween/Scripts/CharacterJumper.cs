
using DG.Tweening;
using UnityEngine;
using UnityEngine.InputSystem;

public class CharacterJumper : MonoBehaviour
{
    [SerializeField] private float jumpDistance = 3f;
    [SerializeField] private float jumpHeight = 1.5f;
    [SerializeField] private float jumpDuration = 0.5f;
    [SerializeField] private float rotateDuration = 0.2f;
    [SerializeField] private float rotateAngle = 90f;

    private bool isJumping;

    private void Update()
    {
        if (!isJumping && Input.GetKeyDown(KeyCode.Space)) Jump();

        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow)) Rotate(-rotateAngle);
        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow)) Rotate(rotateAngle);
    }

    private void Jump()
    {
        isJumping = true;
        var target = transform.position + transform.forward * jumpDistance;

        transform.DOJump(target, jumpHeight, 1, jumpDuration).SetEase(Ease.OutQuad).OnComplete(() => isJumping = false);
    }

    private void Rotate(float angle)
    {
        var targetY = transform.eulerAngles.y + angle;
        transform.DORotate(new Vector3(0f, targetY, 0f), rotateDuration).SetEase(Ease.OutCubic);
    }
}