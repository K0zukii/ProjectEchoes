using System;
using System.Collections;
using System.Runtime.InteropServices;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Audio Sources")]
    [SerializeField] private AudioSource ambientSource;
    [SerializeField] private AudioSource chaseSource;
    [SerializeField] private AudioSource sfxSource;

    [Header("Clips")]
    [SerializeField] private AudioClip ambientClip;
    [SerializeField] private AudioClip chaseClip;
    [SerializeField] private AudioClip flashlightClick;
    [SerializeField] private AudioClip paperPickup;

    [Header("Settings")]
    [SerializeField] private float fadeDuration = 0.8f;

    private Coroutine _fadeCoroutine;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        SetupMusicSource(ambientSource, ambientClip);
        SetupMusicSource(chaseSource, chaseClip);
    }

    private void SetupMusicSource(AudioSource source, AudioClip clip)
    {
        if (source == null) return;
        source.clip = clip;
        source.loop = true;
        source.spatialBlend = 0f;
        source.volume = 0f;
    }

    void Start()
    {
        if (ambientSource != null && ambientClip != null)
        {
            ambientSource.volume = 1f;
            ambientSource.Play();
        }

        if (chaseSource != null && chaseClip != null)
        {
            chaseSource.volume = 0f;
            chaseSource.Play();
        }
    }

    void OnEnable()
    {
        GameEvents.OnFolderCollected += PlayFolderSound;
        GameEvents.OnPlayerCaught += StopAllOnDeath;
        GameEvents.OnGameWon += StopAllOnDeath;
        GameEvents.OnMonsterChaseStart += StartChaseMusic;
        GameEvents.OnMonsterChaseEnd += EndChaseMusic;
    }

    void OnDisable()
    {
        GameEvents.OnFolderCollected -= PlayFolderSound;
        GameEvents.OnPlayerCaught -= StopAllOnDeath;
        GameEvents.OnGameWon -= StopAllOnDeath;
        GameEvents.OnMonsterChaseStart -= StartChaseMusic;
        GameEvents.OnMonsterChaseEnd -= EndChaseMusic;
    }

    private void StartChaseMusic()
    {
        SwitchMusic(chaseSource, ambientSource);
    }

    private void EndChaseMusic()
    {
        SwitchMusic(ambientSource, chaseSource);
    }

    private void SwitchMusic(AudioSource targetSource, AudioSource sourceToMute)
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        _fadeCoroutine = StartCoroutine(CrossfadeRoutine(targetSource, sourceToMute, fadeDuration));
    }

    private IEnumerator CrossfadeRoutine(AudioSource fadeIn, AudioSource fadeOut, float duration)
    {
        float startIn = fadeIn != null ? fadeIn.volume : 0f;
        float startOut = fadeOut != null ? fadeOut.volume : 0f;
        float elapsed = 0f;

        while (elapsed < duration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / duration;

            if (fadeIn != null) fadeIn.volume = Mathf.Lerp(startIn, 1f, t);
            if (fadeOut != null) fadeOut.volume = Mathf.Lerp(startOut, 0f, t);

            yield return null;
        }

        if (fadeIn != null) fadeIn.volume = 1f;
        if (fadeOut != null) fadeOut.volume = 0f;
        _fadeCoroutine = null;
    }

    private void StopAllOnDeath()
    {
        if (_fadeCoroutine != null) StopCoroutine(_fadeCoroutine);
        if (ambientSource != null) ambientSource.Stop();
        if (chaseSource != null) chaseSource.Stop();
    }

    public void PlaySFX(AudioClip clip, float volume = 1f)
    {
        if (clip != null && sfxSource != null)
        {
            sfxSource.PlayOneShot(clip, volume);
        }
    }

    public void PlayFlashlightClick()
    {
        PlaySFX(flashlightClick);
    }

    private void PlayFolderSound()
    {
        PlaySFX(paperPickup);
    }
}
