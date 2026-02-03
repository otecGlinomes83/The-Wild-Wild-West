using UnityEngine;

public class WeaponBuilder : MonoBehaviour
{
    [SerializeField] private Weapon _weapon;
    [SerializeField] private WeaponContext _weaponContext;
    [SerializeField] private WeaponData _weaponData;

    private void Awake()
    {
        BuildWeapon();
    }

    private void Start()
    {
        Destroy(this);
    }

    private void BuildWeapon()
    {
        IAttackStrategy attackStrategy = AttackStrategyFactory.Create(_weaponData.AttackData);

        IDamageableDetector damageableDetector = DamageableDetectorFactory.Create(_weaponData.DetectionData, _weaponContext, transform);

        IReloader reloader;
        Magazine magazine;

        if (_weaponData.ReloadData != null && _weaponData.ReloadData.ReloadType != ReloadType.NonReload)
        {
            reloader = ReloaderFactory.Create(_weaponData.ReloadData, transform);
            magazine = MagazineFactory.CreateMagazine(_weaponData.MagazineData);
        }
        else
        {
            reloader = null;
            magazine = null;
        }

        _weapon.Setup
            (
            _weaponData.AttackData,
            _weaponData.ReloadData,
            attackStrategy,
            damageableDetector,
            magazine,
            reloader
            );

        WeaponEffector weaponEffector = WeaponEffectorFactory.Create
            (
            _weapon,
            _weaponData.WeaponEffectData.SoundData,
            AudioPlayerFactory.Create(transform),
            ShotTracerSpawnerFactory.Create(_weaponData.WeaponEffectData.EffectData, transform),
            WeaponHitEffectSpawnerFactory.Create(_weaponData.WeaponEffectData.HitEffectData, transform),
            transform
            );
    }
}
