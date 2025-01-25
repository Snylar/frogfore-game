using UnityEngine;
using UnityEngine.UI;

public class ButtonClickeSound : MonoBehaviour
{
    public AudioClip clickSound; // Assign your click sound in the Inspector
    private AudioSource audioSource;

    void Start()
    {
        // Get or add an AudioSource component
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        audioSource.playOnAwake = false;

        // Find all buttons in the scene
        Button[] buttons = FindObjectsOfType<Button>();

        // Add OnClick listeners to all buttons
        foreach (Button button in buttons)
        {
            button.onClick.AddListener(() => PlayClickSound());
        }
    }

    void PlayClickSound()
    {
        audioSource.PlayOneShot(clickSound);
    }
}