using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class knobtrigger : MonoBehaviour
{
    private Animator KnobTwistAnim;
    public Animator FireTrigger;
    public VisualEffect vfxEffect; // Assign in the Inspector
    private bool stoveTurnedOn = false;

    [Header("Managers")]
    public RecipeManager recipeManager;
    public ServingManager servingManager;
    public string actionName;

    [SerializeField] UnityEvent actionEvent;
    [SerializeField] UnityEvent TurnoffEvent;
    

    // Audio setup
    public AudioClip knobOnSound;
    public AudioClip knobOffSound;
    private AudioSource audioSource;

    // Start is called before the first frame update
    void Start()
    {
        vfxEffect.Stop();
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
                //FireTrigger.Play("FireOn");

                // Play sound
                PlaySound(knobOnSound);

                vfxEffect.Play();
                
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
                KnobTwistAnim.Play("knobOff");
                FireTrigger.Play("FireOff");

                vfxEffect.Stop();

                //Recipe Manager
                recipeManager.playerActions.Add("turnOff");
                    string result = recipeManager.CheckSequence();
                    Debug.Log(result);

                // Play sound
                PlaySound(knobOffSound);
                TurnoffEvent.Invoke();

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