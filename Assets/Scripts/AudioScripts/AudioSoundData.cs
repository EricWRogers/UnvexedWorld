using UnityEngine;

[System.Serializable]
public class AudioSoundData
{
    public string name;         // The name of the sound (used for identifying it)
    public AudioClip clip;      // The actual audio clip
    public float volume = 1f;   // Volume level (0 to 1)
    public float pitch = 1f;    // Pitch level (1 is normal)
    public bool loop = false;   // Whether or not the sound should loop
    
}
