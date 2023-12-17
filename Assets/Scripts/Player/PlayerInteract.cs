using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteract : MonoBehaviour
{
    // Script References
    [SerializeField] private CellphoneFlashlight _cellphoneFlashlight;

    // Variables

    private void Update()
    {
        // Interact
        if (Input.GetKeyDown(KeyCode.E))
        {
            float interactRange = 2f;
            Collider[] colliderArray = Physics.OverlapSphere(transform.position, interactRange);
            foreach (Collider collider in colliderArray)
            {
                // Debug.Log(collider);
                if (collider.TryGetComponent(out NPCInteractable npcInteractable))
                {
                    npcInteractable.Interact(transform);
                }
            }
        } 

        // Flashlight
        if (Input.GetKeyDown(KeyCode.T))
        {
            _cellphoneFlashlight.ToggleFlashlight();
        }
    }
}
