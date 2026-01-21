using UnityEngine;

public static class WeaponHitEffectorFactory
{
    public static WeaponHitEffectSpawner Create(HitEffectData hitEffectData, Transform parent = null)
    {
        GameObject gameObject = new GameObject("WeaponHitEffector");

        if (parent != null)
            gameObject.transform.SetParent(parent, false);

        WeaponHitEffectSpawner effector = gameObject.AddComponent<WeaponHitEffectSpawner>();
        effector.Setup(hitEffectData);

        return effector;
    }
}
