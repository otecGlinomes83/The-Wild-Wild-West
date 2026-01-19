using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponData", menuName = "Weapons/WeaponData")]
public class WeaponData : ScriptableObject
{
    [SerializeField] private DetectionData _detectionData;
    [SerializeField] private ReloadData _reloadData;
    [SerializeField] private MagazineData _magazineData;
    [SerializeField] private AttackData _attackData;
    [SerializeField] private WeaponEffectData _effectData;

    public DetectionData DetectionData => _detectionData;
    public ReloadData ReloadData => _reloadData;
    public MagazineData MagazineData => _magazineData;
    public AttackData AttackData => _attackData;

    public WeaponEffectData EffectData => _effectData;
}
