using UnityEngine;

[CreateAssetMenu(fileName = "NewEffectData", menuName = "Weapons/EffectData")]
public class EffectData : ScriptableObject
{
    [SerializeField] private ParticleSystem _tracerEffectPrefab;

    public ParticleSystem TracerEffectPrefab => _tracerEffectPrefab;
}