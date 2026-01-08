using System;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] private DamageAblerDetector _damageAblerDetector;
    [SerializeField] private Transform _shootPosition;
    [SerializeField] private int _damage;

    public event Action Shot;

    public void Shoot()
    {
        Shot?.Invoke();

        if (_damageAblerDetector.TryDetect(out IDamageAbler damageAbler) == false)
            return;

        damageAbler.TakeDamage(_damage);
    }
}