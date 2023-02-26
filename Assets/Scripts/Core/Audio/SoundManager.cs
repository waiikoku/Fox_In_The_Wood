using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    [SerializeField] AudioSource _musicSource, _effectSource;
    [SerializeField] private MusicPlaylist music;
    [SerializeField] private SFXPlaylist sfx;
    private float _masterVolume = 1f;
    public float Master => _masterVolume;
    private float _musicVolume = 1f;
    public float Music => _musicVolume;
    private float _musicCurrentVolume = 1f;
    private float _effectVolume = 1f;
    public float Effect => _effectVolume;
    private float _effectCurrentVolume = 1f;

    private Coroutine lerpMusicCoroutine;

    //Events
    public event Action<float> OnMasterVolume;
    public event Action<float> OnBGMVolume;
    public event Action<float> OnSFXVolume;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);
        //PlayMusic(music.Sounds[0],0.1f);
        _musicCurrentVolume = 0f;
        ChangeMusicVolume(1f);
    }

    private void LateUpdate()
    {
        _musicSource.volume = _masterVolume * _musicCurrentVolume;
        _effectSource.volume = _masterVolume * _effectCurrentVolume;
    }

    private IEnumerator LerpMusic()
    {
        bool isDone = false;
        float percentage = 0f;
        float duration = 0.5f;
        float timer = 0f;
        while (!isDone)
        {
            _musicCurrentVolume = Mathf.Lerp(_musicCurrentVolume, _musicVolume, percentage);
            if(percentage >= 1f)
            {
                isDone = true;
            }
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;
            percentage = timer / duration;
        }
    }

    private IEnumerator LerpEffect()
    {
        bool isDone = false;
        float percentage = 0f;
        float duration = 0.5f;
        float timer = 0f;
        while (!isDone)
        {
            _effectCurrentVolume = Mathf.Lerp(_effectCurrentVolume, _effectVolume, percentage);
            if (percentage >= 1f)
            {
                isDone = true;
            }
            yield return new WaitForSeconds(0.01f);
            timer += Time.deltaTime;
            percentage = timer / duration;
        }
    }

    public enum SoundEffect
    {

    }


    public void CreateSFX(AudioClip clip, Vector3 position,float duration)
    {
        GameObject miniPlayer = new GameObject();
        AudioSource tempSource = miniPlayer.AddComponent<AudioSource>();
        tempSource.clip = clip;
        tempSource.volume = _effectVolume;
        Destroy(miniPlayer, duration);
    }

    public void PlaySFX(Sound sound)
    {
        //_effectSource.Stop();
        //_effectSource.clip = sound._Sound;
        _effectSource.PlayOneShot(sound._Sound);
    }

    public enum Sfx_Type
    {
        click,
        tick,
        pickUp,
        hit,
        leverDown,
        leverUp
    }

    public void PlaySFX(int audio)
    {
        switch (audio)
        {
            case 0:
                PlaySFX(sfx.Sounds[0]);
                break;
            case 1:
                PlaySFX(sfx.Sounds[1]);
                break;
            case 2:
                PlaySFX(sfx.Sounds[2]);
                break;
            case 3:
                PlaySFX(sfx.Sounds[3]);
                break;
            case 4:
                PlaySFX(sfx.Sounds[4]);
                break;
            case 5:
                PlaySFX(sfx.Sounds[5]);
                break;
            default:
                break;
        }
    }

    public void PlaySFX(Sfx_Type type)
    {
        switch (type)
        {
            case Sfx_Type.click:
                PlaySFX(sfx.Sounds[0]);
                break;
            case Sfx_Type.tick:
                PlaySFX(sfx.Sounds[1]);
                break;
            case Sfx_Type.pickUp:
                PlaySFX(sfx.Sounds[2]);
                break;
            case Sfx_Type.hit:
                PlaySFX(sfx.Sounds[3]);
                break;
            case Sfx_Type.leverDown:
                PlaySFX(sfx.Sounds[4]);
                break;
            case Sfx_Type.leverUp:
                PlaySFX(sfx.Sounds[5]);
                break;
            default:
                break;
        }
    }

    public void PlayMusic(Sound sound,float delay)
    {
        StartCoroutine(PlayMusicDelay(sound, delay));
    }

    public void PlayMusic(int index)
    {
        PlayMusic(music.Sounds[index]);
    }

    private IEnumerator PlayMusicDelay(Sound sound,float delay)
    {
        yield return new WaitForSeconds(delay);
        PlayMusic(sound);
    }

    public void PlayMusic(Sound sound)
    {
        _musicSource.Stop();
        _musicSource.clip = sound._Sound;
        _musicSource.Play();
    }

    public void StopMusic()
    {
        _musicSource.Stop();
    }

    public void ToggleEffect()
    {
        _effectSource.mute = !_effectSource.mute;
    }

    public void ToggleMusic()
    {
        _musicSource.mute = !_musicSource.mute;
    }

    private void ChangeMasterVolume(float volume)
    {
        _masterVolume = volume;
    }

    public void RaiseEventMaster(float value)
    {
        OnMasterVolume?.Invoke(value);
        ChangeMasterVolume(value);
    }

    private void ChangeMusicVolume(float volume)
    {
        _musicVolume = volume;
        if(Time.timeScale == 0)
        {
            _musicCurrentVolume = volume;
            _musicSource.volume = _musicCurrentVolume;
            return;
        }
        if (lerpMusicCoroutine != null)
        {
            StopCoroutine(lerpMusicCoroutine);
        }
        lerpMusicCoroutine = StartCoroutine(LerpMusic());
    }
    
    public void RaiseEventMusic(float value)
    {
        OnBGMVolume?.Invoke(value);
        ChangeMusicVolume(value);
    }

    private void ChangeSFXVolume(float volume)
    {
        _effectVolume = volume;
        _effectCurrentVolume = _effectVolume;
        _effectSource.volume = _effectCurrentVolume;
        /*
        _effectVolume = volume;
        if (lerpMusicCoroutine != null)
        {
            StopCoroutine(lerpMusicCoroutine);
        }
        lerpMusicCoroutine = StartCoroutine(LerpMusic());
        */
    }

    public void RaiseEventSFX(float value)
    {
        OnSFXVolume?.Invoke(value);
        ChangeSFXVolume(value);
    }

    public void ResetToDefault()
    {
        OnMasterVolume?.Invoke(1f);
        OnBGMVolume?.Invoke(1f);
        OnSFXVolume?.Invoke(1f);
        ChangeMasterVolume(1f);
        ChangeMusicVolume(1f);
        ChangeSFXVolume(1f);
    }
}
