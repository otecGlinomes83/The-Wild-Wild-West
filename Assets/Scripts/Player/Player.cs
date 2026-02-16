using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterAnimator _characterAnimator;
    private PlayerInputHandler _inputHandler;
    private Mover _mover;
    private Rotator _rotator;
    private Aimer _aimer;
    private Jumper _jumper;

    private Inventory _inventory;
    private Weapon _currentWeapon;

    private bool _isShouldRun = false;
    private bool _isSprintingInput = false;
    private bool _isAiming = false;
    private bool _isAttacking = false;

    private bool _isSetupFinished = false;

    private void OnDisable()
    {
        _inputHandler.AimButtonTriggered -= OnAimButtonTriggered;
        _inputHandler.JumpRequested -= OnJumpRequested;
        _inputHandler.SpringButtonTriggered -= OnSprintButtonTriggered;
        _inputHandler.AttackRequested -= OnAttackRequested;
        _inputHandler.ReloadRequested -= OnReloadRequested;
        _inputHandler.SwitchWeaponRequested -= OnSwitchWeaponRequested;
    }

    private void Update()
    {
        if (_isSetupFinished == false)
            return;

        _rotator.Rotate(_inputHandler.MouseDelta);
        _mover.Move(_inputHandler.MoveDirection, _isShouldRun);

        _characterAnimator.SetJumpState(_jumper.IsJump);
        _characterAnimator.UpdateMove(_mover.CurrentDirection, _mover.Speed, _mover.GetMaxSpeed());
    }

    public void Setup(CharacterAnimator characterAnimator, PlayerInputHandler playerInputHandler, Inventory inventory, Mover mover, Rotator rotator, Aimer aimer, Jumper jumper)
    {
        _inputHandler = playerInputHandler;
        _inventory = inventory;
        _mover = mover;
        _rotator = rotator;
        _aimer = aimer;
        _jumper = jumper;
        _characterAnimator = characterAnimator;

        _characterAnimator.AttackStarted += StartAttack;
        _characterAnimator.AttackFinished += FinishAttack;
        _characterAnimator.AttackPerformed += PerformAttack;

        _inputHandler.AimButtonTriggered += OnAimButtonTriggered;
        _inputHandler.JumpRequested += OnJumpRequested;
        _inputHandler.SpringButtonTriggered += OnSprintButtonTriggered;
        _inputHandler.AttackRequested += OnAttackRequested;
        _inputHandler.ReloadRequested += OnReloadRequested;
        _inputHandler.SwitchWeaponRequested += OnSwitchWeaponRequested;

        _currentWeapon = _inventory.GetCurrentWeapon();
        _characterAnimator.SetIdle(_currentWeapon.AttackType, _currentWeapon.transform);

        _isSetupFinished = true;
    }

    private void OnSwitchWeaponRequested()
    {
        _inventory.ReturnWeapon(_currentWeapon);
        _currentWeapon = _inventory.GetCurrentWeapon();
        _characterAnimator.SetIdle(_currentWeapon.AttackType, _currentWeapon.transform);
    }

    private void OnReloadRequested()
    {
        _currentWeapon.TryReload();
    }

    private void OnAttackRequested()
    {
        if (_isAttacking == true)
            return;

        _isAttacking = true;
        _characterAnimator.StartAttack(_currentWeapon.AttackType);
    }

    private void PerformAttack()
    {
        _currentWeapon.TryAttack();
        _isAttacking = false;
    }

    private void FinishAttack()
    {
        _currentWeapon.StopAttacking();
        _isAttacking = false;
    }

    private void StartAttack()
    {
        _currentWeapon.StartAttacking();
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
