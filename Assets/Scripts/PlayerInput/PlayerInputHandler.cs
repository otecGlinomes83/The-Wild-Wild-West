using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<bool> AimButtonTriggered;
    public event Action JumRequested;

    public Vector2 MoveDirection { get; private set; }
    public Vector2 MouseDelta { get; private set; }

    private void Awake()
    {
        _playerInput = new PlayerInput();
        _playerInput.Enable();
    }

    private void OnEnable()
    {
        _playerInput.Player.Move.performed += OnMovePerformed;
        _playerInput.Player.Move.canceled += OnMoveCanceled;

        _playerInput.Player.Aim.performed += OnAimPerformed;
        _playerInput.Player.Aim.canceled += OnAimCanceled;

        _playerInput.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMovePerformed;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;

        _playerInput.Player.Aim.performed -= OnAimPerformed;
        _playerInput.Player.Aim.canceled -= OnAimCanceled;

        _playerInput.Player.Jump.performed -= OnJumpPerformed;

    }

    private void Update()
    {
        MouseDelta = _playerInput.Player.Look.ReadValue<Vector2>();
    }

    public void EnableInput() =>
    _playerInput.Enable();

    public void DisableInput() =>
        _playerInput.Disable();


    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        JumRequested?.Invoke();
    }

    private void OnAimCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
        AimButtonTriggered?.Invoke(false);

    private void OnAimPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
               AimButtonTriggered?.Invoke(true);

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveDirection = Vector2.zero;
    }

    private void OnMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)
    {
        MoveDirection = context.ReadValue<Vector2>();
    }
}
