using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HitAudioEffect), typeof(HitParticleEffect))]
public class WeaponHitEffect : MonoBehaviour
{
    private HitAudioEffect _audioEffect;
    private HitParticleEffect _particleEffect;

    private Coroutine _waitBeforeReleaseCoroutine;

    private float _totalPlaytime;

    public event Action<WeaponHitEffect> ReadyForRelease;

    public void Initialize(AudioClip hitSound, ParticleSystem hitParticle, Vector3 hitPoint, Vector3 hitNormal)
    {
        _audioEffect = GetComponent<HitAudioEffect>();
        _particleEffect = GetComponent<HitParticleEffect>();

        transform.position = hitPoint;
        transform.rotation = Quaternion.LookRotation(hitNormal);



        _audioEffect.Setup(hitSound);
        _particleEffect.Setup(hitParticle);

        _totalPlaytime = Mathf.Max(_audioEffect.GetDuration(), _particleEffect.GetDuration());
    }

    public void Play()
    {
        if (_waitBeforeReleaseCoroutine != null)
            return;

        _audioEffect.Play();
        _particleEffect.Play();

        _waitBeforeReleaseCoroutine = StartCoroutine(DelayedRelease(_totalPlaytime));
    }

    private IEnumerator DelayedRelease(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        ReadyForRelease?.Invoke(this);
        _waitBeforeReleaseCoroutine = null;
    }
}