using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPanelSound : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip objectSound;

    private void Awake()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayObjectSound()
    {
        if (audioSource != null && objectSound != null)
        {
            audioSource.PlayOneShot(objectSound);
        }
    }
}