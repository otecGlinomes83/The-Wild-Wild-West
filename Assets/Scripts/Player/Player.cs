using UnityEngine;

[RequireComponent(typeof(Mover))]
[RequireComponent(typeof(PlayerInputHandler))]
[RequireComponent(typeof(Rotator))]
[RequireComponent(typeof(Aimer))]
[RequireComponent(typeof(Jumper))]
public class Player : MonoBehaviour
{
    [SerializeField] private Gun _gun;

    private PlayerInputHandler _inputHandler;
    private Mover _mover;
    private Rotator _rotator;
    private Aimer _aimer;
    private Jumper _jumper;

    private bool _isShouldRun = false;
    private bool _isSprintingInput = false;
    private bool _isAiming = false;

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
        _inputHandler.JumpRequested += OnJumpRequested;
        _inputHandler.SpringButtonTriggered += OnSprintButtonTriggered;
        _inputHandler.AttackRequested += OnAttackRequested;
        _inputHandler.ReloadRequested += OnReloadRequested;
    }

    private void OnDisable()
    {
        _inputHandler.AimButtonTriggered -= OnAimButtonTriggered;
        _inputHandler.JumpRequested -= OnJumpRequested;
        _inputHandler.SpringButtonTriggered -= OnSprintButtonTriggered;
        _inputHandler.AttackRequested -= OnAttackRequested;
        _inputHandler.ReloadRequested -= OnReloadRequested;
    }

    private void Update()
    {
        _mover.Move(_inputHandler.MoveDirection, _isShouldRun);

        _rotator.Rotate(_inputHandler.MouseDelta);
    }

    private void OnReloadRequested()
    {
        _gun.TryReload();
    }

    private void OnAttackRequested()
    {
        _gun.TryMakeShot();
    }

    private void OnJumpRequested()
    {
        _jumper.TryJump();
    }

    private void OnAimButtonTriggered(bool isAiming)
    {
        _isAiming = isAiming;

        if (isAiming)
            _aimer.TakeAim();
        else
            _aimer.StopAiming();

        RecalculateRunState();
    }

    private void OnSprintButtonTriggered(bool isCanRun)
    {
        _isSprintingInput = isCanRun;
        RecalculateRunState();
    }

    private void RecalculateRunState()
    {
        _isShouldRun = _isSprintingInput && _isAiming == false;
    }
}
