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
        foreach (ShotTracer tracer in _activeTracers)
        {
            tracer.ReadyForRelease -= OnTracerReadyForRelease;
        }
    }

    public void Setup(EffectData data)
    {
        _pool = new ObjectPool<ShotTracer>
            (
                 createFunc: CreateTracer,
                 actionOnGet: tracer => tracer.gameObject.SetActive(true)
            );

        _effectData = data;
    }

    public void SpawnTracer(HitInfo info)
    {

        ShotTracer tracer = _pool.Get();

        if (_activeTracers.Contains(tracer) == false)
        {
            tracer.ReadyForRelease += OnTracerReadyForRelease;
            _activeTracers.Add(tracer);
        }

        tracer.Trace(info);
    }

    private void OnTracerReadyForRelease(ShotTracer tracer)
    {
        tracer.gameObject.SetActive(false);

        if (_activeTracers.Contains(tracer))
        {
            tracer.ReadyForRelease -= OnTracerReadyForRelease;
            _pool.Release(tracer);
            _activeTracers.Remove(tracer);
        }
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
