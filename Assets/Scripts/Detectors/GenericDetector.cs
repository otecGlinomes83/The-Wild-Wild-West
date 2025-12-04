using System;
using UnityEngine;

public class GenericDetector<T> : MonoBehaviour where T : Component
{
    [SerializeField] private float _radius = 3f;
    [SerializeField] private LayerMask _detectingLayer;

    public event Action<T> TargetDetected;
    public event Action TargetLost;

    private T _currentTarget;

    private void FixedUpdate()
    {
        Collider[] hits = Physics.OverlapSphere(transform.position, _radius, _detectingLayer);

        T target = null;

        foreach (var hit in hits)
        {
            if (hit.TryGetComponent(out T component))
            {
                target = component;
                break;
            }
        }

        if (target != null && _currentTarget == null)
        {
            TargetDetected?.Invoke(target);
        }

        if (target == null && _currentTarget != null)
        {
            TargetLost?.Invoke();
        }

        _currentTarget = target;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, _radius);
    }
}
