using System.Collections.Generic;
using UnityEngine;

public class ShotTracerSpawner : MonoBehaviour
{
    private EffectData _effectData;

    private GenericPool<ShotTracer> _pool = new GenericPool<ShotTracer>();

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
        _effectData = data;
        _pool.Setup(transform, "ShotTracer");
    }

    public void SpawnTracer(HitInfo info)
    {
        ShotTracer tracer = _pool.Get();

        tracer.ReadyForRelease += ReleaseTracer;
        _activeTracers.Add(tracer);

        tracer.Setup(_effectData.TracerEffectPrefab);
        tracer.Trace(info);
    }

    private void ReleaseTracer(ShotTracer tracer)
    {
        tracer.ReadyForRelease -= ReleaseTracer;

        _pool.Release(tracer);
        _activeTracers.Remove(tracer);
    }

}
