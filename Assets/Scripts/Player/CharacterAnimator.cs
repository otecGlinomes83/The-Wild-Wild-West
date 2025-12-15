using System;
using UnityEngine;

public class CharacterAnimator : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private Mover _mover;
    [SerializeField] private Jumper _jumper;
    [SerializeField] private Gun _gun;

    [SerializeField] private float _dampTime = 0.08f;
    [SerializeField] private float _maxAnimatorSpeed = 1.2f;

    private int _hashVertical = Animator.StringToHash("Vertical");
    private int _hashHorizontal = Animator.StringToHash("Horizontal");
    private int _hashIsJump = Animator.StringToHash("IsJump");
    private int _hashShot = Animator.StringToHash("Shot");

    private float _runThreshold;
    private float _vertical;
    private float _horizontal;
    private float _speed;

    private float _minAnimatorSpeed = 1f;

    private void Awake()
    {
        _runThreshold = _mover.MaxWalkSpeed;
    }

    private void OnEnable()
    {
        _gun.Shot += OnShot;
    }

    private void OnDisable()
    {
        _gun.Shot -= OnShot;
    }

    private void Update()
    {
        Vector2 direction = _mover.CurrentDirection;
        _vertical = direction.y;
        _horizontal = direction.x;

        _speed = _mover.Speed;
    }

    private void LateUpdate()
    {
        _animator.SetFloat(_hashVertical, _vertical, _dampTime, Time.deltaTime);
        _animator.SetFloat(_hashHorizontal, _horizontal, _dampTime, Time.deltaTime);
        _animator.SetBool(_hashIsJump, _jumper.IsJump);

        if (_speed > _runThreshold)
        {
            float boostRate = (_speed - _runThreshold) / _runThreshold;
            float targetSpeed = _minAnimatorSpeed + boostRate;
            targetSpeed = Mathf.Clamp(targetSpeed, _minAnimatorSpeed, _maxAnimatorSpeed);
            _animator.speed = targetSpeed;
        }
        else
        {
            if (_animator.speed != _minAnimatorSpeed)
                _animator.speed = _minAnimatorSpeed;
        }
    }

    private void OnShot()
    {
        _animator.SetTrigger(_hashShot);
    }
}
