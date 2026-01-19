using UnityEngine;

[CreateAssetMenu(fileName = "NewHitEffectData", menuName = "Weapons/HitEffectData")]
public class HitEffectData : ScriptableObject
{
    [SerializeField] private ParticleSystem _targetHitParticleEffect;
    [SerializeField] private ParticleSystem _obstacleHitParticleEffect;
    [SerializeField] private AudioClip _targetHitAudio;
    [SerializeField] private AudioClip _obstacleHitAudio;

    public AudioClip ObstacleHitAudio => _obstacleHitAudio;
    public AudioClip TargetHitAudio => _targetHitAudio;
    public ParticleSystem TargetHitParticleEffect => _targetHitParticleEffect;
    public ParticleSystem ObstacleHitParticleEffect => _obstacleHitParticleEffect;
}