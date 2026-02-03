using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    private PlayerInputHandler _inputHandler;
    private Mover _mover;
    private Rotator _rotator;
    private Aimer _aimer;
    private Jumper _jumper;

    private Inventory _inventory;
    private Weapon _currentWeapon;

    private Coroutine _updateCoroutine;

    private bool _isShouldRun = false;
    private bool _isSprintingInput = false;
    private bool _isAiming = false;

    private void OnDisable()
    {
        _inputHandler.AimButtonTriggered -= OnAimButtonTriggered;
        _inputHandler.JumpRequested -= OnJumpRequested;
        _inputHandler.SpringButtonTriggered -= OnSprintButtonTriggered;
        _inputHandler.AttackRequested -= OnAttackRequested;
        _inputHandler.ReloadRequested -= OnReloadRequested;
        _inputHandler.SwitchWeaponRequested -= OnSwitchWeaponRequested;

        StopCoroutine(_updateCoroutine);
    }

    public void Setup(PlayerInputHandler playerInputHandler, Inventory inventory, Mover mover, Rotator rotator, Aimer aimer, Jumper jumper)
    {
        _inputHandler = playerInputHandler;
        _inventory = inventory;
        _mover = mover;
        _rotator = rotator;
        _aimer = aimer;
        _jumper = jumper;

        _inputHandler.AimButtonTriggered += OnAimButtonTriggered;
        _inputHandler.JumpRequested += OnJumpRequested;
        _inputHandler.SpringButtonTriggered += OnSprintButtonTriggered;
        _inputHandler.AttackRequested += OnAttackRequested;
        _inputHandler.ReloadRequested += OnReloadRequested;
        _inputHandler.SwitchWeaponRequested += OnSwitchWeaponRequested;

        _currentWeapon = _inventory.GetCurrentWeapon();

        _updateCoroutine = StartCoroutine(UpdateState());
    }

    private void OnSwitchWeaponRequested()
    {
        _inventory.ReturnWeapon(_currentWeapon);
        _currentWeapon = _inventory.GetCurrentWeapon();
    }

    private void OnReloadRequested()
    {
        _currentWeapon.TryReload();
    }

    private void OnAttackRequested()
    {
        _currentWeapon.PerformAttack();
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

    private IEnumerator UpdateState()
    {
        yield return null;

        while (enabled)
        {
            _rotator.Rotate(_inputHandler.MouseDelta);
            _mover.Move(_inputHandler.MoveDirection, _isShouldRun);

            yield return null;
        }
    }
}
