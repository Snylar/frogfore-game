using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputDetection : MonoBehaviour
{
    [SerializeField] Animator animator;
    public RecipeManager recipeManager;

    [Header("Key Pressed")]
    [SerializeField] string PressE;
    [SerializeField] string PressT;
    [SerializeField] string PressG;
    [SerializeField] string PressS;
    [SerializeField] string PressEnter;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("e"))
        {
            recipeManager.playerActions.Add(PressE);
            string result = recipeManager.CheckSequence();
            animator.Play("press-e");
        }
        else if (Input.GetKeyDown("t"))
        {
            recipeManager.playerActions.Add(PressT);
            string result = recipeManager.CheckSequence();
            animator.Play("press-t");
        }
        else if (Input.GetKeyDown("g"))
        {
            recipeManager.playerActions.Add(PressG);
            string result = recipeManager.CheckSequence();
            animator.Play("press-g");
        }
        else if (Input.GetKeyDown("s"))
        {
            recipeManager.playerActions.Add(PressS);
            string result = recipeManager.CheckSequence();
            animator.Play("press-s");
        }
        else if (Input.GetKeyDown(KeyCode.Return))
        {
            recipeManager.playerActions.Add(PressEnter);
            string result = recipeManager.CheckSequence();
            animator.Play("press-enter");
        }
    }
}
