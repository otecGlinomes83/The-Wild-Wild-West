using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private HealthDetector _healthDetector;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private int _damage;

    public event Action Shot;

    public void Shoot()
    {
       Shot?.Invoke();

        if (_healthDetector.TryDetect(out Health health) == false)
            return;

        health.TakeDamage(_damage);
    }
}