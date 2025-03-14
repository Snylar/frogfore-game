using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class griposcript : MonoBehaviour
{
    public Animator GripoOn;
    public string animationtag;
    private bool stoveTurnedOn = false;

    [Header("Managers")]
    public RecipeManager recipeManager;
    public ServingManager servingManager;
    public string actionName;

    [SerializeField] UnityEvent actionEvent;
    

    // Audio setup
    public AudioClip knobOnSound;
    public AudioClip knobOffSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        // Add AudioSource if missing
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Gumagana!");
        if (Input.GetMouseButtonDown(0))
        {
            if (stoveTurnedOn == false)
            {
                // Play animations
                GripoOn.Play(animationtag);

                // Play sound
                PlaySound(knobOnSound);
                
                //Recipe Manager
                recipeManager.playerActions.Add(actionName);
                    string result = recipeManager.CheckSequence();
                    Debug.Log(result);

                // Trigger events
                actionEvent.Invoke();
                stoveTurnedOn = true;
            }
            else
            {
                // Play animations
                //KnobTwistAnim.Play("knobOff");
                //FireTrigger.Play("FireOff");

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