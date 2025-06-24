using UnityEngine;
using UnityEngine.Audio;

public class CookingAudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public AudioMixerGroup mixerGroup; // Optional: Use an Audio Mixer to control audio levels.

    [SerializeField]
    private AudioSource audioSourcePrefab; // Prefab for creating new AudioSources.

    // Play a single AudioClip by name.
    public void PlaySound(string clipName)
    {
        AudioClip clip = Resources.Load<AudioClip>(clipName);

        if (clip == null)
        {
            Debug.LogWarning("Audio clip not found: " + clipName);
            return;
        }

        PlayClip(clip);
    }

    // Play a specific AudioClip.
    public void PlayClip(AudioClip clip)
    {
        if (clip == null)
        {
            Debug.LogWarning("Tried to play a null AudioClip.");
            return;
        }

        // Create a new AudioSource and configure it.
        AudioSource audioSource = Instantiate(audioSourcePrefab);
        audioSource.clip = clip;
        audioSource.outputAudioMixerGroup = mixerGroup;
        audioSource.Play();

        // Destroy the AudioSource when the clip finishes playing.
        Destroy(audioSource.gameObject, clip.length);
    }
}
