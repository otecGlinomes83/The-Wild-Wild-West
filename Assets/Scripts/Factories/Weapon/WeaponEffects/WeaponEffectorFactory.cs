using UnityEngine;

public static class WeaponEffectorFactory
{
    public static WeaponEffector Create(Weapon weapon, SoundData soundData, AudioPlayer audioPlayer, ShotTracerSpawner shotTracerSpawner, WeaponHitEffectSpawner weaponHitEffectSpawner, Transform parent = null)
    {
        GameObject gameObject = new GameObject("WeaponEffector");

        if (parent != null)
            gameObject.transform.SetParent(parent, false);

        WeaponEffector effector = gameObject.AddComponent<WeaponEffector>();

        effector.Setup(weapon, soundData, audioPlayer, shotTracerSpawner, weaponHitEffectSpawner);

        return effector;
    }
}
