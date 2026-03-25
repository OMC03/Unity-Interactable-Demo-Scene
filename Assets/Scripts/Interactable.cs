using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Interactable : MonoBehaviour
{
    //add or remove an InteractionEvent component to this gameobject
    public bool useEvents;

    //message that is displayed to the player when they are looking at an interactable
    public string promptMessage;

    //this function will be called from our player
    public void baseInteract()
    {
        if(useEvents)
        {
            GetComponent<InteractionEvent>().OnInteract.Invoke();
        }
            
        Interact();
    }

    protected virtual void Interact()
    {
        //we wont have any code written in the function
        //this is a templete function to be overridden by our subclasses
    }
}
