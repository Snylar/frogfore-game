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
    [SerializeField] VisualEffect actionEvent;

    [Header("Managers")]
    public RecipeManager recipeManager;
    public ServingManager servingManager;
    public string actionName;
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
                actionEvent.Play();

                recipeManager.playerActions.Add(actionName);
                string result = recipeManager.CheckSequence();
                Debug.Log(result);

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
