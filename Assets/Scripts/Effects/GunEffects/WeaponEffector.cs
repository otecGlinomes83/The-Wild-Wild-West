using System;
using UnityEngine;

public class WeaponEffector : MonoBehaviour
{
    private Weapon _weapon;
    private SoundData _soundData;
    private AudioPlayer _audioPlayer;

    private ShotTracerSpawner _shotTracerSpawner;
    private WeaponHitEffectSpawner _weaponHitEffectSpawner;

    public void Setup(Weapon weapon, SoundData soundData, AudioPlayer audioPlayer, ShotTracerSpawner shotTracerSpawner, WeaponHitEffectSpawner weaponHitEffectSpawner)
    {
        _weapon = weapon;
        _soundData = soundData;
        _audioPlayer = audioPlayer;
        _shotTracerSpawner = shotTracerSpawner;
        _weaponHitEffectSpawner = weaponHitEffectSpawner;

        _weapon.AttackPerformed += OnAttack;
        _weapon.ReloadStarted += OnReloadStarted;
        _weapon.ReloadFinished += OnReloadFinished;
        _weapon.AmmoLoad += OnAmmoLoad;
        _weapon.Hit += OnHit;
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
        _shotTracerSpawner.SpawnTracer(info);
        _weaponHitEffectSpawner.Spawn(info);
    }
}
