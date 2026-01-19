using System;
using System.Collections;
using UnityEngine;

public class NonInterruptReloader : MonoBehaviour, IReloader
{
    private ReloadData _data;
    private WaitForSecondsRealtime _reloadCooldown;
    private Coroutine _reloadCoroutine;

    private bool _isReloading;

    public event Action ReloadStarted;
    public event Action ReloadFinished;

    public bool IsReloading => _isReloading;

    public void Setup(ReloadData data)
    {
        _data = data;
        _reloadCooldown = new WaitForSecondsRealtime(_data.ReloadTime);
    }

    public void TryReload(Magazine magazineToReload)
    {
        if (_reloadCoroutine != null)
            return;

        if (magazineToReload.CurrentAmmoCount >= magazineToReload.MaxAmmoCount)
            return;

        _reloadCoroutine = StartCoroutine(Reload(_reloadCooldown, magazineToReload));
    }

    private IEnumerator Reload(WaitForSecondsRealtime reloadCooldown, Magazine magazine)
    {
        Debug.Log($"<color=yellow> ReloadStarted!</color>");
        ReloadStarted?.Invoke();
        _isReloading = true;

        yield return reloadCooldown;

        magazine.AddAmmo(magazine.MaxAmmoCount - magazine.CurrentAmmoCount);

        _isReloading = false;
        ReloadFinished?.Invoke();
        Debug.Log($"<color=yellow> ReloadFinished!</color>");

        _reloadCoroutine = null;
        yield break;
    }
}
