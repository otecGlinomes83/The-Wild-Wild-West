using System;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class CharacterAnimator : MonoBehaviour
{
    private Animator _animator;
    private AnimatorData _animatorData;
    private AnimatorProxy _animatorProxy;

    private IKController _controller;

    public event Action AttackPerformed;

    private int _hashVertical = Animator.StringToHash("Vertical");
    private int _hashHorizontal = Animator.StringToHash("Horizontal");
    private int _hashIsJump = Animator.StringToHash("IsJump");
    private int _hashShot = Animator.StringToHash("Shot");
    private int _hashHit = Animator.StringToHash("Hit");
    private int _hashIsMelee = Animator.StringToHash("IsMelee");

    public void Setup(Animator animator, AnimatorData animatorData, AnimatorProxy animatorProxy, IKController iKController)
    {
        _animator = animator;
        _animatorData = animatorData;
        _animatorProxy = animatorProxy;
        _controller = iKController;

        _animatorProxy.AttackPerformed += OnAttackPerformed;
    }

    public void SetIdle(AttackType attackType, Transform weaponTransform)
    {
        switch (attackType)
        {
            case AttackType.Melee:
                _animator.SetBool(_hashIsMelee, true);
                break;

            case AttackType.Range:
                _animator.SetBool(_hashIsMelee, false);
                _controller.SetWeaponTransform(weaponTransform);
                break;

            default:
                _animator.SetBool(_hashIsMelee, true);
                break;
        }

        _controller.SetWeight(attackType);

    }

    public void UpdateMove(Vector2 currentDirection, float currentSpeed, float maxSpeed)
    {
        float vertical = currentDirection.y;
        float horizontal = currentDirection.x;

        _animator.SetFloat(_hashVertical, vertical, _animatorData.DampingTime, Time.deltaTime);
        _animator.SetFloat(_hashHorizontal, horizontal, _animatorData.DampingTime, Time.deltaTime);

        if (currentSpeed >= maxSpeed)
        {
            _animator.speed = _animator.speed * _animatorData.MaxAnimatorSpeed;
        }
        else
        {
            if (_animator.speed != _animatorData.DefaultAnimatorSpeed)
                _animator.speed = _animatorData.DefaultAnimatorSpeed;
        }
    }

    public void SetJumpState(bool isJump)
    {
        _animator.SetBool(_hashIsJump, isJump);
    }

    public void StartAttack(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Melee:
                _animator.SetTrigger(_hashHit);
                break;

            case AttackType.Range:
                _animator.SetTrigger(_hashShot);
                break;
        }
    }

    private void OnAttackPerformed() =>
        AttackPerformed?.Invoke();
}

public class IKController
{
    private TwoBoneIKConstraint _leftHandGrabWeapon;
    private MultiAimConstraint _handAim;
    private MultiAimConstraint _gunAim;

    private IKData _data;

    public void Setup(IKData data, TwoBoneIKConstraint twoBoneIKConstraint, MultiAimConstraint handAim, MultiAimConstraint gunAim)
    {
        _data = data;
        _leftHandGrabWeapon = twoBoneIKConstraint;
        _handAim = handAim;
        _gunAim = gunAim;
    }

    public void SetWeight(AttackType attackType)
    {
        switch (attackType)
        {
            case AttackType.Melee:
                ChangeWeight(0f);
                break;

            case AttackType.Range:
                SetWeightToDefault();
                break;

            default:
                break;
        }
    }

    public void SetWeaponTransform(Transform weaponTransform)
    {
        _gunAim.data.constrainedObject = weaponTransform;
    }

    private void SetWeightToDefault()
    {
        _leftHandGrabWeapon.weight = _data.HandGrabWeaponWeight;
        _handAim.weight = _data.HandAimWeight;
        _gunAim.weight = _data.WeaponAimWeight;
    }

    private void ChangeWeight(float rate)
    {
        _leftHandGrabWeapon.weight = rate;
        _handAim.weight = rate;
        _gunAim.weight = rate;
    }
}