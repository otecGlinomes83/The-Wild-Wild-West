using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Pool;

public class ShotTracerSpawner : MonoBehaviour
{
    private EffectData _effectData;

    private ObjectPool<ShotTracer> _pool;

    private List<ShotTracer> _activeTracers = new List<ShotTracer>();

    private void OnDisable()
    {
        for (int i = _activeTracers.Count - 1; i >= 0; i--)
        {
            ReleaseTracer(_activeTracers[i]);
        }
    }

    public void Setup(EffectData data)
    {
        _pool = new ObjectPool<ShotTracer>
            (
                 createFunc: CreateTracer,
                 actionOnGet: tracer => tracer.gameObject.SetActive(true),
                 actionOnRelease: tracer => OnTracerRelease(tracer)
            );

        _effectData = data;
    }

    private void OnTracerRelease(ShotTracer shotTracer)
    {
        shotTracer.Reset();
        shotTracer.gameObject.SetActive(false);
    }

    public void SpawnTracer(HitInfo info)
    {
        ShotTracer tracer = _pool.Get();

        tracer.ReadyForRelease += ReleaseTracer;
        _activeTracers.Add(tracer);

        tracer.Trace(info);
    }

    private void ReleaseTracer(ShotTracer tracer)
    {
        tracer.ReadyForRelease -= ReleaseTracer;
        _pool.Release(tracer);
        _activeTracers.Remove(tracer);
    }

    private ShotTracer CreateTracer()
    {
        GameObject gameObject = new GameObject("ShotTracer");
        gameObject.transform.SetParent(transform, false);

        ShotTracer tracer = gameObject.AddComponent<ShotTracer>();
        tracer.Setup(_effectData.TracerEffectPrefab);
        gameObject.SetActive(false);

        return tracer;
    }
}
