using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class GunSound : MonoBehaviour
{
    [SerializeField] private Gun _gun;

    [SerializeField] private AudioClip _shotSound;
    [SerializeField] private AudioClip _clickSound;
    [SerializeField] private AudioClip _fireRateCooldownSound;
    [SerializeField] private AudioClip _reloadSound;
    [SerializeField] private AudioClip _finishReloadSound;

     private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>(); 
    }

    private void OnEnable()
    {
        _gun.Shot += OnShot;
        _gun.OutOfAmmo += OnOutOfAmmo;
        _gun.WaitingFireRate += OnWaitingFireRate;
        _gun.ReloadStarted += OnReloadStarted;
        _gun.ReloadFinished += OnReloadFinished;
    }

    private void OnDisable()
    {
        _gun.Shot -= OnShot;
        _gun.OutOfAmmo -= OnOutOfAmmo;
        _gun.WaitingFireRate -= OnWaitingFireRate;
        _gun.ReloadStarted -= OnReloadStarted;
        _gun.ReloadFinished -= OnReloadFinished;
    }

    private void OnWaitingFireRate()
    {
        _audioSource.PlayOneShot(_fireRateCooldownSound);
    }

    private void OnShot()
    {
        _audioSource.PlayOneShot(_shotSound);
    }

    private void OnOutOfAmmo()
    {
        _audioSource.PlayOneShot(_clickSound);
    }

    private void OnReloadStarted()
    {
        _audioSource.PlayOneShot(_reloadSound);
    }

    private void OnReloadFinished()
    {
        _audioSource.PlayOneShot(_finishReloadSound);
    }
}