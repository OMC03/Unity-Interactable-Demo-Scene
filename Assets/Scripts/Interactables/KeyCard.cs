using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyCard : Interactable
{
    Animator animator;
    [SerializeField] private AudioSource itemPickup;
    [SerializeField] private MeshRenderer meshRenderer;
    [SerializeField] private BoxCollider boxCollider;
    private string startPrompt;
    public bool hasKeyCard = false;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        startPrompt = promptMessage;  
    }

    // Update is called once per frame
    void Update()
    {
        if (animator.GetCurrentAnimatorStateInfo(0).IsName("KeyCardIdle"))
        {
            promptMessage = startPrompt;
        }
    }

    protected override void Interact()
    {
        hasKeyCard = true;
        itemPickup.Play();
        Debug.Log("Key Card Collected");
        Debug.Log(hasKeyCard);
        meshRenderer.enabled = false;  // Disable MeshRenderer
        boxCollider.enabled = false;   // Disable BoxCollider
    }
}
