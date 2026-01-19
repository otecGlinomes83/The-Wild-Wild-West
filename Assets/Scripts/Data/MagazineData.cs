using UnityEngine;

[CreateAssetMenu(fileName = "NewMagazineData", menuName = "Weapons/MagazineData")]
public class MagazineData : ScriptableObject
{
    [SerializeField] private int _maxAmmoCount;

    public int MaxAmmoCount => _maxAmmoCount;
}
