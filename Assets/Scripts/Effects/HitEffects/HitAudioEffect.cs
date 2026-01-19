using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class HitAudioEffect : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public void Setup(AudioClip hitSound)
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.clip = hitSound;
        _audioSource.spatialBlend = 1f;
        _audioClip = hitSound;
    }

    public void Play()
    {
        _audioSource.volume = Random.Range(0.5f, 0.7f);
        _audioSource.Play();
    }

    public float GetDuration()
    {
        if (_audioClip == null)
            return 0f;

        return _audioClip.length;
    }
}
