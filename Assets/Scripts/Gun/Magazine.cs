using UnityEngine;

public class Magazine : MonoBehaviour
{
    [SerializeField] private int _maxAmmo;

    private int _currentAmmoCount;

    public int MaxAmmo => _maxAmmo;
    public int CurrentAmmoCount => _currentAmmoCount;

    private void Awake()
    {
        _currentAmmoCount = _maxAmmo;
    }

    public void SpendAmmo()
    {
        if (_currentAmmoCount <= 0)
            return;

        _currentAmmoCount--;
    }

    public void Reload() =>
        _currentAmmoCount = _maxAmmo;
}
