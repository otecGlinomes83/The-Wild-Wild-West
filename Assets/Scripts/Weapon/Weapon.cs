using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private IAttackStrategy _attackStrategy;
    private IReloader _reloader;
    private IDamageableDetector _detector;

    private Magazine _magazine;
    private AttackData _attackData;
    private ReloadData _reloadData;
    private WaitForSeconds _cooldown;
    private WaitForSeconds _autoAttackCooldown;

    private AttackType _attackType;

    private bool _isSetupFinished;
    private bool _isAttackAble;

    private Coroutine _attackCoroutine;

    private List<IDamageable> _attackedTargets = new List<IDamageable>();

    public event Action<HitInfo> Hit;
    public event Action AttackPerformed;
    public event Action EmptyShot;
    public event Action ReloadStarted;
    public event Action AmmoLoad;
    public event Action ReloadFinished;

    public AttackType AttackType => _attackType;

    private void OnEnable()
    {
        TrySubscribe();
    }

    private void OnDisable()
    {
        _detector.Hit -= OnHit;

        if (_reloader != null)
        {
            if (_reloader is IInterruptReloader interruptable)
                interruptable.AmmoLoaded -= OnAmmoLoaded;

            _reloader.ReloadStarted -= OnReloadStarted;
            _reloader.ReloadFinished -= OnReloadFinished;
        }
    }

    public void Setup(AttackData attackData, ReloadData reloadData, IAttackStrategy attackStrategy, IDamageableDetector detector, Magazine magazine = null, IReloader reloader = null)
    {
        _attackData = attackData;
        _reloadData = reloadData;

        _detector = detector;
        _attackStrategy = attackStrategy;
        _reloader = reloader;
        _magazine = magazine;

        _attackType = _attackData.AttackType;

        _cooldown = new WaitForSeconds(_attackData.AttackRate);
        _autoAttackCooldown = new WaitForSeconds(0.05f);
        _isAttackAble = true;

        _isSetupFinished = true;

        TrySubscribe();
    }

    public void StartAttacking()
    {
        if (_attackCoroutine != null)
            return;

        _attackCoroutine = StartCoroutine(AutoAttack());
    }

    public void StopAttacking()
    {
        if (_attackCoroutine == null)
            return;

        _attackedTargets.Clear();

        StopCoroutine(_attackCoroutine);
        _attackCoroutine = null;
    }

    public void TryAttack()
    {
        if (_isAttackAble == false)
            return;

        if (_magazine != null && _magazine.IsEmpty())
        {
            EmptyShot?.Invoke();
            return;
        }

        if (_reloader != null && _reloader.IsReloading && TryInterruptReload() == false)
            return;

        _magazine?.SpendAmmo();

        _attackStrategy.Attack(_detector.Detect());
        AttackPerformed?.Invoke();

        StartCoroutine(WaitCooldown(_cooldown));
    }

    public void TryReload()
    {
        if (_reloadData.ReloadType == ReloadType.NonReload)
        {
            return;
        }

        _reloader.TryReload(_magazine);
    }

    private bool TryInterruptReload()
    {
        if (_reloader is IInterruptReloader interruptReloader == false)
        {
            return false;
        }

        interruptReloader.Interrupt();
        return true;
    }

    private void TrySubscribe()
    {
        if (_isSetupFinished == false)
            return;

        _detector.Hit += OnHit;

        if (_reloader != null)
        {
            if (_reloader is IInterruptReloader interruptable)
                interruptable.AmmoLoaded += OnAmmoLoaded;

            _reloader.ReloadStarted += OnReloadStarted;
            _reloader.ReloadFinished += OnReloadFinished;
        }
    }

    private void OnHit(HitInfo info)
    {
        Hit?.Invoke(info);
    }

    private void OnAmmoLoaded()
    {
        AmmoLoad?.Invoke();
    }

    private void OnReloadFinished()
    {
        ReloadFinished?.Invoke();
    }

    private void OnReloadStarted()
    {
        ReloadStarted?.Invoke();
    }

    private IEnumerator AutoAttack()
    {
        List<IDamageable> targetsToAttack = new List<IDamageable>();

        while (enabled)
        {
            yield return _autoAttackCooldown;

            targetsToAttack.Clear();

            List<IDamageable> foundTargets = _detector.Detect();

            foreach (IDamageable damageable in foundTargets)
            {
                if (_attackedTargets.Contains(damageable))
                    continue;

                targetsToAttack.Add(damageable);
                _attackedTargets.Add(damageable);
            }

            _attackStrategy.Attack(targetsToAttack);
        }
    }

    private IEnumerator WaitCooldown(WaitForSeconds cooldown)
    {
        _isAttackAble = false;
        yield return cooldown;
        _isAttackAble = true;
    }
}