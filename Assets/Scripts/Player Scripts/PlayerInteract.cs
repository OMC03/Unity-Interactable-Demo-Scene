using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    private Camera cam;
    [SerializeField] private float distance = 3f;
    [SerializeField] private LayerMask interactableMask; // Mask for interactable objects
    [SerializeField] private LayerMask obstacleMask; // Mask for obstacles
    private PlayerUI playerUI;
    private InputManager inputManager;

    void Start()
    {
        cam = GetComponent<PlayerLook>().cam;
        playerUI = GetComponent<PlayerUI>();
        inputManager = GetComponent<InputManager>();
    }

    void Update()
    {
        playerUI.UpdateText(string.Empty);

        // Create a ray from the center of the camera shooting outward
        Ray ray = new Ray(cam.transform.position, cam.transform.forward);
        Debug.DrawRay(ray.origin, ray.direction * distance, Color.red);

        // Variable to store our collision information
        RaycastHit[] hits = Physics.RaycastAll(ray, distance);

        // Sort hits by distance
        System.Array.Sort(hits, (a, b) => a.distance.CompareTo(b.distance));

        foreach (RaycastHit hit in hits)
        {
            if (obstacleMask == (obstacleMask | (1 << hit.collider.gameObject.layer)))
            {
                // Hit an obstacle, stop processing
                return;
            }

            if (interactableMask == (interactableMask | (1 << hit.collider.gameObject.layer)))
            {
                Interactable interactable = hit.collider.GetComponent<Interactable>();
                if (interactable != null)
                {
                    playerUI.UpdateText(interactable.promptMessage);

                    // Check if the interact key is triggered
                    if (inputManager.onFoot.Interact.triggered)
                    {
                        interactable.baseInteract();
                    }
                }
                // Since interactable is found, stop further checking
                return;
            }
        }
    }
}
