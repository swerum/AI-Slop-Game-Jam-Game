using System;
using UnityEngine;

public enum SoundEffect
{
    UISound,
    Attack,
    Walk,
    Dash,
    GameOver,
}

[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Assign an AudioClip for each SoundEffect")]
    [SerializeField] private AudioClip _uiSound;
    [SerializeField] private AudioClip _gameOverSound;
    [SerializeField] private AudioClip _attack;
    [SerializeField] private AudioClip _walk;
    [SerializeField] private AudioClip _dash;

    [Header("Background Music")]
    [SerializeField] private AudioClip _musicClip;
    [SerializeField, Range(0f, 1f)] private float _musicVolume = 1f;

    private AudioSource _source;
    private AudioSource _musicSource;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        _source = GetComponent<AudioSource>();
        _source.playOnAwake = false;

        // Setup dedicated music AudioSource that loops
        _musicSource = gameObject.AddComponent<AudioSource>();
        _musicSource.playOnAwake = false;
        _musicSource.loop = true;
        _musicSource.clip = _musicClip;
        _musicSource.volume = Mathf.Clamp01(_musicVolume);
        if (_musicClip != null)
        {
            _musicSource.Play();
        }

        // Nothing to resize when using explicit fields; inspector shows individual clip slots.
    }

    /// <summary>
    /// Play the AudioClip associated with the given SoundEffect.
    /// </summary>
    /// <param name="effect">Sound effect to play</param>
    /// <param name="volume">Volume multiplier (0-1)</param>
    public void Play(SoundEffect effect, float volume = 1f)
    {
        AudioClip clip = effect switch
        {
            SoundEffect.UISound => _uiSound,
            SoundEffect.Attack => _attack,
            SoundEffect.Walk => _walk,
            SoundEffect.Dash => _dash,
            SoundEffect.GameOver => _gameOverSound,
            _ => null
        };

        if (clip == null)
        {
            Debug.LogWarning($"AudioManager: Clip for {effect} is not assigned", this);
            return;
        }

        _source.PlayOneShot(clip, Mathf.Clamp01(volume));
    }
}