using System;
using UnityEngine;

public class PlayerInputHandler : MonoBehaviour
{
    private PlayerInput _playerInput;

    public event Action<bool> AimButtonTriggered;
    public event Action<bool> SpringButtonTriggered;
    public event Action JumpRequested;
    public event Action AttackRequested;
    public event Action ReloadRequested;

    public Vector2 MoveDirection { get; private set; }
    public Vector2 MouseDelta { get; private set; }
    public Vector2 MousePosition { get; private set; }

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

        _playerInput.Player.Sprint.performed += OnSprintPerformed;
        _playerInput.Player.Sprint.canceled += OnSprintCanceled;


        _playerInput.Player.Attack.performed += OnAttackPerformed;
        _playerInput.Player.Reload.performed += OnReloadPerformed;
        _playerInput.Player.Jump.performed += OnJumpPerformed;
    }

    private void OnDisable()
    {
        _playerInput.Player.Move.performed -= OnMovePerformed;
        _playerInput.Player.Move.canceled -= OnMoveCanceled;

        _playerInput.Player.Aim.performed -= OnAimPerformed;
        _playerInput.Player.Aim.canceled -= OnAimCanceled;

        _playerInput.Player.Sprint.performed -= OnSprintPerformed;
        _playerInput.Player.Sprint.canceled -= OnSprintCanceled;

        _playerInput.Player.Attack.performed -= OnAttackPerformed;
        _playerInput.Player.Reload.performed -= OnReloadPerformed;
        _playerInput.Player.Jump.performed -= OnJumpPerformed;
    }

    private void Update()
    {
        MousePosition = _playerInput.Player.MousePosition.ReadValue<Vector2>();
        MouseDelta = _playerInput.Player.Look.ReadValue<Vector2>();
    }

    public void EnableInput() =>
    _playerInput.Enable();

    public void DisableInput() =>
        _playerInput.Disable();

    private void OnReloadPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)=>
        ReloadRequested?.Invoke();

    private void OnAttackPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context)=>
        AttackRequested?.Invoke();

    private void OnJumpPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
        JumpRequested?.Invoke();

    private void OnSprintCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
    SpringButtonTriggered?.Invoke(false);

    private void OnSprintPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
        SpringButtonTriggered?.Invoke(true);

    private void OnAimCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
        AimButtonTriggered?.Invoke(false);

    private void OnAimPerformed(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
               AimButtonTriggered?.Invoke(true);

    private void OnMoveCanceled(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
        MoveDirection = Vector2.zero;

    private void OnMovePerformed(UnityEngine.InputSystem.InputAction.CallbackContext context) =>
        MoveDirection = context.ReadValue<Vector2>();
}
