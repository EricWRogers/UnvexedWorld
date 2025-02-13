using UnityEngine;
using System.Collections.Generic;

public class AudioManager : MonoBehaviour
{
    // Music and Sound Effect clips
    [Header("Music")]
    public AudioClip backgroundMusic;
    public AudioClip battleMusic;

    [Header("SFX")]
    public List<AudioClip> playerDash;
    public List<AudioClip> punchSounds;
    public List<AudioClip> landing;
    public List<AudioClip> hurt;
    public List<AudioClip> orb;
    public List<AudioClip> enemyHurt;

    // AudioSources for music and SFX
    private AudioSource musicSource;
    private AudioSource sfxSource;

    // Equalizer settings for Music
    [Header("Music Equalizer")]
    public Equalizer musicEqualizer;

    // Equalizer settings for SFX
    [Header("SFX Equalizer")]
    public Equalizer dashEqualizer;
    public Equalizer punchEqualizer;
    public Equalizer landingEqualizer;
    public Equalizer hurtEqualizer;
    public Equalizer orbEqualizer;
    public Equalizer enemyHurtEqualizer;

    [System.Serializable]
    public class Equalizer
    {
        [Range(0f, 1f)] public float bass = 0.5f;
        [Range(0f, 1f)] public float mid = 0.5f;
        [Range(0f, 1f)] public float treble = 0.5f;
    }

    void Awake()
    {
        // Initialize two AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true; // Music source should loop for background/battle music
    }

    // Music playback
    public void PlayBackgroundMusic()
    {
        if (musicSource.clip != backgroundMusic || !musicSource.isPlaying)
        {
            musicSource.Stop(); // Ensure previous music stops before playing new one
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlayBattleMusic()
    {
        if (musicSource.clip != battleMusic || !musicSource.isPlaying)
        {
            musicSource.Stop(); // Ensure previous music stops before playing new one
            musicSource.clip = battleMusic;
            musicSource.Play();
        }
    }

    // SFX playback
    public void PlayDashSound(int i)
    {
        sfxSource.PlayOneShot(playerDash[i]);
    }

    public void PlayPunchSound(int i)
    {
        sfxSource.PlayOneShot(punchSounds[i]);
    }

    public void PlayLandingSound(int i)
    {
        sfxSource.PlayOneShot(landing[i]);
    }

    public void PlayHurtSound(int i)
    {
        sfxSource.PlayOneShot(hurt[i]);
    }

    public void PlayOrbSound(int i)
    {
        sfxSource.PlayOneShot(orb[i]);
    }

    public void PlayEnemyHurtSound(int i)
    {
        sfxSource.PlayOneShot(enemyHurt[i]);
    }

    // Stop music
    public void StopMusic()
    {
        musicSource.Stop();
    }

    // New method to check if battle music is playing
    public bool IsBattleMusicPlaying()
    {
        return musicSource.clip == battleMusic && musicSource.isPlaying;
    }

    // Equalizer control for Music (bass, mid, treble adjustments)
    void UpdateMusicEqualizer()
    {
        // Adjust the bass, mid, and treble ranges for music
        musicSource.pitch = Mathf.Lerp(0.5f, 2f, musicEqualizer.bass); // Example: adjust pitch for bass
        // The actual frequency ranges for mid and treble can be mapped similarly
    }

    // Equalizer control for each SFX category
    void UpdateSFXEqualizer()
    {
        // Adjust the bass, mid, and treble ranges for SFX
        sfxSource.pitch = Mathf.Lerp(0.5f, 2f, dashEqualizer.bass); // Example: adjust pitch for dash sound
        // Similarly adjust for other SFX like punch, landing, etc.
    }

    // Call the equalizer updates in Update to continuously adjust the audio properties
    void Update()
    {
        UpdateMusicEqualizer();
        UpdateSFXEqualizer();
    }
}
