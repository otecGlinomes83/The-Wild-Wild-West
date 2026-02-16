using System;
using System.Collections;
using UnityEngine;

public class InterruptableReloader : MonoBehaviour, IInterruptReloader
{
    private ReloadData _data;
    private WaitForSecondsRealtime _loadCooldown;
    private Coroutine _loadCoroutine;

    private bool _isReloading = false;

    public event Action AmmoLoaded;
    public event Action ReloadStarted;
    public event Action ReloadFinished;

    public bool IsReloading => _isReloading;

    private void OnDisable()
    {
        Interrupt();
    }

    public void Setup(ReloadData data)
    {
        _data = data;
        _loadCooldown = new WaitForSecondsRealtime(_data.AmmoLoadTime);
    }

    public void Interrupt()
    {
        if (_isReloading == false)
            return;

        StopCoroutine(_loadCoroutine);
        _loadCoroutine = null;

        _isReloading = false;

        ReloadFinished?.Invoke();
        Debug.Log($"<color=yellow> ReloadFinished!</color>");
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
        _isReloading = true;
        ReloadStarted?.Invoke();
        Debug.Log($"<color=yellow> ReloadStarted!</color>");

        while (magazine.CurrentAmmoCount < magazine.MaxAmmoCount)
        {
            yield return loadCooldown;

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
