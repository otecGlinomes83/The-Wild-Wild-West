using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    private ParticleSystem _targetHitParticleEffect;
    private ParticleSystem _obstacleHitParticleEffect;

    public void Reset()
    {
        _targetHitParticleEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _targetHitParticleEffect.time = 0f;

        _obstacleHitParticleEffect.Stop(true, ParticleSystemStopBehavior.StopEmittingAndClear);
        _obstacleHitParticleEffect.time = 0f;
    }

    public void Setup(ParticleSystem targetHitEffectPrefab, ParticleSystem obstacleHitEffectPrefab)
    {
        if (_targetHitParticleEffect == null && _obstacleHitParticleEffect == null)
        {
            _targetHitParticleEffect = Instantiate(targetHitEffectPrefab, transform);
            _obstacleHitParticleEffect = Instantiate(obstacleHitEffectPrefab, transform);
        }
    }

    public void Play(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Obstacle:
                _obstacleHitParticleEffect.Play();
                break;

            case HitType.Target:
                _targetHitParticleEffect.Play();
                break;

            default:
                _obstacleHitParticleEffect.Play();
                break;
        }
    }

    public float GetDuration(HitType hitType)
    {
        if (_obstacleHitParticleEffect == null && _targetHitParticleEffect == null)
            return 0f;

        switch (hitType)
        {
            case HitType.Obstacle:
                return _obstacleHitParticleEffect.main.duration + _obstacleHitParticleEffect.main.startLifetime.constantMax;

            case HitType.Target:
                return _targetHitParticleEffect.main.duration + _targetHitParticleEffect.main.startLifetime.constantMax;

            default:
                return 0f;
        }
    }
}