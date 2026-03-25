using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AltKeyPad : Interactable
{
    [SerializeField] private GameObject door;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource doorOpening;
    [SerializeField] private AudioSource doorClosing;
    private bool doorOpen;
    private string startPrompt;

    // Start is called before the first frame update
    void Start()
    {
        startPrompt = promptMessage;
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Closed"))
        {
            promptMessage = startPrompt;
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            promptMessage = "Door Is Open";
        }
    }
    //this function is where we will dessign our interaction using code
    protected override void Interact()
    {
        doorOpen = !doorOpen;
        door.GetComponent<Animator>().SetBool("isOpen", doorOpen);

        if (doorOpen)
        {
            doorOpening.Play();
        }
        else
        {
            doorClosing.Play();
        }
    }
}
