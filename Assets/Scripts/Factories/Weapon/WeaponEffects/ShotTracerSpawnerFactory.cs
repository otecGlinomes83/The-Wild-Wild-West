using UnityEngine;

public static class ShotTracerSpawnerFactory
{
    public static ShotTracerSpawner Create(EffectData data, Transform parent = null)
    {
        GameObject gameObject = new GameObject("ShotTracerSpawner");

        if (parent != null)
            gameObject.transform.SetParent(parent, false);

        ShotTracerSpawner spawner = gameObject.AddComponent<ShotTracerSpawner>();
        spawner.Setup(data);

        return spawner;
    }
}
