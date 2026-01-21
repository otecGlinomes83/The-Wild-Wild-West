using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponHitEffectSpawner : MonoBehaviour
{
    private HitEffectData _hitEffectData;

    private ObjectPool<WeaponHitEffect> _effectPool;

    private List<WeaponHitEffect> _activeEffects = new List<WeaponHitEffect>();

    private void OnDisable()
    {
        foreach (WeaponHitEffect effect in _activeEffects)
        {
            effect.ReadyForRelease -= OnEffectReadyForRelease;
        }
    }

    public void Setup( HitEffectData data)
    {
        _hitEffectData = data;

        _effectPool = new ObjectPool<WeaponHitEffect>
            (
                createFunc: () => CreateEffect(),
                actionOnGet: effect => effect.gameObject.SetActive(true),
                actionOnRelease: effect => effect.gameObject.SetActive(false)
            );
    }

    private WeaponHitEffect CreateEffect()
    {
        GameObject gameObject = new GameObject("WeaponHitEffect");
        gameObject.transform.SetParent(transform, false);

        WeaponHitEffect effect = gameObject.AddComponent<WeaponHitEffect>();
        gameObject.SetActive(false);

        return effect;
    }

    public void Spawn(HitInfo info)
    {
        WeaponHitEffect effect = _effectPool.Get();

        if (_activeEffects.Contains(effect) == false)
        {
            effect.ReadyForRelease += OnEffectReadyForRelease;
            _activeEffects.Add(effect);
        }

        switch (info.HitType)
        {
            case HitType.Obstacle:
                effect.Initialize(_hitEffectData.ObstacleHitAudio, _hitEffectData.ObstacleHitParticleEffect, info.HitPoint, info.HitNormal);
                effect.Play();
                break;

            case HitType.Target:
                effect.Initialize(_hitEffectData.TargetHitAudio, _hitEffectData.TargetHitParticleEffect, info.HitPoint, info.HitNormal);
                effect.Play();
                break;

            default:
                effect.Initialize(_hitEffectData.ObstacleHitAudio, _hitEffectData.ObstacleHitParticleEffect, info.HitPoint, info.HitNormal);
                effect.Play();
                break;
        }
    }

    private void OnEffectReadyForRelease(WeaponHitEffect effect)
    {
        if (_activeEffects.Contains(effect))
        {
            effect.ReadyForRelease -= OnEffectReadyForRelease;
            _effectPool.Release(effect);
            _activeEffects.Remove(effect);
        }
    }
}
