using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HitAudioEffect), typeof(HitParticleEffect))]
public class WeaponHitEffect : MonoBehaviour, IPooled
{
    private HitAudioEffect _audioEffect;
    private HitParticleEffect _particleEffect;

    private Coroutine _waitBeforeReleaseCoroutine;

    public event Action<WeaponHitEffect> ReadyForRelease;

    public void Reset()
    {
        if (_waitBeforeReleaseCoroutine != null)
        {
            StopCoroutine(_waitBeforeReleaseCoroutine);
            _waitBeforeReleaseCoroutine = null;
        }

        _audioEffect.Reset();
        _particleEffect.Reset();
    }

    public void Initialize(HitEffectData data, Vector3 hitPoint, Vector3 hitNormal)
    {
        _audioEffect = GetComponent<HitAudioEffect>();
        _particleEffect = GetComponent<HitParticleEffect>();

        transform.position = hitPoint;
        transform.rotation = Quaternion.LookRotation(hitNormal);



        _audioEffect.Setup(data.TargetHitAudio, data.ObstacleHitAudio);
        _particleEffect.Setup(data.TargetHitParticleEffect, data.ObstacleHitParticleEffect);
    }

    public void Play(HitType hitType)
    {
        if (_waitBeforeReleaseCoroutine != null)
            return;

        float totalPlaytime = Mathf.Max(_audioEffect.GetDuration(), _particleEffect.GetDuration(hitType));

        _audioEffect.Play(hitType);
        _particleEffect.Play(hitType);

        _waitBeforeReleaseCoroutine = StartCoroutine(DelayedRelease(totalPlaytime));
    }

    private IEnumerator DelayedRelease(float delay)
    {
        yield return new WaitForSeconds(delay);

        ReadyForRelease?.Invoke(this);
        _waitBeforeReleaseCoroutine = null;
    }
}