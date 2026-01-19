using System;
using System.Collections;
using UnityEngine;

public class InterruptableReloader : MonoBehaviour, IInterruptReloader
{
    private ReloadData _data;
    private WaitForSecondsRealtime _loadCooldown;
    private Coroutine _loadCoroutine;

    private bool _isNeededToInterrupt = false;
    private bool _isReloading = false;

    public event Action AmmoLoaded;
    public event Action ReloadStarted;
    public event Action ReloadFinished;

    public bool IsReloading => _isReloading;

    public void Setup(ReloadData data)
    {
        _data = data;
        _loadCooldown = new WaitForSecondsRealtime(_data.AmmoLoadTime);
    }

    public void Interrupt()
    {
        if (_isReloading == false)
            return;

        _isNeededToInterrupt = true;
    }

    public void TryReload(Magazine magazineToReload)
    {
        if (_loadCoroutine != null)
            return;

        if (magazineToReload.CurrentAmmoCount >= magazineToReload.MaxAmmoCount)
            return;

        _loadCoroutine = StartCoroutine(Reload(_loadCooldown, magazineToReload));
    }

    private IEnumerator Reload(WaitForSecondsRealtime loadCooldown, Magazine magazine)
    {
        yield return null;

        _isReloading = true;
        ReloadStarted?.Invoke();

        while (magazine.CurrentAmmoCount < magazine.MaxAmmoCount)
        {
            yield return loadCooldown;

            if (_isNeededToInterrupt)
            {
                _isNeededToInterrupt = false;
                Debug.LogWarning($"<color=yellow> ReloadInterrupted!</color>");
                break;
            }

            magazine.AddAmmo();
            AmmoLoaded?.Invoke();
        }

        _isReloading = false;
        ReloadFinished?.Invoke();
        Debug.Log($"<color=yellow> ReloadFinished!</color>");

        _loadCoroutine = null;
        yield break;
    }
}
