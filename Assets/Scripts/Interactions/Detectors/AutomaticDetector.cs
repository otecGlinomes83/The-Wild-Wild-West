using System;
using UnityEngine;

public class AutomaticDetector<T> : GenericDetector<T> where T : Component
{
    private T _currentTarget;

    public event Action<T> TargetDetected;
    public event Action TargetLost;

    private void FixedUpdate()
    {
        if (TryDetect(out T target))
        {
            if (_currentTarget == null)
            {
                _currentTarget = target;
                TargetDetected?.Invoke(target);
            }
            else if (_currentTarget != target)
            {
                TargetLost?.Invoke();
                _currentTarget = target;
                TargetDetected?.Invoke(target);
            }
        }
        else
        {
            if (_currentTarget != null)
            {
                TargetLost?.Invoke();
                _currentTarget = null;
            }
        }
    }
}
