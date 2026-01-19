using UnityEngine;

public static class WeaponHitEffectorFactory
{
    public static WeaponHitEffector Create(Weapon weapon, HitEffectData hitEffectData, Transform parent = null)
    {
        GameObject gameObject = new GameObject("WeaponHitEffector");

        if (parent != null)
            gameObject.transform.SetParent(parent, false);

        WeaponHitEffector effector = gameObject.AddComponent<WeaponHitEffector>();
        effector.Setup(weapon, hitEffectData);

        return effector;
    }
}
