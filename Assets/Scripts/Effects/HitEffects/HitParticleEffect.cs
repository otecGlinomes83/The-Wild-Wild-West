using UnityEngine;

public class HitParticleEffect : MonoBehaviour
{
    private ParticleSystem _effect;

    public void Setup(ParticleSystem hitEffectPrefab)
    {
        if (_effect == null)
            _effect = Instantiate(hitEffectPrefab, transform);
    }

    public void Play()
    {
        _effect.Play();
    }

    public float GetDuration()
    {
        if (_effect == null)
            return 0f;

        return _effect.main.duration + _effect.main.startLifetime.constantMax;
    }

}