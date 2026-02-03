using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

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

public class GenericPool<T> where T : Component, IPooled
{
    private ObjectPool<T> _pool;

    public void Setup(Transform parent, string name)
    {
        _pool = new ObjectPool<T>
            (
            createFunc: () => Create(parent, name),
            actionOnGet: objectToGet => OnGet(objectToGet),
            actionOnRelease: objectToRelease => OnRelease(objectToRelease)
            );
    }

    public T Get() =>
         _pool.Get();

    public void Release(T objectToRelease) =>
        _pool.Release(objectToRelease);

    private void OnRelease(T objectToRelease)
    {
        objectToRelease.Reset();
        objectToRelease.gameObject.SetActive(false);
    }

    protected void OnGet(T objectToGet)
    {
        objectToGet.gameObject.SetActive(true);
    }

    protected T Create(Transform parent, string name)
    {
        GameObject gameObject = new GameObject(name);
        gameObject.transform.SetParent(parent, false);

        T createdObject = gameObject.AddComponent<T>();
        gameObject.SetActive(false);

        return createdObject;
    }
}