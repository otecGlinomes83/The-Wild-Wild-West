using UnityEngine;

public class Magazine
{
    private MagazineData _data;
    private int _currentAmmoCount;

    public int CurrentAmmoCount => _currentAmmoCount;
    public int MaxAmmoCount => _data.MaxAmmoCount;

    public Magazine(MagazineData magazineData)
    {
        _data = magazineData;
        _currentAmmoCount = _data.MaxAmmoCount;
    }

    public void AddAmmo(int count = 1)
    {
        _currentAmmoCount = Mathf.Min(_currentAmmoCount + count, _data.MaxAmmoCount);
        Debug.Log($"<color=#FF69B4>Ammo Loaded! {_currentAmmoCount}/{_data.MaxAmmoCount}</color>");
    }

    public void SpendAmmo(int count = 1)
    {
        _currentAmmoCount = Mathf.Max(_currentAmmoCount - count, 0);
        Debug.Log($"<color=#FF69B4>Ammo Spent! {_currentAmmoCount}/{_data.MaxAmmoCount}</color>");
    }

    public bool IsEmpty() =>
        _currentAmmoCount <= 0;
}

