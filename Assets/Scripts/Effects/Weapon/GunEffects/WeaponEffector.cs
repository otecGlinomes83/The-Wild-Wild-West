   using UnityEngine;

public class WeaponEffector : MonoBehaviour
{
    private Weapon _weapon;
    private SoundData _soundData;
    private AudioPlayer _audioPlayer;

    private ShotTracerSpawner _shotTracerSpawner;
    private WeaponHitEffectSpawner _weaponHitEffectSpawner;

    private bool _isSetupFinished;

    private void OnEnable()
    {
        TrySubscribe();
    }

    private void OnDisable()
    {
        _weapon.EmptyShot -= OnEmptyShot;
        _weapon.AttackPerformed -= OnAttack;
        _weapon.ReloadStarted -= OnReloadStarted;
        _weapon.ReloadFinished -= OnReloadFinished;
        _weapon.AmmoLoad -= OnAmmoLoad;
        _weapon.Hit -= OnHit;
    }

    public void Setup(Weapon weapon, SoundData soundData, AudioPlayer audioPlayer, ShotTracerSpawner shotTracerSpawner, WeaponHitEffectSpawner weaponHitEffectSpawner)
    {
        _weapon = weapon;
        _soundData = soundData;
        _audioPlayer = audioPlayer;
        _shotTracerSpawner = shotTracerSpawner;
        _weaponHitEffectSpawner = weaponHitEffectSpawner;

        _isSetupFinished = true;

        TrySubscribe();
    }

    private void TrySubscribe()
    {
        if (_isSetupFinished == false)
            return;

        _weapon.EmptyShot += OnEmptyShot;
        _weapon.AttackPerformed += OnAttack;
        _weapon.ReloadStarted += OnReloadStarted;
        _weapon.ReloadFinished += OnReloadFinished;
        _weapon.AmmoLoad += OnAmmoLoad;
        _weapon.Hit += OnHit;
    }

    private void OnEmptyShot()
    {
        _audioPlayer.SetSound(_soundData.EmptyShotSound);
        _audioPlayer.Play();
    }

    private void OnAmmoLoad()
    {
        _audioPlayer.SetSound(_soundData.AmmoLoadSound);
        _audioPlayer.Play();
    }

    private void OnReloadFinished()
    {
        _audioPlayer.SetSound(_soundData.ReloadFinishedSound);
        _audioPlayer.Play();
    }

    private void OnReloadStarted()
    {
        _audioPlayer.SetSound(_soundData.ReloadStartedSound);
        _audioPlayer.Play();
    }

    private void OnAttack()
    {
        _audioPlayer.SetSound(_soundData.AttackSound);
        _audioPlayer.Play();
    }

    private void OnHit(HitInfo info)
    {
        if (_weapon.AttackType == AttackType.Range)
            _shotTracerSpawner.SpawnTracer(info);

        _weaponHitEffectSpawner.Spawn(info);
    }
}
