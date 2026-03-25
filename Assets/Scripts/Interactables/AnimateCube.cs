using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateCube : Interactable
{
    Animator animator;
    private string startPrompt;
    private bool state = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPrompt = promptMessage;  
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("AtRest"))
        {
            promptMessage = startPrompt;
        }
        else
        {
            promptMessage = "Animating...";
        }
    }

    protected override void Interact()
    {
        state = !state;
        animator.SetBool("IsAtRest", state);
    }
}
