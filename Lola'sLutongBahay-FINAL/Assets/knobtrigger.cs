using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class knobtrigger : MonoBehaviour
{
    private Animator KnobTwistAnim;
    public Animator FireTrigger;
    private bool stoveTurnedOn = false;
    [SerializeField] UnityEvent actionEvent;

    // Audio setup
    public AudioClip knobOnSound;
    public AudioClip knobOffSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        KnobTwistAnim = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();

        // Add AudioSource if missing
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (stoveTurnedOn == false)
            {
                // Play animations
                KnobTwistAnim.Play("knobOn");
                FireTrigger.Play("FireOn");

                // Play sound
                PlaySound(knobOnSound);

                // Trigger events
                actionEvent.Invoke();
                stoveTurnedOn = true;
            }
            else
            {
                // Play animations
                KnobTwistAnim.Play("knobOff");
                FireTrigger.Play("FireOff");

                // Play sound
                PlaySound(knobOffSound);

                stoveTurnedOn = false;
            }
        }
    }

    private void PlaySound(AudioClip clip)
    {
        if (clip != null && audioSource != null)
        {
            audioSource.PlayOneShot(clip);
        }
    }
}