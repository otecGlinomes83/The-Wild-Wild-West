using UnityEngine;

[RequireComponent(typeof(AudioPlayer))]
public class HitAudioEffect : MonoBehaviour
{
    private AudioPlayer _audioPlayer;

    private AudioClip _obstacleHitAudio;
    private AudioClip _targetHitAudio;

    public void Reset()
    {
        _audioPlayer.Stop();
    }

    public void Setup(AudioClip targetHitSound, AudioClip obstacleHitSound)
    {
        _audioPlayer = GetComponent<AudioPlayer>();
        _audioPlayer.Setup();
        _obstacleHitAudio = obstacleHitSound;
        _targetHitAudio = targetHitSound;
    }

    public void Play(HitType hitType)
    {
        switch (hitType)
        {
            case HitType.Obstacle:
                _audioPlayer.SetSound(_obstacleHitAudio);
                _audioPlayer.Play();
                break;

            case HitType.Target:
                _audioPlayer.SetSound(_targetHitAudio);
                _audioPlayer.Play();
                break;

            default:
                _audioPlayer.SetSound(_obstacleHitAudio);
                _audioPlayer.Play();
                break;
        }
    }

    public float GetDuration()
    {
        return _audioPlayer.GetDuration();
    }
}
