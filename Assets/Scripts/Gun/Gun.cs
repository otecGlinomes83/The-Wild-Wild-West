using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Shooter))]
[RequireComponent(typeof(Magazine))]
public class Gun : MonoBehaviour
{
    [SerializeField] private float _fireRate = 0.25f;
    [SerializeField] private float _reloadTime = 1f;

    private Shooter _shooter;
    private Magazine _magazine;

    private WaitForSecondsRealtime _fireRateCooldown;
    private WaitForSecondsRealtime _reloadCooldown;

    private bool _isAbleToShot = true;

    public event Action Shot;
    public event Action ReloadStarted;
    public event Action ReloadFinished;
    public event Action OutOfAmmo;
    public event Action WaitingFireRate;

    private void Awake()
    {
        _shooter = GetComponent<Shooter>();
        _magazine = GetComponent<Magazine>();

        _fireRateCooldown = new WaitForSecondsRealtime(_fireRate);
        _reloadCooldown = new WaitForSecondsRealtime(_reloadTime);
    }

    public void TryMakeShot()
    {
        if (_isAbleToShot == false)
            return;

        if (_magazine.CurrentAmmo <= 0)
        {
            OutOfAmmo?.Invoke();
            return;
        }

        Shot?.Invoke();
        _shooter.Shoot();
        _magazine.SpendAmmo();

        StartCoroutine(CooldownWaiter(_fireRateCooldown));
        WaitingFireRate?.Invoke();
    }

    public void TryReload()
    {
        if (_magazine.CurrentAmmo >= _magazine.MaxAmmo)
            return;

        ReloadStarted?.Invoke();
        StartCoroutine(CooldownWaiter(_reloadCooldown, () => OnReloadFinished()));
    }

    private void OnReloadFinished()
    {
        _magazine.Reload();
        ReloadFinished?.Invoke();
    }

    private IEnumerator CooldownWaiter(WaitForSecondsRealtime cooldown, Action onComplete = null)
    {
        _isAbleToShot = false;
        yield return cooldown;
        _isAbleToShot = true;
        onComplete?.Invoke();

        yield break;
    }
}
