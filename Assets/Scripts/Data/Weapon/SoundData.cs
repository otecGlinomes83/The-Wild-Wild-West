using UnityEngine;

[CreateAssetMenu(fileName = "NewSoundData", menuName = "Weapons/SoundData")]
public class SoundData : ScriptableObject
{
    [SerializeField] AudioClip _attackSound;
    [SerializeField] AudioClip _reloadStartedSound;
    [SerializeField] AudioClip _ammoLoadSound;
    [SerializeField] AudioClip _reloadFinishedSound;
    [SerializeField] AudioClip _emptyShotSound;

    public AudioClip AttackSound => _attackSound;
    public AudioClip ReloadStartedSound => _reloadStartedSound;
    public AudioClip AmmoLoadSound => _ammoLoadSound;
    public AudioClip ReloadFinishedSound => _reloadFinishedSound;
    public AudioClip EmptyShotSound => _emptyShotSound;
}