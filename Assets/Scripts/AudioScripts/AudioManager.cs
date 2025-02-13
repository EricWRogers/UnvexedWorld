using UnityEngine;
using System.Collections;
using System.Linq;

public class AudioManager : MonoBehaviour
{
    public SoundData[] sounds;  // Array of sound settings (Set in Unity Inspector)
    //public AudioSoundData audioSoundData;
    public SoundPool soundPool; // Reference to the SoundPool (Assign in Unity)
    private AudioSource audioSource;
    [SerializeField] private AudioSource backgroundMusicSource;
    [SerializeField] private AudioSource battleMusicSource;
    public static AudioManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    // Play a Sound by Name
    public void Play(string name)
    {
        Debug.Log("AudioManager: Play " + name);
        SoundData sound = sounds.FirstOrDefault(s => s.name == name);
        if (sound != null && soundPool != null)
        {
            GameObject audioObject = soundPool.GetPooledObject();
            if (audioObject != null)
            {
                AudioSource audioSource = audioObject.GetComponent<AudioSource>();
                audioSource.clip = sound.clip;
                audioSource.volume = sound.volume;
                audioSource.pitch = sound.pitch;
                audioSource.loop = sound.loop;
                audioObject.SetActive(true);
                audioSource.Play();

                // Disable the object after the sound finishes playing
                if (!sound.loop)
                {
                    StartCoroutine(DeactivateAfterPlay(audioSource));  // Fix here
                }
            }
        }
        else
        {
            Debug.LogWarning($"Sound '{name}' was not found!");
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.Play();
        }
    }

    public void PlayBattleMusic()
    {
        if (battleMusicSource != null && !battleMusicSource.isPlaying)
        {
            battleMusicSource.Play();
        }
    }

    public bool IsBattleMusicPlaying()
    {
        return battleMusicSource != null && battleMusicSource.isPlaying;
    }

    public bool IsBackgroundMusicPlaying()
    {
        return backgroundMusicSource != null && backgroundMusicSource.isPlaying;
    }

    // Crossfade Function for music
    public void CrossfadeBattleToBackground(float fadeDuration = 2f)
    {
        StartCoroutine(Crossfade(fadeDuration));
    }

    // Coroutine for fading
    private IEnumerator Crossfade(float fadeDuration)
    {
        float startTime = Time.time;
        float battleMusicStartVolume = battleMusicSource.volume;
        float backgroundMusicStartVolume = backgroundMusicSource.volume;

        //Battle music fade
        while (Time.time - startTime < fadeDuration)
        {
            float t = (Time.time - startTime) / fadeDuration;
            battleMusicSource.volume = Mathf.Lerp(battleMusicStartVolume, 0f, t);
            backgroundMusicSource.volume = Mathf.Lerp(backgroundMusicStartVolume, 1f, t);
            yield return null;
        }

        //final volumes
        battleMusicSource.volume = 0f;
        backgroundMusicSource.volume = 1f;

        //Stop battle music after fade out
        battleMusicSource.Stop();
    }

    // Stop a Specific Sound
    public void Stop(string name)
    {
        SoundData sound = sounds.FirstOrDefault(s => s.name == name);
        if (sound != null)
        {
            foreach (GameObject obj in soundPool.pooledObjects)
            {
                AudioSource audioSource = obj.GetComponent<AudioSource>();
                if (audioSource.clip == sound.clip && obj.activeInHierarchy)
                {
                    audioSource.Stop();
                    obj.SetActive(false);
                    return;
                }
            }
        }
    }

    // Stop All Sounds
    public void StopAll()
    {
        foreach (GameObject obj in soundPool.pooledObjects)
        {
            obj.SetActive(false);
        }
    }

    // Deactivate Pooled Object After Sound Finishes
    private IEnumerator DeactivateAfterPlay(AudioSource source)  // Fix here
    {
        yield return new WaitForSeconds(source.clip.length);  // Correct reference to `source`
        source.gameObject.SetActive(false);  // Correct reference to `source`
    }

    // 🎵 Quick Play Methods for Specific Sounds
    public void PlayPunchSound() => Play("PunchSound");
    public void PlayDashSound() => Play("DashSound");
    public void PlayLandingSound() => Play("LandingSound");
}
