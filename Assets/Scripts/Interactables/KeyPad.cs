using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyPad : Interactable
{
    public KeyCard card;
    public KeyPad_2 keyPad2;
    [SerializeField] private GameObject door;
    [SerializeField] private Animator anim;
    [SerializeField] private AudioSource unlockSound;
    [SerializeField] private AudioSource lockSound;
    [SerializeField] private AudioSource doorOpening;
    [SerializeField] private AudioSource doorClosing;
    [SerializeField] private Material keypadEnabled;
    private MeshRenderer mesh;
    private string startPrompt;
    private bool isLocked;
    public bool doorOpen;
    private bool playSound = true;
    private bool doorUnlocked = false; // Track if the door has been unlocked

    // Start is called before the first frame update
    void Start()
    {
        mesh = GetComponent<MeshRenderer>();
        startPrompt = promptMessage;
        StartCoroutine(DelayedAssignment());
    }

    IEnumerator DelayedAssignment()
    {
        yield return new WaitForSeconds(1f); // Adjust the delay time as needed
        card = FindObjectOfType<KeyCard>();
        if (card == null)
        {
            Debug.LogError("KeyCard component not found in the scene.");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Locked"))
        {
            if (card.hasKeyCard)
            {
                promptMessage = "Insert Keycard";
            }
            else
            {
                promptMessage = startPrompt;
                isLocked = true;
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Closed"))
        {
            promptMessage = "[E] Use Keypad";
            isLocked = false;
            mesh.material = keypadEnabled;
            if (!keyPad2.doorOpen && !doorUnlocked) // Check if door is not open and not already unlocked
            {
                keyPad2.UnlockKeyPad(true);  // Unlock the main keypad
                doorUnlocked = true; // Mark door as unlocked
            }
        }
        else if (anim.GetCurrentAnimatorStateInfo(0).IsName("Open"))
        {
            promptMessage = "Door Is Open";
            isLocked = false;
        }
    }

    public void UnlockKeyPad2(bool unLocked)
    {
        anim.SetBool("KeyCard", unLocked);
    }

    protected override void Interact()
    {
        bool hasCard = false;

        if (card != null)
        {
            hasCard = card.hasKeyCard;

            if (hasCard)
            {
                door.GetComponent<Animator>().SetBool("KeyCard", true);
                if (playSound && !doorUnlocked)
                {
                    unlockSound.Play();
                    playSound = false;
                    doorUnlocked = true; // Mark door as unlocked
                }
            }
        }
        else
        {
            Debug.LogError("KeyCard component is not assigned to the KeyPad script.");
            return;
        }

        if (isLocked)
        {
            if (!hasCard)
            {
                lockSound.Play();
            }
        }

        if (!isLocked)
        {
            doorOpen = !doorOpen;
            door.GetComponent<Animator>().SetBool("isOpen", doorOpen);
            keyPad2.doorOpen = doorOpen; // Synchronize the state with the second keypad

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
}
