using UnityEngine;

public static class WeaponHitEffectSpawnerFactory
{
    public static WeaponHitEffectSpawner Create(HitEffectData data, Transform parent = null)
    {
        GameObject gameObject = new GameObject("WeaponHitSpawner");

        if (parent != null)
            gameObject.transform.SetParent(parent, false);

        WeaponHitEffectSpawner spawner = gameObject.AddComponent<WeaponHitEffectSpawner>();
        spawner.Setup(data);

        return spawner;
    }
}
