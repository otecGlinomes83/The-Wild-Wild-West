using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Aimer))]
[RequireComponent(typeof(Jumper))]
public class Player : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    private Mover _mover;
    private Rotator _rotator;
    private Aimer _aimer;
    private Jumper _jumper;

    private void Awake()
    {
        _inputHandler = GetComponent<PlayerInputHandler>();
        _mover = GetComponent<Mover>();
        _rotator = GetComponent<Rotator>();
        _aimer = GetComponent<Aimer>();
        _jumper = GetComponent<Jumper>();
    }

    private void OnEnable()
    {
        _inputHandler.AimButtonTriggered += OnAimButtonTriggered;
        _inputHandler.JumRequested += OnJumpRequested;
    }

    private void OnDisable()
    {
        _inputHandler.AimButtonTriggered -= OnAimButtonTriggered;
        _inputHandler.JumRequested -= OnJumpRequested;
    }

    private void Update()
    {
        _mover.Move(_inputHandler.MoveDirection);
        _rotator.Rotate(_inputHandler.MouseDelta);
    }

    private void OnJumpRequested()
    {
        _jumper.TryJump();
    }

    private void OnAimButtonTriggered(bool isAiming)
    {
        if (isAiming)
            _aimer.TakeAim();
        else
            _aimer.StopAiming();
    }
}
