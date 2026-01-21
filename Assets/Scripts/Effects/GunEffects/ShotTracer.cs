using System;
using System.Collections;
using UnityEngine;

public class ShotTracer : MonoBehaviour
{
    private ParticleSystem _shotParticle;
    private Coroutine _waitBeforeReleaseCoroutine;

    public event Action<ShotTracer> ReadyForRelease;

    public void Setup(ParticleSystem shotParticlePrefab)
    {
        if (_shotParticle == null)
            _shotParticle = Instantiate(shotParticlePrefab, transform);
    }

    public void Trace(HitInfo info)
    {
        if (_waitBeforeReleaseCoroutine != null)
            return;

        transform.position = info.StartPoint;
        transform.rotation = Quaternion.LookRotation(info.Direction);

        ParticleSystem.MainModule main = _shotParticle.main;

        float speed = main.startSpeed.constant;
        float lifeTime = info.Distance / speed;

        main.startLifetime = lifeTime;

        _shotParticle.Play();

        _waitBeforeReleaseCoroutine = StartCoroutine(DelayedRelease(lifeTime += 0.02f));
    }

    private IEnumerator DelayedRelease(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        ReadyForRelease?.Invoke(this);
        _waitBeforeReleaseCoroutine = null;
    }
}