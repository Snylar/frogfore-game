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
                actionEvent.Invoke();
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
