using NaughtyAttributes;
using System;
using System.Collections;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private IAttackStrategy _attackStrategy;
    private IReloader _reloader;
    private IDamageableDetector _detector;

    private Magazine _magazine;
    private AttackData _attackData;
    private ReloadData _reloadData;
    private WaitForSecondsRealtime _cooldown;

    private bool _isAttackAble;

    public event Action<HitInfo> Hit;
    public event Action AttackPerformed;
    public event Action EmptyShot;
    public event Action ReloadStarted;
    public event Action AmmoLoad;
    public event Action ReloadFinished;

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

        _cooldown = new WaitForSecondsRealtime(_attackData.AttackRate);
        _isAttackAble = true;

        _detector.Hit += OnHit;

        if (_reloader != null)
        {
            if (_reloader is IInterruptReloader interruptable)
                interruptable.AmmoLoaded += OnAmmoLoaded;

            _reloader.ReloadStarted += OnReloadStarted;
            _reloader.ReloadFinished += OnReloadFinished;
        }

        Debug.Log($"<color=green>Setup complete! {gameObject.name}</color>");
    }

    [Button]
    public void PerformAttack()
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


    [Button]
    public void TryReload()
    {
        if (_reloadData.ReloadType == ReloadType.NonReload)
        {
            Debug.LogWarning($"<color=green> NonReloadable! {gameObject.name}</color>");
            return;
        }

        _reloader.TryReload(_magazine);
    }

    public bool TryInterruptReload()
    {
        if (_reloader is IInterruptReloader interruptReloader == false)
        {
            Debug.LogWarning($"<color=green> NonInterruptable! {gameObject.name}</color>");
            return false;
        }

        interruptReloader.Interrupt();
        return true;
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

    private IEnumerator WaitCooldown(WaitForSecondsRealtime cooldown)
    {
        _isAttackAble = false;
        yield return cooldown;
        _isAttackAble = true;
    }
}
