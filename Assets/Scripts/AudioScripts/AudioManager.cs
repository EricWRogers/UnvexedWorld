using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SFXElement
    {
        public string name;
        public AudioClip clip;
    }

    [System.Serializable]
    public class MusicElement
    {
        public string name;
        public AudioClip clip;
    }

    [Header("Music")]
    public List<MusicElement> musicTracks;

    [Header("Sound Effects")]
    public List<SFXElement> soundEffects;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Initialize AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true; // Music should loop
    }

    private AudioClip GetMusicClip(string name)
    {
        foreach (MusicElement music in musicTracks)
        {
            if (music.name == name)
                return music.clip;
        }
        return null;
    }

    private AudioClip GetSFXClip(string name)
    {
        foreach (SFXElement sfx in soundEffects)
        {
            if (sfx.name == name)
                return sfx.clip;
        }
        return null;
    }

    // --- Music Methods ---
    public void PlayBackgroundMusic()
    {
        AudioClip clip = GetMusicClip("BackgroundMusic");
        if (clip != null && (musicSource.clip != clip || !musicSource.isPlaying))
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void PlayBattleMusic()
    {
        AudioClip clip = GetMusicClip("BattleMusic");
        if (clip != null && (musicSource.clip != clip || !musicSource.isPlaying))
        {
            musicSource.Stop();
            musicSource.clip = clip;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    public bool IsBattleMusicPlaying()
    {
        AudioClip clip = GetMusicClip("BattleMusic");
        return clip != null && musicSource.clip == clip && musicSource.isPlaying;
    }

    // --- SFX Methods ---
    public void PlayDashSound()
    {
        AudioClip clip = GetSFXClip("Dash");
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayPunchSound(int i)
    {
        if (i >= 0 && i < soundEffects.Count)
            sfxSource.PlayOneShot(soundEffects[i].clip);
    }

    public void PlayLandingSound()
    {
        AudioClip clip = GetSFXClip("Landing");
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayHurtSound()
    {
        AudioClip clip = GetSFXClip("Hurt");
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayOrbSound()
    {
        AudioClip clip = GetSFXClip("Orb");
        if (clip != null) sfxSource.PlayOneShot(clip);
    }

    public void PlayEnemyHurtSound()
    {
        AudioClip clip = GetSFXClip("EnemyHurt");
        if (clip != null) sfxSource.PlayOneShot(clip);
    }
}
