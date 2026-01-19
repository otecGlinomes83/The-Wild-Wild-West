using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class WeaponEffector : MonoBehaviour
{
    private Weapon _weapon;
    private SoundData _soundData;
    private AudioPlayer _audioPlayer;

    public void Setup()
    {
        _audioPlayer.Setup();

        _weapon.AttackPerformed += OnAttack;
        _weapon.ReloadStarted += OnReloadStarted;
        _weapon.ReloadFinished += OnReloadFinished;
        _weapon.AmmoLoad += OnAmmoLoad;
    }

    private void OnAmmoLoad()
    {
        _audioPlayer.SetSound(_soundData.AmmoLoadSound);
        _audioPlayer.Play();
    }

    private void OnReloadFinished()
    {
        _audioPlayer.SetSound(_soundData.ReloadFinishedSound);
        _audioPlayer.Play();
    }

    private void OnReloadStarted()
    {
        _audioPlayer.SetSound(_soundData.ReloadStartedSound);
        _audioPlayer.Play();
    }

    private void OnAttack()
    {
        _audioPlayer.SetSound(_soundData.AttackSound);
        _audioPlayer.Play();
    }
}

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

public class ShotTracerSpawner : MonoBehaviour
{
    private EffectData _effectData;

    private ObjectPool<ShotTracer> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<ShotTracer>(
            createFunc: CreateTracer,
            actionOnGet: tracer => tracer.gameObject.SetActive(true),
            actionOnRelease: tracer => tracer.gameObject.SetActive(false)
        );
    }

    private ShotTracer CreateTracer()
    {
        GameObject gameObject = new GameObject("ShotTracer");
        gameObject.transform.SetParent(transform, false);

        ShotTracer tracer = gameObject.AddComponent<ShotTracer>();
        tracer.Setup(_effectData.TracerEffectPrefab);
        gameObject.SetActive(false);

        return tracer;
    }

    public void SpawnTracer(HitInfo info)
    {
        ShotTracer tracer = _pool.Get();

        tracer.TraceShot(info);
    }
}

public class ShotTracer : MonoBehaviour
{
    private ParticleSystem _shotParticle;
    private Coroutine _waitBeforeReleaseCoroutine;

    public event Action<ShotTracer> ReadyForRelease;

    public void Setup(ParticleSystem shotParticlePrefab)
    {
        if (_shotParticle == null)
            _shotParticle = Instantiate(shotParticlePrefab, transform);
    }

    public void TraceShot(HitInfo info)
    {
        if (_waitBeforeReleaseCoroutine != null)
            return;

        transform.position = info.StartPoint;
        transform.rotation = Quaternion.LookRotation(info.Direction);

        ParticleSystem.MainModule main = _shotParticle.main;

        float speed = main.startSpeed.constant;
        float lifeTime = info.Distance / speed;

        main.startLifetime = lifeTime;

        _shotParticle.Play();

        _waitBeforeReleaseCoroutine = StartCoroutine(DelayedRelease(lifeTime += 0.02f));
    }

    private IEnumerator DelayedRelease(float delay)
    {
        yield return new WaitForSecondsRealtime(delay);

        ReadyForRelease?.Invoke(this);
        _waitBeforeReleaseCoroutine = null;
    }
}