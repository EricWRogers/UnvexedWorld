using UnityEngine;
using UnityEngine.Audio;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    [System.Serializable]
    public class SFXElement
    {
        public string name;
        public AudioClip clip;
        public AudioMixerGroup mixerGroup; // Unique Mixer Group for each SFX
    }

    [System.Serializable]
    public class MusicElement
    {
        public string name;
        public AudioClip clip;
        public AudioMixerGroup mixerGroup; // Unique Mixer Group for each Music track
    }

    [Header("Audio Mixer")]
    public AudioMixer audioMixer;  // Reference to the Audio Mixer

    [Header("Music")]
    public List<MusicElement> musicTracks;

    [Header("Sound Effects")]
    public List<SFXElement> soundEffects;

    void Awake()
    {
        // Ensure the audio mixer is correctly assigned
        if (audioMixer == null)
        {
            Debug.LogError("Audio Mixer is missing in AudioManager!");
        }
    }

    private MusicElement GetMusicElement(string name)
    {
        return musicTracks.Find(music => music.name == name);
    }

    private SFXElement GetSFXElement(string name)
    {
        return soundEffects.Find(sfx => sfx.name == name);
    }

    // --- Music Methods ---
    public void PlayBackgroundMusic()
    {
        MusicElement music = GetMusicElement("BackgroundMusic");
        if (music != null)
        {
            PlaySound(music.clip, music.mixerGroup);
        }
    }

    public void PlayBattleMusic()
    {
        MusicElement music = GetMusicElement("BattleMusic");
        if (music != null)
        {
            PlaySound(music.clip, music.mixerGroup);
        }
    }

    public void StopMusic()
    {
        // Stop all currently playing audio
        AudioSource[] sources = GetComponents<AudioSource>();
        foreach (AudioSource source in sources)
        {
            source.Stop();
        }
    }

    public bool IsBattleMusicPlaying()
    {
        MusicElement music = GetMusicElement("BattleMusic");
        AudioSource[] sources = GetComponents<AudioSource>();

        foreach (AudioSource source in sources)
        {
            if (music != null && source.clip == music.clip && source.isPlaying)
                return true;
        }
        return false;
    }

    // --- SFX Methods ---
    public void PlayDashSound()
    {
        PlaySFX("Dash");
    }

    public void PlayPunchSound(int i)
    {
        if (i >= 0 && i < soundEffects.Count)
        {
            PlaySound(soundEffects[i].clip, soundEffects[i].mixerGroup);
        }
    }

    public void PlayLandingSound()
    {
        PlaySFX("Landing");
    }

    public void PlayHurtSound()
    {
        PlaySFX("Hurt");
    }

    public void PlayOrbSound()
    {
        PlaySFX("Orb");
    }

    public void PlayEnemyHurtSound()
    {
        PlaySFX("EnemyHurt");
    }

    private void PlaySFX(string name)
    {
        SFXElement sfx = GetSFXElement(name);
        if (sfx != null)
        {
            PlaySound(sfx.clip, sfx.mixerGroup);
        }
    }

    private void PlaySound(AudioClip clip, AudioMixerGroup mixerGroup)
    {
        if (clip != null)
        {
            AudioSource source = gameObject.AddComponent<AudioSource>();
            source.clip = clip;
            source.outputAudioMixerGroup = mixerGroup;
            source.Play();
            Destroy(source, clip.length); // Destroy audio source after playing
        }
    }

    // --- Equalizer Controls for each sound element ---
    public void SetBass(string soundName, float value)
    {
        SetEQ(soundName, "Bass", value);
    }

    public void SetMid(string soundName, float value)
    {
        SetEQ(soundName, "Mid", value);
    }

    public void SetTreble(string soundName, float value)
    {
        SetEQ(soundName, "Treble", value);
    }

    private void SetEQ(string soundName, string parameter, float value)
    {
        SFXElement sfx = GetSFXElement(soundName);
        MusicElement music = GetMusicElement(soundName);

        AudioMixerGroup mixerGroup = sfx != null ? sfx.mixerGroup : music?.mixerGroup;
        if (mixerGroup != null)
        {
            audioMixer.SetFloat(parameter + "_" + soundName, Mathf.Log10(Mathf.Max(value, 0.0001f)) * 20);
        }
    }
}
