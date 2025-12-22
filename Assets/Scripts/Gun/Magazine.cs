using System;
using UnityEngine;

public class Magazine : MonoBehaviour, IChangeObservable
{
    [SerializeField] private int _maxAmmo;

    private int _currentAmmo;

    public event Action<int, int> ValueChanged;

    public int MaxAmmo => _maxAmmo;
    public int CurrentAmmo => _currentAmmo;

    private void Awake()
    {
        Reload();
    }

    public void SpendAmmo()
    {
        if (_currentAmmo <= 0)
            return;

        _currentAmmo--;
        ValueChanged?.Invoke(_currentAmmo, _maxAmmo);
    }

    public void Reload()
    {
        _currentAmmo = _maxAmmo;
        ValueChanged?.Invoke(_currentAmmo, _maxAmmo);
    }
}
