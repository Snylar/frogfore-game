using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.VFX;

public class knobtrigger : MonoBehaviour
{
    private Animator KnobTwistAnim;
    public Animator FireTrigger;
    private bool stoveTurnedOn = false;
<<<<<<< HEAD
    [SerializeField] UnityEvent actionEvent;
=======
    [SerializeField] VisualEffect actionEvent;

    [Header("Managers")]
    public RecipeManager recipeManager;
    public ServingManager servingManager;
    public string actionName;
>>>>>>> 3772f6c181df36b40a8e567ce28586393d464b48
    // Start is called before the first frame update
    void Start()
    {
        KnobTwistAnim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnMouseDown()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (stoveTurnedOn == false)
            {
                KnobTwistAnim.Play("knobOn");
                FireTrigger.Play("FireOn");
<<<<<<< HEAD
                actionEvent.Invoke();
=======
                actionEvent.Play();

                recipeManager.playerActions.Add(actionName);
                string result = recipeManager.CheckSequence();
                Debug.Log(result);

>>>>>>> 3772f6c181df36b40a8e567ce28586393d464b48
                stoveTurnedOn = true;
            }
            else
            {
                KnobTwistAnim.Play("knobOff");
                FireTrigger.Play("FireOff");
                stoveTurnedOn = false;
            }
        }
    }
}
