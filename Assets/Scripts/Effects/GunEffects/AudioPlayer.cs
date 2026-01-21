using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class AudioPlayer : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioClip _audioClip;

    public void Setup()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    public void SetSound(AudioClip newSound)
    {
        _audioClip = newSound;
        _audioSource.clip = _audioClip;
    }

    public void Play()
    {
        _audioSource.Play();
    }

    public float GetDuration()
    {
        if (_audioClip == null)
            return 0f;

        return _audioClip.length;
    }
}
