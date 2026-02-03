using UnityEngine;

[CreateAssetMenu(fileName = "NewWeaponEffectData", menuName = "Weapons/WeaponEffectData")]
public class WeaponEffectData : ScriptableObject
{
    [SerializeField] private HitEffectData _hitEffectData;
    [SerializeField] private EffectData _effectData;
    [SerializeField] private SoundData _soundData;

    public SoundData SoundData => _soundData;
    public HitEffectData HitEffectData => _hitEffectData;

    public EffectData EffectData => _effectData;
}
