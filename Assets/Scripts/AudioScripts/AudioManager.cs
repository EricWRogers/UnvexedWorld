using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Music and Sound Effect clips
    public AudioClip backgroundMusic;
    public AudioClip battleMusic;
    public AudioClip playerDash;
    public AudioClip punch;
    public AudioClip landing;
    public AudioClip hurt;

     public AudioClip orb;

    private AudioSource musicSource;
    private AudioSource sfxSource;

    void Awake()
    {
        // Initialize two AudioSources
        musicSource = gameObject.AddComponent<AudioSource>();
        sfxSource = gameObject.AddComponent<AudioSource>();

        musicSource.loop = true; // Music source should loop for background/battle music
    }

    public void PlayBackgroundMusic()
    {
        // Only play background music if it's not already playing
        if (musicSource.clip != backgroundMusic || !musicSource.isPlaying)
        {
            musicSource.Stop(); // Ensure previous music stops before playing new one
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void PlayBattleMusic()
    {
        // Only play battle music if it's not already playing
        if (musicSource.clip != battleMusic || !musicSource.isPlaying)
        {
            musicSource.Stop(); // Ensure previous music stops before playing new one
            musicSource.clip = battleMusic;
            musicSource.Play();
        }
    }

    public void PlayDashSound()
    {
        sfxSource.PlayOneShot(playerDash);
    }

    public void PlayPunchSound()
    {
        sfxSource.PlayOneShot(punch);
    }

    public void PlayLandingSound()
    {
        sfxSource.PlayOneShot(landing);
    }

    public void PlayHurtSound()
    {
        sfxSource.PlayOneShot(hurt);
    }
     public void PlayOrbSound()
    {
        sfxSource.PlayOneShot(orb);
    }

    public void StopMusic()
    {
        musicSource.Stop();
    }

    // New method to check if battle music is playing
    public bool IsBattleMusicPlaying()
    {
        return musicSource.clip == battleMusic && musicSource.isPlaying;
    }
}
