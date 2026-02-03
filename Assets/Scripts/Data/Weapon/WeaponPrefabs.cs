using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponPrefabs", menuName = "Weapons/WeaponPrefabs")]
public class WeaponPrefabs : ScriptableObject
{
    [SerializeField] private Weapon _axe;
    [SerializeField] private Weapon _automaticRifle;
    [SerializeField] private Weapon _shotgun;

    public Weapon Axe => _axe;
    public Weapon AutomaticRifle => _automaticRifle;
    public Weapon Shotgun => _shotgun;
}
