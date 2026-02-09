using System.Collections.Generic;
using UnityEngine;

public class WeaponHitEffectSpawner : MonoBehaviour
{
    private HitEffectData _hitEffectData;
    private GenericPool<WeaponHitEffect> _effectPool = new GenericPool<WeaponHitEffect>();
    private List<WeaponHitEffect> _activeEffects = new List<WeaponHitEffect>();

    private void OnDisable()
    {
        for (int i = _activeEffects.Count - 1; i >= 0; i--)
        {
            ReleaseEffect(_activeEffects[i]);
        }
    }

    public void Setup(HitEffectData data)
    {
        _hitEffectData = data;
        _effectPool.Setup(transform, "WeaponHitEffect");
    }

    public void Spawn(HitInfo info)
    {
        WeaponHitEffect effect = _effectPool.Get();

        effect.ReadyForRelease += ReleaseEffect;
        _activeEffects.Add(effect);

        effect.Initialize(_hitEffectData, info.HitPoint, info.HitNormal);
        effect.Play(info.HitType);
    }

    private void ReleaseEffect(WeaponHitEffect effect)
    {
        effect.ReadyForRelease -= ReleaseEffect;
        _effectPool.Release(effect);
        _activeEffects.Remove(effect);
    }
}
