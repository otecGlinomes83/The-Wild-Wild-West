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

    [SerializeField] private float _minPitch = 0.95f;
    [SerializeField] private float _maxPitch = 1.125f;

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
        PlaySoundWithParameters(_shotSound, 0.5f);
    }

    private void OnOutOfAmmo()
    {
        PlaySoundWithParameters(_clickSound);
    }

    private void OnReloadStarted()
    {
        _audioSource.PlayOneShot(_reloadSound);
    }

    private void OnReloadFinished()
    {
        PlaySoundWithParameters(_finishReloadSound);
    }

    private void PlaySoundWithParameters(AudioClip sound, float volume = 1f)
    {
        _audioSource.clip = sound;
        _audioSource.volume = volume;
        _audioSource.pitch = GetRandomPitch();
        _audioSource.Play();
    }

    private float GetRandomPitch() =>
        Random.Range(_minPitch, _maxPitch);
}