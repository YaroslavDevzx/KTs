using System;
using UnityEngine;

public class InputReader : MonoBehaviour
{
    public event Action<Vector2> MovePerformed;
    public event Action JumpPerformed;
    public event Action<Vector2> LookPerformed;
    public event Action<float> ZoomPerformed;
    public event Action<Vector2> DrivePerformed;

    private PlayerControls controls;

    private void Awake()
    {
        controls = new PlayerControls();

        controls.Player.Move.performed += ctx => MovePerformed?.Invoke(ctx.ReadValue<Vector2>());
        controls.Player.Move.canceled += _ => MovePerformed?.Invoke(Vector2.zero);
        controls.Player.Jump.performed += _ => JumpPerformed?.Invoke();
        controls.Player.Look.performed += ctx => LookPerformed?.Invoke(ctx.ReadValue<Vector2>());
        controls.Player.Zoom.performed += ctx => ZoomPerformed?.Invoke(ctx.ReadValue<float>());

        controls.Vehicle.Drive.performed += ctx => DrivePerformed?.Invoke(ctx.ReadValue<Vector2>());
        controls.Vehicle.Drive.canceled += _ => DrivePerformed?.Invoke(Vector2.zero);
    }

    public void EnablePlayerMap()
    {
        controls.Vehicle.Disable();
        controls.Player.Enable();
    }

    public void EnableVehicleMap()
    {
        controls.Player.Disable();
        controls.Vehicle.Enable();
    }

    private void OnEnable()
    {
        controls.Player.Enable();
        controls.Vehicle.Disable();
    }

    private void OnDisable() => controls.Disable();
}